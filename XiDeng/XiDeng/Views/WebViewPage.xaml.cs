using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile), QueryProperty("WebUrl", "WebUrl")]
    public partial class WebViewPage : ContentPage
    {
        private string webUrl;

        public string WebUrl
        {
            get { return webUrl; }
            set { webUrl = Uri.UnescapeDataString(value); }
        }
        public WebViewPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.wv.Source = WebUrl;
            this.wv.Reload();
        }
    }
}