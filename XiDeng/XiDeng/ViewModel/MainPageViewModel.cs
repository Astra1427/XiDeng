using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Models.Collections;
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
                base.Appearing(obj);
                if (Utility.LoggedAccount == null || Utility.LoggedAccount.JwtToken.IsEmpty())
                {
                    return;
                }

                await this.Try(async o =>
                {
                    RunningPlan = await App.Database.GetAsync<AccountRunningPlanDTO>(x => !x.IsPause && x.AccountId == Utility.LoggedAccount.Id);

                    if (RunningPlan == null)
                    {
                        Plan = null;
                        return;
                    }
                    Plan = await App.Database.GetAsync<ExercisePlanDTO>(x => x.Id == RunningPlan.PlanId);

                    if (Plan == null)
                    {
                        //need download plan data

                        var response = await (ActionNames.ExercisePlan.GetPlanByID + $"?planId={RunningPlan.PlanId}").GetStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            Plan = response.Content.To<ExercisePlanDTO>();
                            if (Plan == null)
                            {
                                await this.Message("计划数据丢失");
                                return;
                            }
                            await App.Database.SaveAsync(Plan);
                            await App.Database.DeleteAllAsync<PlanEachDayDTO>(x=>x.PlanId == Plan.Id);
                            await App.Database.InsertAllAsync(Plan.PlanEachDays);
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                        {
                            await this.Message($"加载计划数据失败!\n{response.Message}");
                            return;
                        }
                        else
                        {
                            await this.Message("计划数据丢失");
                            return;
                        }

                    }
                    if (Plan.IsRemoved)
                    {
                        await this.Message("该计划已被作者删除!");
                        Plan = null;
                        return;
                    }

                    Plan.PlanEachDays = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == Plan.Id)).ToObservableCollection();


                    CurrentDay = RunningPlan.StartTime.HasValue ? (int)(DateTime.Now - RunningPlan.StartTime).Value.TotalDays + 1 : 1;

                    if (CurrentDay > (Plan.DayNumber ?? 0))
                    {
                        if (Plan.IsLoop)
                        {
                            CurrentDay = (CurrentDay % Plan.DayNumber.Value) == 0 ? Plan.DayNumber.Value : (CurrentDay % Plan.DayNumber.Value);
                        }
                        else
                        {
                            // The plan is finished
                            IsFinished = true;
                        }
                    }
                }, obj, true);

            });

            GotoPlanDetailCommand = new Command<object>(async obj=> {
                if (Plan == null)
                {
                    return;
                }
                await this.GoAsync(nameof(PlanDetailPage) + $"?PlanId={Plan.Id}&ByWeek={Plan.Cycle == 0}");
            });
            StartPlanTraningCommand = new Command<object>(async obj=> {
                if (IsFinished)
                {
                    await this.Message("该计划已完成！");
                    return;
                }
                await this.GoAsync(nameof(TraningPlanPage)+$"?RunningPlanID={RunningPlan.Id}");
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
