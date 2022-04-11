using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }

        private async void GotoFeedbackEmailPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackEmailPage));
        }
    }
}