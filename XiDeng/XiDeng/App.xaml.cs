using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Views.AccountViews;
using Plugin.FilePicker;
using XiDeng.Models.SkillModels;
using System.Text;
using System.IO;
using XiDeng.Models.AccountModels;
using XiDeng.Models.ExerciseLogs;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;

namespace XiDeng
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            VersionTracking.Track();
            

            InitSettingFile();
            //InitData();
            LoadAccountInfo();

            MainPage = new ShellApp();
            //MainPage = new MainPage();
            //MainPage = new Views.ThemeSettingViews.AddThemePage();
        }
        
        public async Task<bool> LoadNewVersionData()
        {
            bool IsLogged = Utility.LoggedAccount != null && !Utility.LoggedAccount.JwtToken.IsEmpty();
            if (IsLogged)
            {
                SkillDataCommon.Skills = (await App.Database.GetAllAsync<SkillDTO>(x => x.OrderNumber <= 6 || x.OwnerId == Utility.LoggedAccount.Id)).ToObservableCollection();
            }
            else
            {
                SkillDataCommon.Skills = (await App.Database.GetAllAsync<SkillDTO>(x => x.OrderNumber <= 6)).ToObservableCollection();
            }

            if (SkillDataCommon.Skills == null || SkillDataCommon.Skills.Count < 6)
            {
                SkillDataCommon.Skills = Encoding.UTF8.GetString(XiDeng.Properties.Resources.XiDengSkillsDataJson).To<ObservableCollection<SkillDTO>>();
            }
            else
            {
                await SkillDataCommon.Skills.ForEachAsync(async x=> {
                    x.SkillStyles = await App.Database.GetAllAsync<SkillStyleDTO>(st=>st.SkillId == x.Id);
                });
            }


            return SkillDataCommon.Skills != null;
        }

        public void InitFiles()
        {
            if (!FileHelper.IsExist(FileHelper.VideoUriFile))
            {
                //var skills2 = DataCommon.Skills;
                var skills2 = SkillDataCommon.Skills;
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

        public void InitSettingFile()
        {
            if (!FileHelper.IsExist(FileHelper.SettingFile) || VersionTracking.IsFirstLaunchForCurrentVersion)
            {
                string initContent = JsonConvert.SerializeObject(new ConfigModel { 
                    NumberSecond = 1200, 
                    SleepSecond = 45, 
                    IsRespiratoryRhythm = 0,
                    StartContinueSecond = 3,
                    BackAudioVolume = 1,
                    PersonAudioVolume = 1, 
                    VersionNumber = VersionTracking.CurrentVersion, 
                    UpNumberSecond = 2000, 
                    DownNumberSecond = 3000 ,
                    IsOffline = false
                });
                FileHelper.WriteFile(FileHelper.SettingFile, initContent);
            }
        }

        public void InitData()
        {
            #region Init Skill Video

            //InitSkillVideo();
            #endregion

            #region Init Exercises
            try
            {
                string exerciseJson = FileHelper.ReadFile(FileHelper.ExerciseLogFile);
                if (exerciseJson == null)
                {
                    DataCommon.ExerciseLogs = new List<ExerciseLogDTO>();
                    return;
                }
                DataCommon.ExerciseLogs = JsonConvert.DeserializeObject<List<ExerciseLogDTO>>(exerciseJson);
            }
            catch (Exception ex)
            {
                //Load Feiled
                App.Current.MainPage.DisplayAlert("Error","加载锻炼日志失败！\n请检查日志文件是否损坏或者缺失","OK");
                DataCommon.ExerciseLogs = new List<ExerciseLogDTO>();
            }
            #endregion
        }

        public void LoadAccountInfo()
        {
            
            if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken == null)
            {
                //try load login info
                string loginInfoJson = FileHelper.ReadFile(FileHelper.LoginInfoFile);
                Utility.LoggedAccount = loginInfoJson.To<AccountDTO>();
                Utility.LoggedAccount = Utility.LoggedAccount ?? new AccountDTO();
            }
        }

        //public void InitSkillVideo()
        //{
        //    try
        //    {
        //        string json = FileHelper.ReadFile(FileHelper.VideoUriFile);
        //        if (json == null || json == "")
        //        {
        //            return;
        //        }
        //        var skills = JsonConvert.DeserializeObject<List<Skill>>(json);
        //        for (int i = 0; i < skills.Count; i++)
        //        {
        //            for (int j = 0; j < skills[i].Styles.Count; j++)
        //            {
        //                if (skills[i].Styles[j].LocalVideoUri == null || skills[i].Styles[j].LocalVideoUri == "")
        //                {
        //                    DataCommon.Skills[i].Styles[j].DisplayVideoUri = DataCommon.Skills[i].Styles[j].VideoUri;
        //                }
        //                else
        //                {
        //                    DataCommon.Skills[i].Styles[j].DisplayVideoUri = skills[i].Styles[j].LocalVideoUri;
        //                }
        //                DataCommon.Skills[i].Styles[j].LocalVideoUri = skills[i].Styles[j].LocalVideoUri;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected override async void OnStart()
        {
            try
            {
                if (VersionTracking.IsFirstLaunchEver)
                {
                    await this.Message("这是你第一次运行熄灯App，请联网并登录一个账号，以便熄灯App从云端下载必备的数据！");
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                }

                

                await VersionHelper.CheckUpdate(false);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }



        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private static SQLiteDatabase database;
        public static SQLiteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"xdDB.db3"));
                }
                return database;
            }
        }

        private static ConfigModel config;

        public static ConfigModel Config
        {
            get {
                if (config == null)
                {
                    config = JsonConvert.DeserializeObject<ConfigModel>(FileHelper.ReadFile(FileHelper.SettingFile));
                }
                return config;
            }
            set { config = value; }
        }

        

    }
}
