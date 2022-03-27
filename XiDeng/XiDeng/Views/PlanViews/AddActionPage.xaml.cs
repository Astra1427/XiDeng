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
    [QueryProperty("PlanID", "PlanID")]
    [QueryProperty("DayNumber", "DayNumber")]
    [QueryProperty("ByWeek", "ByWeek")]
    public partial class AddActionPage : ContentPage
    {
        private int dayNumber;

        public string DayNumber
        {
            set
            {
                int day = 0;
                int.TryParse(value, out day);
                dayNumber = day;
            }
        }
        private Guid planID;

        public string PlanID
        {
            set { planID = Guid.Parse(value); }
        }
        private bool byWeek;

        public string ByWeek
        {
            set
            {
                bool b = false;
                bool.TryParse(value, out b);
                byWeek = b;
                dwConvert.ByWeek = b;
            }
        }
        public AddActionPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = new AddActionPageViewModel(planID, dayNumber, byWeek);

            //ItemDisplayBinding="{Binding .,Converter={StaticResource DayWeekConverter},ConverterParameter=}"
            //pDayList.ItemDisplayBinding = new Binding(".", converter: new DayWeekConverter(), converterParameter: byWeek);
            //dwConvert.ByWeek = byWeek;

        }
    }
}