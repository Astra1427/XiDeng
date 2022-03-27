using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.ViewModel.PlanViewModels;

namespace XiDeng.Views.PlanViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("PlanId","PlanId")]
    [QueryProperty("ByWeek", "ByWeek")]
    public partial class PlanDetailPage : ContentPage
    {
        private PlanDetailPageViewModel vm;
        private Guid planId;

        public string PlanId
        {
            set { 
                planId = Guid.Parse(value);
            }
        }
        private bool byWeek;

        public string ByWeek
        {
            
            set {
                byWeek = bool.Parse(value);
                dwConvert.ByWeek = byWeek;

            }
        }


        public PlanDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.vm = new PlanDetailPageViewModel(planId);
            await vm.Init();
            this.BindingContext = vm;

            if (planId != Utility.LoggedAccount?.Id)
            {
                this.ToolbarItems.RemoveAt(0);
                this.ToolbarItems.RemoveAt(0);
                this.ToolbarItems.RemoveAt(0);
            }

        }

    }
}