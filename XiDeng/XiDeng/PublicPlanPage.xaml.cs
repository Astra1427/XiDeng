using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.PlanViewModels;
namespace XiDeng
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublicPlanPage : ContentPage
    {
        public PublicPlanPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext.GetType() != typeof(PublicPlanPageViewModel))
            {
                this.BindingContext = new PublicPlanPageViewModel();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (this.BindingContext is PublicPlanPageViewModel vm)
            {
                vm.IsRefresh = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}
