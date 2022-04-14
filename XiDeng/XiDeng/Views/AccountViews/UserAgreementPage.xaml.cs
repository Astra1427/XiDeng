using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;

namespace XiDeng.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAgreementPage : ContentPage
    {
        public UserAgreementPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadUserAgreementAsync();
        }
        private async Task LoadUserAgreementAsync()
        {
            try
            {
                rvView.IsRefreshing = true;
                var response = await Utility.Client.GetStringAsync("https://gitee.com/AC200/turn-off-the-lights/raw/master/%E7%94%A8%E6%88%B7%E5%8D%8F%E8%AE%AE.txt");
                lContent.Text = response;
            }
            catch (Exception ex)
            {
                await this.Message("获取用户协议失败！");
            }
            finally
            {
                rvView.IsRefreshing = false;
            }
        }
        private async void rvView_Refreshing(object sender, EventArgs e)
        {
            await LoadUserAgreementAsync();

        }
    }
}