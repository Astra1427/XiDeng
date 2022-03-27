using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Data;
using System.Linq;
using XiDeng.Common;
using Newtonsoft.Json;

namespace XiDeng.ViewModel
{
    class ExerciseProjectDetailPageViewModel:BaseViewModel
    {


        private ExerciseLog exercise;

        public ExerciseLog Exercise
        {
            get { return exercise; }
            set { exercise = value;RaisePropertyChanged("Exercise"); }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged("Title"); }
        }
        private string feeling;

        public string Feeling
        {
            get { return feeling; }
            set { feeling = value; RaisePropertyChanged("Feeling"); }
        }


        public DelegateCommand SaveFeelingCommand { get => new DelegateCommand { ExecuteAction = new Action<object>((obj)=> {
            try
            {
                this.Exercise.Feeling = Feeling;
                this.Exercise.DisFeeling = Feeling.Length > 9 ? Feeling.Substring(0, 9) + "……" : Feeling;
                ExerciseLogPageViewModel.IsChanged = true;

                App.Current.MainPage.DisplayAlert("提示","成功！","好");
                FileHelper.WriteFile(FileHelper.ExerciseLogFile,JsonConvert.SerializeObject(DataCommon.ExerciseLogs));
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("提示","更改失败！","好");
            }
        }) }; }
        /// <summary>
        /// ctor    
        /// </summary>
        public ExerciseProjectDetailPageViewModel(Guid eid)
        {
            Exercise = DataCommon.ExerciseLogs.First(a => a.Id == eid);
            Title = Exercise.SkillName + "-" + Exercise.ToString();
            Feeling = Exercise.Feeling;
        }

    }
}
