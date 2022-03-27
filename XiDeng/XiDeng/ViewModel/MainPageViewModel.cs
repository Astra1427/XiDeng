using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;
using XiDeng.Views;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel
{
    class MainPageViewModel:BaseViewModel
    {
        #region Init Image
        private ImageSource bookIcon;

        public ImageSource BookIcon
        {
            get { return bookIcon; }
            set { bookIcon = value;this.RaisePropertyChanged("BookIcon"); }
        }

        #endregion

        private ObservableCollection<SkillDTO> skills;

        public ObservableCollection<SkillDTO> Skills
        {
            get { return skills; }
            set { skills = value; this.RaisePropertyChanged("Skills"); }
        }
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

        private int currentDay;
        public int CurrentDay
        {
            get { return currentDay; }
            set
            {
                currentDay = value;
                this.RaisePropertyChanged(nameof(CurrentDay));
            }
        }

        public bool IsFinished { get; set; }
        public MainPageViewModel()
        {
            OnAppearingCommand = new Command<object>(async obj=> {
                if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty())
                {
                    return;
                }
                RunningPlan = await App.Database.GetAsync<AccountRunningPlanDTO>(x=>!x.IsPause && x.AccountId == Utility.LoggedAccount.Id);
                if (RunningPlan == null)
                {
                    Plan = null;
                    return;
                }
                Plan = await App.Database.GetAsync <ExercisePlanDTO>(x=>x.Id == RunningPlan.PlanId);
                if (Plan == null)
                {
                    //need download plan data
                    await this.Message("计划数据丢失，请联网并重试。");
                }

                CurrentDay = RunningPlan.StartTime.HasValue ? (int)(DateTime.Now - RunningPlan.StartTime).Value.TotalDays + 1 : 1;
                if (CurrentDay > (Plan.DayNumber ?? 0) && Plan.IsLoop)
                {
                    CurrentDay = (CurrentDay % Plan.DayNumber.Value) == 0 ? Plan.DayNumber.Value : (CurrentDay % Plan.DayNumber.Value);
                }
                else
                {
                    // The plan is finished
                    IsFinished = true;
                }
                //


            });

            GotoPlanDetailCommand = new Command<object>(async obj=> {
                if (Plan == null)
                {
                    return;
                }
                await Shell.Current.GoToAsync(nameof(PlanDetailPage) + $"?PlanId={Plan.Id}&ByWeek={Plan.Cycle == 0}");
            });
            StartPlanTraningCommand = new Command<object>(async obj=> {
                if (IsFinished)
                {
                    await this.Message("该计划已完成！");
                    return;
                }
                await Shell.Current.GoToAsync(nameof(TraningPlanPage)+$"?RunningPlanID={RunningPlan.Id}");
            });
            Init();
        }
        /// <summary>
        /// Init data
        /// </summary>
        private void Init()
        {
            OnAppearingCommand?.Execute(null);
            Skills = SkillDataCommon.Skills;
            InitImage();
        }

        private void InitImage()
        {
            BookIcon = Utility.GetImage("book_64");
        }

        public Command<object> OnAppearingCommand { get; set; }
        public Command<object> GotoPlanDetailCommand { get; set; }
        public Command<object> StartPlanTraningCommand { get; set; }
        public Command<object> ShareCommand { get; set; }

    }
}
