using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.AccountModels;
using XiDeng.Views.AccountViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class PersonalInfoPageViewModel:BaseViewModel
    {
        private AccountDTO account;
        public AccountDTO Account
        {
            get { return account; }
            set
            {
                account = value;
                this.RaisePropertyChanged(nameof(Account));
            }
        }
        public ImageSource ImgRight => Utility.GetImage("arrow_63_240");
        public PersonalInfoPageViewModel()
        {
            this.Account = Utility.LoggedAccount;
            if (Account.JwtToken == null)
            {
                //await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            GotoEditAccountNameCommand = new Command<object>(async delegate
            {
                var popup = new EditAccountNamePopupPage();
                await Shell.Current.Navigation.PushPopupAsync(popup);
                bool result = await popup.PopupClosedTask;
                if (result)
                {
                    Account.Name = Utility.LoggedAccount.Name;
                    this.RaisePropertyChanged("Account");
                }

            });
            ExitLoginCommand = new Command<object>(async obj=> {
                //clear login info
                FileHelper.WriteFile(FileHelper.LoginInfoFile,"");
                Utility.LoggedAccount = new AccountDTO() ;
                await this.GoAsync("../");
            });
        }

        public Command<object> GotoEditAccountNameCommand { get; set; }
        public Command<object> ExitLoginCommand { get; set; }

    }
}
