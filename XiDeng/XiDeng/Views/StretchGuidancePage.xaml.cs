using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile),QueryProperty("VideoUrl", "VideoUrl")]

    public partial class StretchGuidancePage : ContentPage
    {
        private string videoUrl;

        public string VideoUrl
        {
            get { return videoUrl; }
            set { videoUrl = Uri.UnescapeDataString(value); }
        }

        public StretchGuidancePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.wv.Source = VideoUrl;
            this.wv.Reload();
        }
    }
}