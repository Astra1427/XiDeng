using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonationPage : ContentPage
    {
        public DonationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.imgAliPay.Source = Utility.GetImage("AliPay");
            this.imgWeChatPay.Source = Utility.GetImage("WeChatPay");
        }

        public bool IsBigShow = false;
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsBigShow = true;
            BigImg.IsVisible = IsBigShow;
            BigImg.Source = (sender as Image).Source;
        }

        private void Hide_Tapped(object sender, EventArgs e)
        {
            IsBigShow = false;
            BigImg.IsVisible = IsBigShow;
        }
    }
}