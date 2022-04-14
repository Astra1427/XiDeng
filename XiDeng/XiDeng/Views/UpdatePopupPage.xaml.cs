using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;

namespace XiDeng.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePopupPage : PopupPage
	{
        public VersionModel VersionModel { get; set; }
        public UpdatePopupPage ( VersionModel model)
		{
			InitializeComponent ();
			VersionModel = model;
			this.BindingContext = this;
		}

        private async void Update_Clicked(object sender, EventArgs e)
        {
			await Browser.OpenAsync(new Uri(VersionModel.DownloadAddress));
        }
    }
}