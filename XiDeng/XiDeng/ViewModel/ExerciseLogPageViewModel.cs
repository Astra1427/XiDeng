using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XiDeng.Data;
using System.Linq;
using XiDeng.Command;
using Xamarin.Forms;
using XiDeng.Common;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using XiDeng.Models.ExerciseLogs;

namespace XiDeng.ViewModel
{
    public class ExerciseLogPageViewModel:BaseViewModel
    {
        public static bool IsChanged = false;
        private IEnumerable<ExerciseLogDTO> exerciseLogs;

        public IEnumerable<ExerciseLogDTO> ExerciseLogs
        {
            get { return exerciseLogs; }
            set { exerciseLogs = value; this.RaisePropertyChanged("ExerciseLogs"); }
        }

        private IEnumerable<IGrouping<string, ExerciseLogDTO>> groupExerciseLogs;

        public IEnumerable<IGrouping<string, ExerciseLogDTO>> GroupExerciseLogs
        {
            get { return groupExerciseLogs; }
            set { 
                groupExerciseLogs = value; 
                //changed 
                //scroll to 

                RaisePropertyChanged("GroupExerciseLogs");
            }
        }
        public List<string> GroupDateTime { get; set; }
        private string selectedDateTime;

        public string SelectedDateTime
        {
            get { return selectedDateTime; }
            set { selectedDateTime = value; RaisePropertyChanged("SelectedDateTime"); }
        }


        public DelegateCommand ExerciseProjectDateTappedCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(ExerciseProjectDateTappedFunc) }; }

        private async void ExerciseProjectDateTappedFunc(object obj)
        {
            //obj = ExerciseDatetime
            await this.GoAsync($"ExerciseDateDetailPage?ExerciseDate={obj}");
        }


        public DelegateCommand ExerciseProjectTappedCommand { get => new DelegateCommand {ExecuteAction = new Action<object>(ExerciseProjectTappedFunc) }; }

        private async void ExerciseProjectTappedFunc(object obj)
        {
            //obj = ID
            await this.GoAsync($"ExerciseProjectDetailPage?EID={obj}");
        }

        public DelegateCommand StatisticsCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(async obj=> {
            await this.GoAsync($"StatisticsPage");

        })}; }

        public DelegateCommand DeleteExerciseProjectCommand { get => new DelegateCommand { ExecuteAction = new Action<object>((obj)=> { 
            
        })}; }

        public DelegateCommand SaveCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(async(obj)=> {
            //Save changed
            if (await App.Current.MainPage.DisplayAlert("提示", "确定保存？", "确定", "取消"))
            {
                if (FileHelper.WriteFile(FileHelper.ExerciseLogFile, JsonConvert.SerializeObject(DataCommon.ExerciseLogs)))
                {
                    await App.Current.MainPage.DisplayAlert("提示", "保存成功", "确定");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("提示", "保存失败", "确定");
                }
            }

        })}; }

        /// <summary>
        /// Export Exercise Logs
        /// </summary>
        public DelegateCommand ExportCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(async obj=> {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                //File.WriteAllText("data/XiDengBackUp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", FileHelper.ReadFile(FileHelper.ExerciseLogFile));
                
                bool isSuccess = await CrossFilePicker.Current.SaveFile(new FileData("data", $"XiDengBackUp{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", () =>
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(FileHelper.ReadFile(FileHelper.ExerciseLogFile)));
                }));
                if (isSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("提示", "导出数据成功！\n文件存储在手机根目录下", "好的");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("提示", "导出数据失败！", "好的");

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("提示","导出数据失败！","好的");
            }
        })}; }

        /// <summary>
        /// Import Exercise Logs
        /// </summary>
        public DelegateCommand ImportCommand
        {
            get => new DelegateCommand
            {
                ExecuteAction = new Action<object>(async obj => {
                    try
                    {
                        await App.Current.MainPage.DisplayAlert("提示", "1、请确保导入文件为txt（文本文件）类型!\n2、导入成功后将会覆盖原有的数据，请悉知！", "好的");
                        var rs = await FilePicker.PickAsync(PickOptions.Default);
                        if (rs == null)
                        {
                                return;
                        }
                        string ExerciseLogsTxt = File.ReadAllText(rs.FullPath);
                        var list = JsonConvert.DeserializeObject<List<ExerciseLog>>(ExerciseLogsTxt);
                        
                        if (FileHelper.WriteFile(FileHelper.ExerciseLogFile, JsonConvert.SerializeObject(list)))
                        {
                            await App.Current.MainPage.DisplayAlert("提示", "导入成功！\n重新进入程序即可生效！", "好的");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("提示", "导入失败！", "好的");
                        }
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("提示", "导入失败！\n请检查文件内容是否完整！", "好的");

                    }
                })
            };
        }

        /// <summary>
        /// ctor
        /// </summary>
        public ExerciseLogPageViewModel()
        {
            try
            {
                ExerciseLogs = DataCommon.ExerciseLogs;
                GroupDateTime = new List<string>();
                GroupExerciseLogs = ExerciseLogs.GroupBy(a => a.ExerciseDateTime.ToString("yyyy-MM-dd")).Reverse();
                foreach (var item in GroupExerciseLogs)
                {
                    GroupDateTime.Add(item.Key);

                }
                if (GroupDateTime.Count>0)
                {
                    SelectedDateTime = GroupDateTime[0];
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("提示","获取日志信息失败！","好");
            }
            //foreach (var item in GroupExerciseLogs)
            //{
            //    foreach (var it in item)
            //    {
            //        it.ID
            //    }
            //}

            //Set Icons
            SaveIcon = Utility.GetImage("save_128");
            StatisticsIcon = Utility.GetImage("statistics_128");
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

    }
}
