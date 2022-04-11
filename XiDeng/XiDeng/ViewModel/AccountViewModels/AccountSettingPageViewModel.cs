using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using XiDeng.Common;
using XiDeng.Models.AccountModels;
using XiDeng.Views.AccountViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class AccountSettingPageViewModel:BaseViewModel
    {
        public AccountSettingPageViewModel()
        {
            GotoResetPasswordPageCommand = new AsyncCommand(async ()=> {
                await this.GoAsync(nameof(ForgotPasswordPage)+$"?AccountEmail={Utility.LoggedAccount.Email}");
            });
            LogoutComamnd = new AsyncCommand(async ()=> {
                //clear login info
                FileHelper.WriteFile(FileHelper.LoginInfoFile, "");
                Utility.LoggedAccount = new AccountDTO();
                await this.GoAsync("../");
            });
            GotoDestroyAccountPageCommand = new AsyncCommand(async ()=> {
                await this.GoAsync(nameof(DestroyAccountPage));
            });

        }
        public AsyncCommand GotoResetPasswordPageCommand { get; set; }
        public AsyncCommand LogoutComamnd { get; set; }
        public AsyncCommand GotoDestroyAccountPageCommand { get; set; }

    }
}
