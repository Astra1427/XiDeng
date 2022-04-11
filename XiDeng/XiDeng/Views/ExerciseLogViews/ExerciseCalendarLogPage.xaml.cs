using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.ExerciseLogViewModels;

namespace XiDeng.Views.ExerciseLogViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseCalendarLogPage : ContentPage
    {
        public ExerciseCalendarLogPage()
        {
            InitializeComponent();
            this.BindingContext = new ExerciseCalendarLogPageViewModel();
        }
    }
}