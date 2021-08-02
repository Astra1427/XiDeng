﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XiDeng.Data;
using System.Linq;
using System.Web;
using Xamarin.Forms;
using System.Threading.Tasks;
using XiDeng.Command;
using Plugin.SimpleAudioPlayer;
using System.Reflection;
using System.IO;
using XiDeng.Common;
using Xamarin.Essentials;

namespace XiDeng.ViewModel
{
    class TraningPageViewModel:NotificationObject
    {


        #region UI
        private bool isImg1 = true;

        public bool IsImg1
        {
            get { return isImg1; }
            set { isImg1 = value; this.RaisePropertyChanged("IsImg1"); }
        }

        private bool isImg2 = false;

        public bool IsImg2
        {
            get { return isImg2; }
            set { isImg2 = value; this.RaisePropertyChanged("IsImg2"); }
        }



        #endregion

        #region TraningEnd
        private bool isEnd;

        public bool IsEnd
        {
            get { return isEnd; }
            set { isEnd = value; this.RaisePropertyChanged("IsEnd"); }
        }
        public DelegateCommand BackCommand { get => new DelegateCommand {ExecuteAction = new Action<object>(BackFunc) }; }

        private void BackFunc(object obj)
        {
            ClearSound();
            IsBack = true;
            Shell.Current.GoToAsync("..");
        }

        public DelegateCommand AgainCommand { get => new DelegateCommand() { ExecuteAction = new Action<object>(AgainFunc)}; }

        private void AgainFunc(object obj)
        {
            IsEnd = false;
            IsImg1 = true;
            IsImg2 = false;
            CountDownPlay();
        }

        #endregion

        #region  Feeling
        public DelegateCommand WriteFeelingCommand { get => new DelegateCommand { ExecuteAction = new Action<object>( async (obj)=> {
            string feeling = await App.Current.MainPage.DisplayPromptAsync("感想","有什么需要备注的吗？","写完了","不写了","在此处写下你的心得体会",-1,null,"");
            if (string.IsNullOrEmpty(feeling))
            {
                return;
            }
            DataCommon.ExerciseLogs.Last().Feeling += "\n"+feeling;
            DataCommon.ExerciseLogs.Last().DisFeeling = feeling.Length > 9 ? feeling.Substring(0,9) + "……" : feeling;
            FileHelper.WriteFile(FileHelper.ExerciseLogFile, JsonConvert.SerializeObject(DataCommon.ExerciseLogs));
        }) }; }
        #endregion
        #region Stretch Guidance
        public DelegateCommand StretchGuidanceCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(obj=> {
                    Browser.OpenAsync($"https://www.baidu.com/s?wd={DataCommon.Skills.First(a=>a.ID == SkillStyle.SkillID).Name} 拉伸");

        })
        }; }
        #endregion

        public double ExerciseTime { get; set; }

        private SkillStyle skillStyle;

        public SkillStyle SkillStyle
        {
            get { return skillStyle; }
            set { skillStyle = value; this.RaisePropertyChanged("SkillStyle"); }
        }

        private Standard standard;

        public Standard Standard
        {
            get { return standard; }
            set { standard = value; this.RaisePropertyChanged("Standard"); }
        }

        #region Init Sound
        public ISimpleAudioPlayer StartAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer BackAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer OneAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer TwoAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer ThreeAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer FinishAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public ISimpleAudioPlayer RecoveryAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        //public ISimpleAudioPlayer TenSecondsCoundDownAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

        public void InitSound()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream startMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.start.mp3");
            Stream backMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.bengbengca.mp3");
            Stream oneMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.one.mp3");
            Stream twoMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.two.mp3");
            Stream threeMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.three.mp3");
            Stream finishMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.finish.mp3");
            Stream recoveryhMp3 = assembly.GetManifestResourceStream("XiDeng."+ "Resources.recovery.mp3");
            //Stream TenSecondsCoundDownMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.countdown10seconds.mp3");



            StartAudio.Load(startMp3);
            BackAudio.Load(backMp3);
            BackAudio.Loop = true;
            OneAudio.Load(oneMp3);
            TwoAudio.Load(twoMp3);
            ThreeAudio.Load(threeMp3);
            FinishAudio.Load(finishMp3);
            RecoveryAudio.Load(recoveryhMp3);
            //TenSecondsCoundDownAudio.Load(TenSecondsCoundDownMp3);


            //set Volume
            BackAudio.Volume = Config.BackAudioVolume;
            StartAudio.Volume = Config.PersonAudioVolume;
            OneAudio.Volume = Config.PersonAudioVolume;
            TwoAudio.Volume = Config.PersonAudioVolume;
            ThreeAudio.Volume = Config.PersonAudioVolume;
            FinishAudio.Volume = Config.PersonAudioVolume;
            RecoveryAudio.Volume = Config.PersonAudioVolume;
            //TenSecondsCoundDownAudio.Volume = Config.PersonAudioVolume;
        }


        public async void ContinueSound() {
            BackAudio.Play();
            await StartTraning();
        }


        public void StopSound()
        {
            if (BackAudio.IsPlaying)
            {
                BackAudio.Pause();
            }
        }

        public void ClearSound()
        {
            StartAudio.Dispose();
            BackAudio.Stop();
            BackAudio.Dispose();
            OneAudio.Dispose();
            TwoAudio.Dispose();
            FinishAudio.Dispose();
            RecoveryAudio.Dispose();
        }
        private bool isStop;

        public bool IsStop
        {
            get { return isStop; }
            set { isStop = value;RaisePropertyChanged("IsStop"); }
        }


        public DelegateCommand StopContinueCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(StopContinueFunc) }; }

        private async void StopContinueFunc(object obj)
        {
            if (!IsStop)
            {
                IsStop = true;
                IsStart = false;
                IsCountDown = false;
                StopSound();
            }
            else
            {
                IsStop = false;
                IsStart = false;
                IsCountDown = false;
                await CountDown(Config.StartContinueSecond);
                if (IsStop)
                {
                    return;
                }
                await StartTraning();

            }
        }
        public DelegateCommand ContinueCommand { get => new DelegateCommand {ExecuteAction = new Action<object>(ContinueFunc) }; }

        private async void ContinueFunc(object obj)
        {
            if (!IsStop)
            {
                return;
            }
            IsStop = false;
            await CountDown(Config.StartContinueSecond);
            ContinueSound();
        }

        #endregion

        private ConfigModel config;

        public ConfigModel Config
        {
            get { return config; }
            set { config = value; }
        }

        public int NumberSecond { get; set; }
        public int UpNumberSecond { get; set; }
        public int DownNumberSecond { get; set; }
        public bool IsBack = false;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="SkillID"></param>
        /// <param name="StyleID"></param>
        /// <param name="StandardJson"></param>
        public TraningPageViewModel(string SkillID,string StyleID,string StandardJson)
        {
            try
            {
                Standard = JsonConvert.DeserializeObject<Standard>(Uri.UnescapeDataString(StandardJson));
                Config = JsonConvert.DeserializeObject<ConfigModel>(FileHelper.ReadFile(FileHelper.SettingFile));
                SleepCountDownNumber = Config.SleepSecond;

                if (Standard.TraningType == 1)
                {

                    DownNumberSecond = 1000;
                    UpNumberSecond = 1000;
                }
                else
                {
                    NumberSecond = Config.NumberSecond;
                    DownNumberSecond = Config.DownNumberSecond;
                    UpNumberSecond = Config.UpNumberSecond;
                }
            }
            catch (Exception ex)
            { 
                App.Current.MainPage.DisplayAlert("错误", "读取设置信息失败，请重新设置", "好的");
                BackFunc(null);
                return;
            }
            int sid = int.Parse(SkillID);
            int stid = int.Parse(StyleID);
            SkillStyle = DataCommon.Skills.Where(a=>a.ID == sid).FirstOrDefault().Styles.Where(a=>a.ID == stid).FirstOrDefault();
            InitSound();
            CountDownPlay();
        }

        private int currentGroupNumber;

        public int CurrentGroupNumber
        {
            get { return currentGroupNumber; }
            set { currentGroupNumber = value;this.RaisePropertyChanged("CurrentGroupNumber"); }
        }


        private int currentNumber;

        public int CurrentNumber
        {
            get { return currentNumber; }
            set { currentNumber = value; this.RaisePropertyChanged("CurrentNumber"); }
        }

        private bool isSleep;

        public bool IsSleep
        {
            get { return isSleep; }
            set { isSleep = value; this.RaisePropertyChanged("IsSleep"); }
        }


        /// <summary>
        /// Start Is Only One
        /// </summary>
        public bool IsStart = false;    
        /// <summary>
        /// Start Traning
        /// </summary>
        public async Task StartTraning()
        {
            if (IsStart)
            {
                return;
            }
            IsStart = true;
            for (int i = 0; i < Standard.GroupsNumber; i++)
            {

                while (true)
                {
                    if (IsStop || IsBack)
                    {
                        return ;
                    }
                    //1
                    //Play 1
                    CurrentNumber++;
                    if (Standard.TraningType == 1)
                    {
                        //count down
                        await TextToSpeech.SpeakAsync(CurrentNumber.ToString(),new SpeechOptions() { Volume = (float)Config.PersonAudioVolume});
                    }
                    else
                    {
                        OneAudio.Play();
                    }
                    IsImg1 = !IsImg1;
                    IsImg2 = !IsImg2;
                    await Task.Delay(DownNumberSecond);
                    //record Exercise Time
                    ExerciseTime += DownNumberSecond / 1000;
                    
                    //2
                    if (Standard.TraningType == 1)
                    {
                        //count down
                        CurrentNumber++;
                        await TextToSpeech.SpeakAsync(CurrentNumber.ToString(),new SpeechOptions() { Volume = (float)Config.PersonAudioVolume});
                    }
                    else
                    {
                        TwoAudio.Play();
                    }
                    IsImg1 = !IsImg1;
                    IsImg2 = !IsImg2;
                    await Task.Delay(UpNumberSecond);

                    //record Exercise Time
                    ExerciseTime += UpNumberSecond / 1000;
                    //Is Complete
                    if (CurrentNumber >= Standard.Number)
                    {
                        CurrentNumber = 0;
                        break;
                    }
                }

                if (Standard.IsSingle == 1)
                {
                    IsImg1 = true;
                    IsImg2 = false;
                    await TextToSpeech.SpeakAsync("换边",new SpeechOptions() { Volume = 1});
                    await CountDown(Config.StartContinueSecond);
                    ExerciseTime += Config.StartContinueSecond;

                    CurrentNumber = 0;
                    while (true)
                    {
                        if (IsStop || IsBack)
                        {
                            return;
                        }
                        //1
                        //Play 1
                        CurrentNumber++;
                        OneAudio.Play();
                        IsImg1 = !IsImg1;
                        IsImg2 = !IsImg2;
                        await Task.Delay(DownNumberSecond);
                        //record Exercise Time
                        ExerciseTime += DownNumberSecond / 1000;
                        //2
                        TwoAudio.Play();
                        IsImg1 = !IsImg1;
                        IsImg2 = !IsImg2;
                        await Task.Delay(UpNumberSecond);
                        //record Exercise Time
                        ExerciseTime += UpNumberSecond / 1000;
                        //Is Complete
                        if (CurrentNumber >= Standard.Number)
                        {
                            CurrentNumber = 0;
                            break;
                        }
                    }
                }

                CurrentGroupNumber++;
                if (CurrentGroupNumber == Standard.GroupsNumber)
                {
                    //Compeleted traning
                    break;
                }

                RecoveryAudio.Play();
                IsImg1 = true;
                IsImg2 = false;
                await Sleep(SleepCountDownNumber);
                
            }
            //traning end
            FinishAudio.Play();

            #region record Exercise 
            RecordExercise();

            #endregion

            IsSleep = false;
            IsEnd = true;
            CurrentNumber = 0;
            CurrentGroupNumber = 0;
            IsStart = false;
        }

        /// <summary>
        /// record Exercise
        /// </summary>
        private void RecordExercise()
        {
            if (IsBack)
            {
                return;
            }
            string SkillName = "???";
            try
            {
                SkillName = DataCommon.Skills.Single(a => a.ID == this.SkillStyle.SkillID).Name;
            }
            catch (Exception ex)
            {

            }
            DataCommon.ExerciseLogs.Add(new ExerciseLog()
            {
                ID = DataCommon.ExerciseLogs.LastOrDefault() == null ? 1 : DataCommon.ExerciseLogs.LastOrDefault().ID + 1,
                SkillID = this.SkillStyle.SkillID,
                SkillName = SkillName,
                StyleID = this.SkillStyle.ID,
                StyleName = this.SkillStyle.Name,
                ExerciseDateTime = DateTime.Now,
                ExerciseTime = this.ExerciseTime,
                ExerciseStandard = this.Standard,
                Feeling = ""
            });
            FileHelper.WriteFile(FileHelper.ExerciseLogFile, JsonConvert.SerializeObject(DataCommon.ExerciseLogs));
        }


        private int countDownNumber;

        public int CountDownNumber
        {
            get { return countDownNumber; }
            set { countDownNumber = value;this.RaisePropertyChanged("CountDownNumber"); }
        }


        private int sleepCountDownNumber;

        public int SleepCountDownNumber
        {
            get { return sleepCountDownNumber; }
            set { sleepCountDownNumber = value; this.RaisePropertyChanged("SleepCountDownNumber"); }
        }

        /// <summary>
        /// Sleep second default : 60
        /// </summary>
        public async Task Sleep(int sleepSecond )
        {
            IsSleep = true;
            CountDownNumber = sleepSecond;
            while (IsSleep)
            {
                //If Coutdowning User Tapped Stop
                if (IsStop == true)
                {
                    IsSleep = false;
                    return;
                }

                await Task.Delay(1000);
                ExerciseTime++;
                sleepSecond--;
                CountDownNumber--;
                if (sleepSecond <= 3)
                {
                    //Sound
                    if (sleepSecond == 3)
                    {
                        ThreeAudio.Play();
                    }
                    if (sleepSecond == 2)
                    {
                        TwoAudio.Play();
                    }
                    if (sleepSecond == 1)
                    {
                        OneAudio.Play();
                    }
                }
                //If Coutdowning User Tapped Stop
                if (IsStop == true)
                {
                    IsSleep = false;
                    return;
                }

                if (sleepSecond <= 0)
                {
                    StartAudio.Play();
                    await Task.Delay(1000);
                    IsSleep = false;
                }
            }
        }

        public async void CountDownPlay()
        {
            //IsSleep = true;
            //CountDownNumber = 3;
            //while (IsSleep)
            //{
            //    if (CountDownNumber == 3)
            //    {
            //        ThreeAudio.Play();
            //    }
            //    if (CountDownNumber == 2)
            //    {
            //        TwoAudio.Play();
            //    }
            //    if (CountDownNumber == 1)
            //    {
            //        OneAudio.Play();
            //    }
            //    await Task.Delay(1000);

            //    CountDownNumber--;
            //    if (CountDownNumber <= 0)
            //    {
            //        IsSleep = false;
            //    }
            //}
            //StartAudio.Play();
            //await Task.Delay(1000);
            //BackAudio.Play();
            await CountDown(Config.StartContinueSecond);
            await StartTraning();


        }
        /// <summary>
        /// Count Donw Is Only one
        /// </summary>
        public bool IsCountDown = false;    
        public async Task CountDown(int second = 3)
        {
            if (IsCountDown )
            {
                return;
            }
            IsCountDown = true;

            IsSleep = true;
            CountDownNumber = second;
            while (IsSleep)
            {
                //If Coutdowning User Tapped Stop
                if (IsStop == true)
                {
                    IsSleep = false;
                    return;
                }

                if (second == 3)
                {
                    ThreeAudio.Play();
                }
                if (second == 2)
                {
                    TwoAudio.Play();
                }
                if (second == 1)
                {
                    OneAudio.Play();
                }
                await Task.Delay(1000);

                second--;
                CountDownNumber--;
                if (second <= 0)
                {
                    IsSleep = false;
                }

                //If Coutdowning User Tapped Stop
                if (IsStop == true)
                {
                    IsSleep = false;
                    return;
                }

            }
            StartAudio.Play();
            await Task.Delay(1000);
            if (!BackAudio.IsPlaying)
            {
                BackAudio.Play();
            }
            IsCountDown = false;
        }

    }
}