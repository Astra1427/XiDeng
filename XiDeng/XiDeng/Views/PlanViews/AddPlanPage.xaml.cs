using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.ViewModel.PlanViewModels;

namespace XiDeng.Views.PlanViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlanPage : ContentPage
    {
        public AddPlanPage()
        {
            InitializeComponent();
            this.BindingContext = new AddPlanPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}