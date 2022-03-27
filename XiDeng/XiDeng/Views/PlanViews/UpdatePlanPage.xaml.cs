using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Common;
using XiDeng.ViewModel.PlanViewModels;

namespace XiDeng.Views.PlanViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("PlanJson", "PlanJson")]
    public partial class UpdatePlanPage : ContentPage
    {
        private ExercisePlanDTO Plan;

        public string PlanJson
        {
            set
            {
                Plan = value.To<ExercisePlanDTO>();
                if (this.BindingContext == null)
                    this.BindingContext = new UpdatePlanPageViewModel(Plan);
            }
        }


        public UpdatePlanPage()
        {
            InitializeComponent();
        }
    }
}