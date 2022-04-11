using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XiDeng.Data;
using System.Linq;
using XiDeng.Command;
using Xamarin.Forms;
using XiDeng.Models.ExerciseLogs;

namespace XiDeng.ViewModel
{
    class ExerciseDateDetailPageViewModel:BaseViewModel
    {
        private IList<ExerciseLogDTO> exerciseLogs;

        public IList<ExerciseLogDTO> ExerciseLogs
        {
            get { return exerciseLogs; }
            set { exerciseLogs = value; RaisePropertyChanged("ExerciseLogs"); }
        }
        public DelegateCommand ExerciseProjectTappedCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(async(obj)=> {
            await Shell.Current.GoToAsync($"ExerciseProjectDetailPage?EID={obj}");
        })}; }
        /// <summary>
        /// ctor
        /// </summary>
        public ExerciseDateDetailPageViewModel(string date)
        {
            ExerciseLogs = DataCommon.ExerciseLogs.ToList().Where(a=>a.ExerciseDateTime.ToString("yyyy-MM-dd") == date).ToList();
            
        }


    }
}
