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
    public partial class MyPlanPage : ContentPage
    {
        public MyPlanPage()
        {
            InitializeComponent();
            this.BindingContext = new MyPlanPageViewModel();
        }
    }
}