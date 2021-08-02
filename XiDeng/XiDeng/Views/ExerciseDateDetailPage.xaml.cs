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
    [QueryProperty("ExerciseDate","ExerciseDate")]
    public partial class ExerciseDateDetailPage : ContentPage
    {
        public string ExerciseDate { set {
                this.Title = value;
                this.BindingContext = new ExerciseDateDetailPageViewModel(value);
            } }
        public ExerciseDateDetailPage()
        {
            InitializeComponent();
        }
    }
}