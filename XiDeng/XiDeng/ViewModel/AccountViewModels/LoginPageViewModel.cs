using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Models.AccountModels;
using XiDeng.Models.SkillModels;
using XiDeng.Views.AccountViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.RaisePropertyChanged(nameof(Password));
            }
        }

        public LoginPageViewModel()
        {
            LoginCommand = new Command<object>(async obj => {
                await this.Try(async o=> {
                    if (Email.IsEmpty() || Password.IsEmpty())
                    {
                        await this.Message("请输入账号密码！");
                        return;
                    }

                    //Login
                    string json = new { Email = this.Email, Password = this.Password }.ToJson();

                    var response = await ActionNames.Account.Authenticate.PostAsync(new StringContent(json, Encoding.UTF8, "application/json"));
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        //login failed
                        await this.Message($"登录失败！\n{response.Message}");
                        return;
                    }


                    //login success!
                    var loggedAccount = response.Content.To<AccountDTO>();
                    FileHelper.WriteFile(FileHelper.LoginInfoFile, loggedAccount.ToJson());
                    

                    if (Utility.LoggedAccount?.Id != loggedAccount.Id)
                    {
                        //cloud to local
                        Utility.LoggedAccount = loggedAccount;
                        if (await SynchronizationHelper.CloudToLocal())
                        {
                            await Shell.Current.GoToAsync("../");
                        }
                        return;

                    }
                    //save login info 
                    Utility.LoggedAccount = loggedAccount;

                    //if first run
                    //down load app data

                    var skillResponse = await ActionNames.Skill.GetSkillsWithDefault.GetStringAsync();
                    if (!skillResponse.IsSuccessStatusCode)
                    {
                        await this.Message("加载数据失败。\n请检查你的网络连接是否正常。");
                    }
                    else
                    {
                        SkillDataCommon.Skills = skillResponse.Content.To<ObservableCollection<SkillDTO>>();
                        FileHelper.WriteFile(FileHelper.SkillFile, skillResponse.Content);
                        if (await App.Database.CheckConflictAsync<SkillDTO>(SkillDataCommon.Skills.FirstOrDefault(x => x.OwnerId == Utility.LoggedAccount.Id)))
                        {
                            await this.Message("本地数据与云端数据冲突，请执行同步操作！");
                        }
                        else
                        {
                            int rows = await App.Database.SaveAllAsync(SkillDataCommon.Skills);
#if DEBUG
                            await this.Message("Download Rows:"+rows);
#endif
                        }
                    }

                    //go to my profile page
                    await Shell.Current.GoToAsync("../");
                },obj,true);
                

            });

            ForgotPasswordCommand = new Command<object>(async obj =>
            {
                await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
            });

            RegisterCommand = new Command<object>(async obj =>
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            });
        }
        
        public Command<object> LoginCommand { get; set; }
        public Command<object> ForgotPasswordCommand { get; set; }
        public Command<object> RegisterCommand { get; set; }

    }
}
