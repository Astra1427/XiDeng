using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.AccountModels;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class DestroyAccountPageViewModel:BaseViewModel
    {
        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set
            {
                accountName = value;
                this.RaisePropertyChanged(nameof(AccountName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                this.RaisePropertyChanged(nameof(Email));
            }
        }
        private int useDay;
        public int UseDay
        {
            get { return useDay; }
            set
            {
                useDay = value;
                this.RaisePropertyChanged(nameof(UseDay));
            }
        }
        private string authCode;
        public string AuthCode
        {
            get { return authCode; }
            set
            {
                authCode = value;
                this.RaisePropertyChanged(nameof(AuthCode));
            }
        }
        private static int sendEmailCountDown = 60;
        public int SendEmailCountDown
        {
            get { return sendEmailCountDown; }
            set
            {

                sendEmailCountDown = value == 0 ? 60 : value;

                this.RaisePropertyChanged(nameof(SendEmailCountDown));
                this.RaisePropertyChanged(nameof(SendEmailButtonText));
            }
        }
        private static string sendEmailButtonText = "发送验证码到邮箱";
        public string SendEmailButtonText
        {
            get { return sendEmailButtonText; }
            set
            {
                sendEmailButtonText = SendEmailCountDown == 60 ? "发送验证码到邮箱" : value;
                this.RaisePropertyChanged(nameof(SendEmailButtonText));
            }
        }
        private bool canExecuteSendEmail = true;
        public bool CanExecuteSendEmail
        {
            get { return canExecuteSendEmail; }
            set
            {
                canExecuteSendEmail = value;
                this.RaisePropertyChanged(nameof(CanExecuteSendEmail));
            }
        }
        private bool isDestroyData;
        public bool IsDestroyData
        {
            get { return isDestroyData; }
            set
            {
                isDestroyData = value;
                this.RaisePropertyChanged(nameof(IsDestroyData));
            }
        }

        public DestroyAccountPageViewModel()
        {
            AccountName = Utility.LoggedAccount.Name;
            Email = Utility.LoggedAccount.Email;
            UseDay = (int)(DateTime.Now - Utility.LoggedAccount.CreateTime).TotalDays;
            if (IsTimer)
            {
                CanExecuteSendEmail = false;
            }

            SendCodeToEmailCommand = new AsyncCommand(async ()=> {
                //request email
                await this.Try<object>(async o=> {
                    await Task.Delay(200);
                    var response = await (ActionNames.Account.SendDestroyAccountEmail + $"?email={Email}").PostAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        await this.Message($"失败！\n{response.Message}");
                    }
                    CanExecuteSendEmail = false;
                    SetCountDown();
                },null,true);
            });
            DestroyCommand = new AsyncCommand(async ()=> {
                if (AuthCode.IsEmpty())
                {
                    await  this.Message("请输入验证码");
                    return;
                }

                if (!await this.YesMessage("确定注销账号？\n此操作将无法撤销！"))
                {
                    return;
                }

                await this.Try<object>(async o=> {
                    await Task.Delay(200);
                    var response = await (ActionNames.Account.DestroyAccount + $"?email={Email}&code={AuthCode}&isDestroyData={IsDestroyData}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        await this.Message("账号注销成功!");
                        //clear login info
                        FileHelper.WriteFile(FileHelper.LoginInfoFile, "");
                        Utility.LoggedAccount = new AccountDTO();
                        await this.GoAsync("../");
                    }
                    else
                    {
                        await this.Message($"失败！\n{response.Message}");
                    }
                },null,true);
            });
        }

        public static bool IsTimer { get; set; }
        private void SetCountDown()
        {
            
            IsTimer = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                SendEmailButtonText = $"{--SendEmailCountDown} 秒后可再次发送";
                if (SendEmailCountDown <= 0 || SendEmailCountDown == 60)
                {
                    CanExecuteSendEmail = true;
                    IsTimer = false;
                    return false;
                }
                CanExecuteSendEmail = false;
                return true;
            });
            
        }

        public AsyncCommand SendCodeToEmailCommand { get; set; }
        public AsyncCommand DestroyCommand { get; set; }
    }
}
