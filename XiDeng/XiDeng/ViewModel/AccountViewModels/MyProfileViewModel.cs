using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Models.AccountModels;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;
using XiDeng.Views;
using XiDeng.Views.AccountViews;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class MyProfileViewModel : BaseViewModel
    {
        private AccountDTO account;

        public AccountDTO Account
        {
            get { return account; }
            set
            {
                account = value;
                RaisePropertyChanged(nameof(Account));
                IsLogged = value != null;
            }
        }
        public ImageSource ImgRight => Utility.GetImage("arrow_63_240");
        private bool isLogged;
        public bool IsLogged
        {
            get { return isLogged; }
            set
            {
                isLogged = value;
                this.RaisePropertyChanged(nameof(IsLogged));
            }
        }


        public MyProfileViewModel()
        {
            PrepareAccountDataCommand = new Command<object>(delegate
            {
                this.Account = Utility.LoggedAccount;
                if (this.Account == null || this.Account.JwtToken == null)
                {
                    //try load login info
                    string loginInfoJson = FileHelper.ReadFile(FileHelper.LoginInfoFile);
                    this.Account = loginInfoJson.To<AccountDTO>() ?? Account;
                    Utility.LoggedAccount = this.Account;
                    if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty())
                    {
                        IsLogged = false;
                    }
                }
            });

            ViewMyProfileCommand = new Command<object>(async obj =>
            {
                //check login info is valid in server
                if (this.Account == null || this.Account.JwtToken == null)
                {
                    //goto login page
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return;
                }

                //goto user info page
                await Shell.Current.GoToAsync(nameof(PersonalInfoPage));
            });
            GotoMyPlanPageCommand = new Command<object>(async obj =>
            {
                await Shell.Current.GoToAsync(nameof(MyPlanPage));
            });

            GotoSettingsPage = new Command<object>(async delegate
            {
                await Shell.Current.GoToAsync(nameof(SettingPage));
            });
            TapTableItemCommand = new Command<object>(async tag =>
            {
                if (tag == null || tag.ToString().IsEmpty())
                {
                    return;
                }
                await Shell.Current.GoToAsync(tag.ToString());
            });

            SynchronizationCommand = new Command<object>(async obj =>
            {
                string selected = await Shell.Current.DisplayActionSheet("同步", "取消", "", "本地数据上传至云端", "云端数据覆盖至本地");

                await this.Try(async o =>
                {
                    if (selected == "本地数据上传至云端")
                    {
                        await SynchronizationHelper.LocalToCloud();
                    }
                    else if (selected == "云端数据覆盖至本地")
                    {
                        await SynchronizationHelper.CloudToLocal();
                    }
                    else
                    {

                    }

                    //Set data is updated
                    await App.Database.ExercisePlans.SetDataUpdated();
                    await App.Database.PlanEachDays.SetDataUpdated();
                    await App.Database.Skills.SetDataUpdated();
                    await App.Database.SkillStyles.SetDataUpdated();
                    await App.Database.Standards.SetDataUpdated();

                }, obj, true);
            });
        }



        public Command<object> PrepareAccountDataCommand { get; set; }
        public Command<object> ViewMyProfileCommand { get; set; }
        public Command<object> GotoMyPlanPageCommand { get; set; }
        public Command<object> GotoSettingsPage { get; set; }
        public Command<object> SynchronizationCommand { get; set; }
        public Command<object> TapTableItemCommand { get; set; }



    }
}
