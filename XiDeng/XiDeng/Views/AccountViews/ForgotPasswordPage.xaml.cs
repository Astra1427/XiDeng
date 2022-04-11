using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.AccountViewModels;
using XiDeng.Common;

namespace XiDeng.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("AccountEmail", "AccountEmail")]
    public partial class ForgotPasswordPage : ContentPage
    {
        private string accountEmail;

        public string AccountEmail
        {
            get { return accountEmail; }
            set {
                accountEmail = value;
                this.BindingContext = new ForgotPasswordPageViewModel(value);
            }
        }

        public ForgotPasswordPage()
        {
            InitializeComponent();
            if (AccountEmail.IsEmpty())
            {
                this.BindingContext = new ForgotPasswordPageViewModel();
            }
        }
    }
}