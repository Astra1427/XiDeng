using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Data;
using XiDeng.ViewModel;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseLogPage : ContentPage
    {
        public ExerciseLogPageViewModel ViewModel { get; set; }
        public List<IGrouping<string,Data.ExerciseLog>> GroupExerciseLogList { get; set; }
        public ExerciseLogPage()
        {
            InitializeComponent();
            //ViewModel = new ExerciseLogPageViewModel();

            ViewModel = new ExerciseLogPageViewModel();
            BindingContext = ViewModel;
            GroupExerciseLogList = ViewModel.GroupExerciseLogs.ToList();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ExerciseLogPageViewModel.IsChanged)
            {
                ViewModel.ExerciseLogs = DataCommon.ExerciseLogs;
                ViewModel.GroupDateTime = new List<string>();
                ViewModel.GroupExerciseLogs = ViewModel.ExerciseLogs.GroupBy(a => a.ExerciseDateTime.ToString("yyyy-MM-dd")).Reverse();
                foreach (var item in ViewModel.GroupExerciseLogs)
                {
                    ViewModel.GroupDateTime.Add(item.Key);

                }
                ExerciseLogPageViewModel.IsChanged = false;
            }
        }

        [Obsolete]
        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            try
            {


                pDateTime.Title = GroupExerciseLogList[e.FirstVisibleItemIndex].Key;
            }
            catch (Exception ex)
            {

            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //int index = GroupExerciseLogList.IndexOf(GroupExerciseLogList.Where(a => a.Key == ViewModel.SelectedDateTime).FirstOrDefault());
                var item = GroupExerciseLogList.Where(a => a.Key == ViewModel.SelectedDateTime).FirstOrDefault().FirstOrDefault();
                cvExerciseLogs.ScrollTo(item,pDateTime.SelectedItem,ScrollToPosition.Start);
            }
            catch (Exception ex)
            {
                //this.DisplayAlert("Tips","没有找到当天的锻炼信息","OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("a","a","a");
        }
    }
}