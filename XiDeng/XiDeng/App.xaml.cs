using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.Data;

namespace XiDeng
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DataCommon.InitDatas();
            InitFile();
            InitData();
            MainPage = new ShellApp();
        }


        public void InitFile()
        {
            VersionTracking.Track();
            if (!FileHelper.IsExist(FileHelper.SettingFile) || VersionTracking.IsFirstLaunchForCurrentVersion)
            {
                string initContent = JsonConvert.SerializeObject(new ConfigModel { NumberSecond = 1200, SleepSecond = 45, IsRespiratoryRhythm = 0, StartContinueSecond = 3 ,BackAudioVolume = 1,PersonAudioVolume=1,VersionNumber = VersionTracking.CurrentVersion,UpNumberSecond = 2000,DownNumberSecond = 3000});
                FileHelper.WriteFile(FileHelper.SettingFile, initContent);
            }
            if (!FileHelper.IsExist(FileHelper.VideoUriFile))
            {
                var skills2 = DataCommon.Skills;
                string initContent = JsonConvert.SerializeObject(skills2);
                FileHelper.WriteFile(FileHelper.VideoUriFile, initContent);
            }

            if (!FileHelper.IsExist(FileHelper.ExerciseLogFile))
            {
                var exercise = new List<ExerciseLog>();
                string initContent = JsonConvert.SerializeObject(exercise);
                FileHelper.WriteFile(FileHelper.ExerciseLogFile, initContent);
            }

        }
        public void InitData()
        {
            #region Init Skill Video

            InitSkillVideo();
            #endregion

            #region Init Exercises
            try
            {
                string exerciseJson = FileHelper.ReadFile(FileHelper.ExerciseLogFile);
                if (exerciseJson == null)
                {
                    return;
                }
                DataCommon.ExerciseLogs = JsonConvert.DeserializeObject<List<ExerciseLog>>(exerciseJson);
            }
            catch (Exception ex)
            {
                //Load Feiled
                App.Current.MainPage.DisplayAlert("Error","加载锻炼日志失败！\n请检查日志文件是否损坏或者缺失","OK");
                DataCommon.ExerciseLogs = new List<ExerciseLog>();
            }
            #endregion
        }

        public void InitSkillVideo()
        {
            try
            {
                string json = FileHelper.ReadFile(FileHelper.VideoUriFile);
                if (json == null || json == "")
                {
                    return;
                }
                var skills = JsonConvert.DeserializeObject<List<Skill>>(json);
                for (int i = 0; i < skills.Count; i++)
                {
                    for (int j = 0; j < skills[i].Styles.Count; j++)
                    {
                        if (skills[i].Styles[j].LocalVideoUri == null || skills[i].Styles[j].LocalVideoUri == "")
                        {
                            DataCommon.Skills[i].Styles[j].DisplayVideoUri = DataCommon.Skills[i].Styles[j].VideoUri;
                        }
                        else
                        {
                            DataCommon.Skills[i].Styles[j].DisplayVideoUri = skills[i].Styles[j].LocalVideoUri;
                        }
                        DataCommon.Skills[i].Styles[j].LocalVideoUri = skills[i].Styles[j].LocalVideoUri;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
