using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Data;
using System.Linq;
using XiDeng.Common;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.ObjectModel;
using XiDeng.Models.ExerciseLogs;

namespace XiDeng.ViewModel
{
    class ExerciseProjectDetailPageViewModel:BaseViewModel
    {


        private ExerciseLogDTO exercise;

        public ExerciseLogDTO Exercise
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



        /// <summary>
        /// ctor    
        /// </summary>
        public ExerciseProjectDetailPageViewModel(Guid eid)
        {
            Exercise = DataCommon.ExerciseLogs.First(a => a.Id == eid);
            Title = Exercise.Style.Name + "-" + Exercise.ToString();
            Feeling = Exercise.Feeling;

            SaveFeelingCommand = new AsyncCommand(async ()=> {
                try
                {
                    this.Exercise.Feeling = Feeling;
                    this.Exercise.DisFeeling = Feeling.Length > 9 ? Feeling.Substring(0, 9) + "……" : Feeling;
                    this.Exercise.Updated = false;
                    ExerciseLogPageViewModel.IsChanged = true;

                    await App.Current.MainPage.DisplayAlert("提示", "成功！", "好");
                    //FileHelper.WriteFile(FileHelper.ExerciseLogFile,JsonConvert.SerializeObject(DataCommon.ExerciseLogs));
                    await App.Database.SaveAsync(this.Exercise);
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("提示", "更改失败！", "好");
                }
            });
        }
        public AsyncCommand SaveFeelingCommand { get; set; }

    }
}
