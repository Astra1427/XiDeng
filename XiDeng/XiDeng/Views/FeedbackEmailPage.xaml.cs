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
    public partial class FeedbackEmailPage : ContentPage
    {
        public FeedbackEmailPage()
        {
            InitializeComponent();
            this.BindingContext = new FeedbackEmailPageViewModel();
        }
    }
}