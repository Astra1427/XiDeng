using Newtonsoft.Json;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Models.ExerciseLogs;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class TraningPlanPageViewModel:BaseViewModel
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

        private int reallySecond;

        public int ReallySecond
        {
            get { return reallySecond; }
            set { reallySecond = value; RaisePropertyChanged(nameof(ReallySecond)); }
        }

        private int currentGroupNumber;

        public int CurrentGroupNumber
        {
            get { return currentGroupNumber; }
            set { currentGroupNumber = value; this.RaisePropertyChanged("CurrentGroupNumber"); }
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

        private bool isShowNavBar = true;
        public bool IsShowNavBar
        {
            get { return isShowNavBar; }
            set
            {
                isShowNavBar = value;
                this.RaisePropertyChanged(nameof(IsShowNavBar));
            }
        }

        private bool isStop;

        public bool IsStop
        {
            get { return isStop; }
            set { isStop = value; RaisePropertyChanged("IsStop"); IsShowNavBar = value; }
        }
        private int countDownNumber;

        public int CountDownNumber
        {
            get { return countDownNumber; }
            set { countDownNumber = value; this.RaisePropertyChanged("CountDownNumber"); }
        }
        private int sleepCountDownNumber;

        public int SleepCountDownNumber
        {
            get { return sleepCountDownNumber; }
            set { sleepCountDownNumber = value; this.RaisePropertyChanged("SleepCountDownNumber"); }
        }
        /// <summary>
        /// Start Is Only One
        /// </summary>
        public bool IsStart = false;
        /// <summary>
        /// Count Down Is Only one
        /// </summary>
        public bool IsCountDown = false;
        public bool IsBack = false;

        #endregion
        #region Exercise Config Data

        private ConfigModel config;

        public ConfigModel Config
        {
            get { return config; }
            set { config = value; }
        }
        public int NumberSecond { get; set; }
        public int UpNumberSecond { get; set; }
        public int DownNumberSecond { get; set; }
        public SpeechOptions NumberSpeechOptions { get; set; }

        #endregion

        #region ExercisePlan Data

        private AccountRunningPlanDTO runningPlan;
        public AccountRunningPlanDTO RunningPlan
        {
            get { return runningPlan; }
            set
            {
                runningPlan = value;
                this.RaisePropertyChanged(nameof(RunningPlan));
            }
        }

        private ExercisePlanDTO plan;
        public ExercisePlanDTO Plan
        {
            get { return plan; }
            set
            {
                plan = value;
                this.RaisePropertyChanged(nameof(Plan));
            }
        }

        private int toDayNumber;
        public int ToDayNumber
        {
            get { return toDayNumber; }
            set
            {
                toDayNumber = value;
                this.RaisePropertyChanged(nameof(ToDayNumber));
            }
        }

        private PlanEachDayDTO currentAction;
        public PlanEachDayDTO CurrentAction
        {
            get { return currentAction; }
            set
            {
                currentAction = value;

                LoadConfigForAction();
                CurrentGroupNumber = 0;
                CurrentNumber = 0;
                this.RaisePropertyChanged(nameof(CurrentAction));
            }
        }

        #endregion

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
            try
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                Stream startMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.start.mp3");
                Stream backMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.bengbengca.mp3");
                Stream oneMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.one.mp3");
                Stream twoMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.two.mp3");
                Stream threeMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.three.mp3");
                Stream finishMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.finish.mp3");
                Stream recoveryhMp3 = assembly.GetManifestResourceStream("XiDeng." + "Resources.recovery.mp3");
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

                startMp3.Dispose();
                backMp3.Dispose();
                oneMp3.Dispose();
                twoMp3.Dispose();
                threeMp3.Dispose();
                finishMp3.Dispose();
                recoveryhMp3.Dispose();
            }
            catch (Exception)
            {

            }
        }


        public async void ContinueSound()
        {
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






        #endregion

        private CancellationTokenSource stopTaskCancelTokenSource;
        public CancellationTokenSource StopTaskCancelTokenSource
        {
            get { return stopTaskCancelTokenSource; }
            set
            {
                stopTaskCancelTokenSource = value;
                this.RaisePropertyChanged(nameof(StopTaskCancelTokenSource));
            }
        }
        private string planTitle;
        public string PlanTitle
        {
            get { return planTitle; }
            set
            {
                planTitle = value;
                this.RaisePropertyChanged(nameof(PlanTitle));
            }
        }



        public TraningPlanPageViewModel(Guid runningPlanId)
        {
            AgainCommand = new Command<object>(async obj=> {
                LoadConfigForAction();
                CurrentAction = Plan.PlanEachDays[0];
                CurrentGroupNumber = 0;
                CurrentNumber = 0;
                await CountDownPlay();
            });

            InitCommand = new Command<object>(async obj=> {
                base.Appearing(null);

                #region init config
                Config = JsonConvert.DeserializeObject<ConfigModel>(FileHelper.ReadFile(FileHelper.SettingFile));
                SleepCountDownNumber = Config.SleepSecond;
                NumberSpeechOptions = new SpeechOptions() { Volume = (float)Config.PersonAudioVolume };
                #endregion
                InitSound();

                #region Check data and Init Data
                this.RunningPlan = await App.Database.GetAsync<AccountRunningPlanDTO>(x => x.Id == runningPlanId);
                if (RunningPlan == null)
                {
                    await this.Message("数据丢失，你可以选择重新开始该计划或者同步数据。");
                    this.BackCommand?.Execute(null);
                    return;
                }
                Plan = await App.Database.GetAsync<ExercisePlanDTO>(x => x.Id == RunningPlan.PlanId);
                if (Plan == null)
                {
                    await this.Message("数据丢失，你可以选择重新开始该计划或者同步数据。");
                    this.BackCommand?.Execute(null);
                    return;
                }
                if (RunningPlan.StartTime == null)
                {
                    await this.Message("该计划尚未开始。");
                    this.BackCommand?.Execute(null);
                    return;
                }
                ToDayNumber = (int)(DateTime.Now - (RunningPlan.StartTime ?? DateTime.Now)).TotalDays + 1;

                if (!Plan.IsLoop && ToDayNumber > (Plan.DayNumber ?? 0))
                {
                    await this.Message("该计划已完成！");
                    this.BackCommand?.Execute(null);
                    return;
                }
                ToDayNumber = (ToDayNumber % Plan.DayNumber.Value) == 0 ? Plan.DayNumber.Value : (ToDayNumber % Plan.DayNumber.Value);

                //load actions
                Console.WriteLine(Plan.Id.ToString());
                Console.WriteLine(ToDayNumber.ToString());
                Plan.PlanEachDays = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == Plan.Id)).Where(x=>x.DayNumber == ToDayNumber).OrderBy(x => x.OrderNumber).ToObservableCollection();
                Console.WriteLine(Plan.PlanEachDays.Count);

                if (Plan.PlanEachDays == null || Plan.PlanEachDays.Count == 0)
                {
                    await this.Message("数据丢失，你可以选择重新开始该计划或者同步数据。");
                    this.BackCommand?.Execute(null);
                    return;
                }
                CurrentAction = Plan.PlanEachDays.FirstOrDefault(x=>x.DayNumber == ToDayNumber);
                if (CurrentAction == null)
                {
                    await this.Message("数据丢失，你可以选择重新开始该计划或者同步数据。");
                    this.BackCommand?.Execute(null);
                    return;
                }
                if (CurrentAction.IsRestDay)
                {
                    await this.Message("今天是休息日");
                    this.BackCommand?.Execute(null);
                    return;
                }


                #endregion

                PlanTitle = Plan.Name + $"第 {Plan.DayNumber} 天";

                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                    ReallySecond++;
                    ExerciseTime++;
                    return true;
                });

                await CountDownPlay();
            });
        }

        private void LoadConfigForAction()
        {
            if (CurrentAction.Style == null)
            {
                return;
            }
            if (CurrentAction.Style.TraningType)
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

        #region Commands

        public Command<object> InitCommand { get; set; }
        public Command<object> AgainCommand { get; set; }
        public DelegateCommand StopContinueCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(StopContinueFunc) }; }
        private async void StopContinueFunc(object obj)
        {
            if (!IsStop)
            {
                if (IsCountDown == false || StopTaskCancelTokenSource == null)
                {
                    StopTaskCancelTokenSource = new CancellationTokenSource();
                }
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
                await CountDown(Config.StartContinueSecond,()=> {
                    StopTaskCancelTokenSource.Cancel();
                    
                });

                if (IsStop)
                {
                    return;
                }

            }
        }
        public DelegateCommand ContinueCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(ContinueFunc) }; }

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

        public new DelegateCommand BackCommand { get => new DelegateCommand { ExecuteAction = new Action<object>(BackFunc) }; }

        private async void BackFunc(object obj)
        {
            ClearSound();
            IsBack = true;
            GC.Collect();
            await this.GoAsync("..");
        }
        #endregion


        #region Traning
        public long ExerciseTime { get; set; }
        private bool isEnd;

        public bool IsEnd
        {
            get { return isEnd; }
            set { isEnd = value; this.RaisePropertyChanged("IsEnd"); }
        }
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
            for (int i = 0; i < Plan.PlanEachDays.Count; i++)
            {
                await Traning();

                await RecordExercise();
                //action and action sleep second
                if (i+1 == Plan.PlanEachDays.Count)
                {
                    //finished
                    break;
                }

                CurrentAction = Plan.PlanEachDays[i+1];
                await Sleep(SleepCountDownNumber);

                if (IsStop)
                {
                    await Utility.CancellationDelay(100000, StopTaskCancelTokenSource.Token);
                }
            }
            //traning end
            FinishAudio.Play();

            #region record Exercise 
            //await RecordExercise();

            #endregion

            IsSleep = false;
            IsEnd = true;
            CurrentNumber = 0;
            CurrentGroupNumber = 0;
            IsStart = false;
        }

        public async Task Traning()
        {
            for (int i = 0; i < CurrentAction.GroupNumber; i++)
            {

                while (true)
                {
                    if (IsStop)
                    {
                        await Utility.CancellationDelay(100000, StopTaskCancelTokenSource.Token);

                    }
                    if (IsBack)
                    {
                        return;
                    }
                    //1
                    //Play 1
                    CurrentNumber++;
                    if (CurrentAction.Style.TraningType)
                    {
                        //count down
                        TextToSpeech.SpeakAsync(CurrentNumber.ToString(), NumberSpeechOptions);
                    }
                    else
                    {
                        OneAudio.Play();
                    }
                    IsImg1 = !IsImg1;
                    IsImg2 = !IsImg2;
                    await Task.Delay(DownNumberSecond);
                    //record Exercise Time
                    //ExerciseTime += DownNumberSecond / 1000;

                    //2
                    if (CurrentAction.Style.TraningType)
                    {
                        //count down
                        CurrentNumber++;
                        TextToSpeech.SpeakAsync(CurrentNumber.ToString(), NumberSpeechOptions);
                    }
                    else
                    {
                        TwoAudio.Play();
                    }
                    IsImg1 = !IsImg1;
                    IsImg2 = !IsImg2;
                    await Task.Delay(UpNumberSecond);

                    //record Exercise Time
                    //ExerciseTime += UpNumberSecond / 1000;
                    //Is Complete
                    if (CurrentNumber >= CurrentAction.Number)
                    {
                        CurrentNumber = 0;
                        break;
                    }
                }

                if (CurrentAction.Style.IsSingle)
                {
                    IsImg1 = true;
                    IsImg2 = false;
                    await TextToSpeech.SpeakAsync("换边", new SpeechOptions() { Volume = 1 });
                    await CountDown(Config.StartContinueSecond);
                    //ExerciseTime += Config.StartContinueSecond;

                    CurrentNumber = 0;
                    while (true)
                    {
                        if (IsStop)
                        {
                            await Utility.CancellationDelay(100000, StopTaskCancelTokenSource.Token);

                        }
                        if (IsBack)
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
                        //ExerciseTime += DownNumberSecond / 1000;
                        //2
                        TwoAudio.Play();
                        IsImg1 = !IsImg1;
                        IsImg2 = !IsImg2;
                        await Task.Delay(UpNumberSecond);
                        //record Exercise Time
                        //ExerciseTime += UpNumberSecond / 1000;
                        //Is Complete
                        if (CurrentNumber >= CurrentAction.Number)
                        {
                            CurrentNumber = 0;
                            break;
                        }
                    }
                }

                CurrentGroupNumber++;

                RecoveryAudio.Play();
                IsImg1 = true;
                IsImg2 = false;
                if (CurrentGroupNumber == CurrentAction.GroupNumber)
                {
                    //Compeleted traning
                    return;
                }
                await Sleep(SleepCountDownNumber);
            }
        }


        public async Task CountDown(int second = 3,Action Callback = null)
        {
            if (IsCountDown)
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
            if (!IsStop)
            {
                Callback?.Invoke();
            }
        }

        /// <summary>
        /// Sleep second default : 60
        /// </summary>
        public async Task Sleep(int sleepSecond)
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
                //ExerciseTime++;
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

        /// <summary>
        /// record Exercise
        /// </summary>
        private async Task RecordExercise()
        {
            if (IsBack)
            {
                return;
            }
            string SkillName = "???";
            try
            {
                //SkillName = SkillDataCommon.Skills.FirstOrDefault(a => a.Id == this.CurrentAction.Style.SkillId)?.Name;
                SkillName = CurrentAction.Style.SkillName;
            }
            catch (Exception ex)
            {

            }
            DataCommon.ExerciseLogs.Add(new ExerciseLogDTO()
            {
                Id = Guid.NewGuid(),
                AccountId = Utility.LoggedAccount.Id,
                SkillName = SkillName,
                StyleId = CurrentAction.Style.Id,
                ExerciseDateTime = DateTime.Now,
                ExerciseTime = this.ExerciseTime,
                GroupNumber = CurrentAction.GroupNumber.Value,
                Number = CurrentAction.Number.Value,
                Feeling = "",
                Updated = false,
            });
            await App.Database.SaveAsync(DataCommon.ExerciseLogs?.LastOrDefault());
            //FileHelper.WriteFile(FileHelper.ExerciseLogFile, JsonConvert.SerializeObject(DataCommon.ExerciseLogs));

        }

        public async Task CountDownPlay()
        {
            await CountDown(Config.StartContinueSecond);
            IsShowNavBar = false;
            await StartTraning();

        }

        #endregion

    }
}
