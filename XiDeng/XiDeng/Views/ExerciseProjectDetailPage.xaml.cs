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
    [QueryProperty("Eid", "EID")]
    public partial class ExerciseProjectDetailPage : ContentPage
    {
        public int Eid
        {
            set {
                //int v = 0;
                
                //try
                //{
                //    v  = int.Parse(value);
                //}
                //catch (Exception ex)
                //{
                //    return;
                //}
                this.BindingContext = new ExerciseProjectDetailPageViewModel(value);
            }
        }

        public ExerciseProjectDetailPage()
        {
            InitializeComponent();
            //this.BindingContext = new ExerciseProjectDetailPageViewModel();
        }
    }
}