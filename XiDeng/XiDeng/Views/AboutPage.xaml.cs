using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}