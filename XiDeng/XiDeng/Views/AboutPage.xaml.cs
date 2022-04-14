using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.lVersion.Text = $"当前版本：{VersionTracking.CurrentVersion}";
        }

        private void YuanLiMan_Tapped(object sender, EventArgs e)
        {
            
        }

        private async void CopyQQ_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("745872311");
            await Shell.Current.DisplayToastAsync("已复制群号");
        }

        private async void GotoLicenseFragment_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LicenseFragmentPage));
        }

        private async void CheckUpdate_Clicked(object sender, EventArgs e)
        {
            await VersionHelper.CheckUpdate();
        }
    }
}