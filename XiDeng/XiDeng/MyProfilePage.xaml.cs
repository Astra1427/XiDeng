using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.ViewModel.AccountViewModels;

namespace XiDeng
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            this.BindingContext = new MyProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }



    }
}