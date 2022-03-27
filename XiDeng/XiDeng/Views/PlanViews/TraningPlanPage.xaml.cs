using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.PlanViewModels;

namespace XiDeng.Views.PlanViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("RunningPlanID", "RunningPlanID")]
    public partial class TraningPlanPage : ContentPage
    {
        private Guid runningPlanID;

        public string RunningPlanID
        {
            set {
                Guid id = Guid.Empty;
                Guid.TryParse(value, out id);
                runningPlanID = id; 
            }
        }
        public TraningPlanPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new TraningPlanPageViewModel(runningPlanID);
        }


        protected override bool OnBackButtonPressed()
        {
            (this.BindingContext as TraningPlanPageViewModel).ClearSound();
            (this.BindingContext as TraningPlanPageViewModel).IsBack = true;
            return base.OnBackButtonPressed();
        }
    }
}