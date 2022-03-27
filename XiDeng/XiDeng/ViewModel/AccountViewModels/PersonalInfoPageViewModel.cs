using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.AccountModels;

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
            ExitLoginCommand = new Command<object>(async obj=> {
                //clear login info
                FileHelper.WriteFile(FileHelper.LoginInfoFile,"");
                Utility.LoggedAccount = new AccountDTO() ;
                await Shell.Current.GoToAsync("../");
            });
        }

        public Command<object> ExitLoginCommand { get; set; }

    }
}
