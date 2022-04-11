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
    [QueryProperty("EID", "EID")]
    public partial class ExerciseProjectDetailPage : ContentPage
    {
        public string Eid
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
                this.BindingContext = new ExerciseProjectDetailPageViewModel(Guid.Parse(value));
            }
        }

        public ExerciseProjectDetailPage()
        {
            InitializeComponent();
            //this.BindingContext = new ExerciseProjectDetailPageViewModel();
        }
    }
}