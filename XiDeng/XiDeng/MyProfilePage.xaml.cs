using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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