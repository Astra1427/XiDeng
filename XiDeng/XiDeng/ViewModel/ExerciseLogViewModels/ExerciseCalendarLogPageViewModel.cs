using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Models.ExerciseLogs;
using XiDeng.Views;

namespace XiDeng.ViewModel.ExerciseLogViewModels
{
    public class ExerciseCalendarLogPageViewModel:BaseViewModel
    {
        

        private EventCollection logEvents = new EventCollection();
        public EventCollection LogEvents
        {
            get { return logEvents; }
            set
            {
                logEvents = value;
                this.RaisePropertyChanged(nameof(LogEvents));
            }
        }



        private List<ExerciseLogDTO> exerciseLogs;

        public List<ExerciseLogDTO> ExerciseLogs
        {
            get { return exerciseLogs; }
            set { exerciseLogs = value; this.RaisePropertyChanged("ExerciseLogs"); }
        }

        private IEnumerable<IGrouping<DateTime, ExerciseLogDTO>> groupExerciseLogs;

        public IEnumerable<IGrouping<DateTime, ExerciseLogDTO>> GroupExerciseLogs
        {
            get { return groupExerciseLogs; }
            set
            {
                groupExerciseLogs = value;
                //changed 
                //scroll to 

                RaisePropertyChanged("GroupExerciseLogs");
            }
        }
        private CultureInfo localCulture;
        public CultureInfo LocalCulture
        {
            get { return localCulture; }
            set
            {
                localCulture = value;
                this.RaisePropertyChanged(nameof(LocalCulture));
            }
        }

        public ExerciseCalendarLogPageViewModel()
        {
            //Set Icons
            SaveIcon = Utility.GetImage("save_128");
            StatisticsIcon = Utility.GetImage("chart_4_240");
            LocalCulture = CultureInfo.CurrentCulture;

            AppearingCommand = new AsyncCommand<object>(async obj=> {

                if (LogEvents.LongCount() != 0)
                    return;

                await this.Try<object>(async o =>
                {
                    await Task.Delay(200);
                    if (DataCommon.ExerciseLogs == null || DataCommon.ExerciseLogs.Count == 0)
                        await DataCommon.LoadLog();

                    ExerciseLogs = DataCommon.ExerciseLogs;

                    GroupExerciseLogs = exerciseLogs.GroupBy(x => x.ExerciseDateTime.Date);
                    foreach (var item in GroupExerciseLogs)
                    {
                        LogEvents.Add(item.Key, item.ToList());
                    }
                    this.RaisePropertyChanged(nameof(LogEvents));
                }, null, true);

            });
            GotoExerciseProjectDetailCommand = new AsyncCommand<object>(async obj=> {
                if (obj is Guid EID)
                {
                    await Shell.Current.GoToAsync(nameof(ExerciseProjectDetailPage)+$"?EID={EID}");
                }
            });
            ExportExerciseLogCommand = new AsyncCommand(async () => {
                await this.Try<object>(async o=> {
                    await Task.Delay(200);
                    var exerciseLogs = await App.Database.GetAllAsync<ExerciseLogDTO>(x => x.AccountId == Utility.LoggedAccount.Id);
                    if (exerciseLogs == null)
                    {
                        await this.Message("目前没有锻炼记录！");
                        return;
                    }
                    string json = exerciseLogs.ToJson();
                    FileHelper.WriteFile(FileHelper.ExerciseLogFile, json);
                },null,true);

                if (!FileHelper.IsExist(FileHelper.ExerciseLogFile))
                {
                    await this.Message("目前没有锻炼记录！");
                    return;
                }

                await Share.RequestAsync(new ShareFileRequest(new ShareFile(FileHelper.BasePath + "/" + FileHelper.ExerciseLogFile + ".txt")));

            });
            GotoStatisticsPageCommand = new AsyncCommand(async delegate { 
                await this.GoAsync($"StatisticsPage");
            });
        }

        

        #region Icons
        private ImageSource saveIcon;

        public ImageSource SaveIcon
        {
            get { return saveIcon; }
            set { saveIcon = value; RaisePropertyChanged("SaveIcon"); }
        }

        private ImageSource statisticsIcon;

        public ImageSource StatisticsIcon
        {
            get { return statisticsIcon; }
            set { statisticsIcon = value; RaisePropertyChanged("StatisticsIcon"); }
        }


        #endregion

        public new AsyncCommand<object> AppearingCommand { get; set; }
        public AsyncCommand<object> GotoExerciseProjectDetailCommand { get; set; }
        public AsyncCommand ExportExerciseLogCommand { get; set; }
        public AsyncCommand GotoStatisticsPageCommand { get; set; }

    }
}
