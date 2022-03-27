using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingPageViewModel();
            
        }

        protected override bool OnBackButtonPressed()
        {
            App.Config = (this.BindingContext as SettingPageViewModel).Config;
            return base.OnBackButtonPressed();
        }
    }
}