using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.AccountViewModels;

namespace XiDeng.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountSettingPage : ContentPage
    {
        public AccountSettingPage()
        {
            InitializeComponent();
            this.BindingContext = new AccountSettingPageViewModel();
        }
    }
}