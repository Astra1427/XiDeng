﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;
using XiDeng.Views;
using XiDeng.Views.ExerciseLogViews;
using XiDeng.Views.PlanViews;
namespace XiDeng.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        #region Init Image
        private ImageSource bookIcon;
        public ImageSource BookIcon
        {
            get { return bookIcon; }
            set { bookIcon = value; this.RaisePropertyChanged("BookIcon"); }
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
        private ImageSource tempCover = Utility.GetImage("pexels_evgeny_tchebotarev_4101555");
        public ImageSource TempCover
        {
            get { return tempCover; }
            set
            {
                tempCover = value;
                this.RaisePropertyChanged(nameof(TempCover));
            }
        }
        public bool IsFinished { get; set; }
        public MainPageViewModel()
        {
            OnAppearingCommand = new AsyncCommand<object>(async obj =>
            {
                await base.Appearing(obj);
                await InitSkills();
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
                            await App.Database.DeleteAllAsync<PlanEachDayDTO>(x => x.PlanId == Plan.Id);
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

                    if (Plan.Cycle == 1)
                    {
                        CurrentDay = RunningPlan.StartTime.HasValue ? (int)(DateTime.Now - RunningPlan.StartTime).Value.TotalDays + 1 : 1;
                    }
                    else
                    {
                        CurrentDay = (int)DateTime.Now.DayOfWeek == 0 ? 7 : (int)DateTime.Now.DayOfWeek;
                    }

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
                    if (!Plan.CoverUrl.IsEmpty())
                    {
                        Uri CoverUri = null;
                        if (Uri.TryCreate(Plan.CoverUrl, UriKind.Absolute, out CoverUri))
                        {
                            TempCover = ImageSource.FromUri(CoverUri);
                        }
                    }
                }, obj, true);
            });
            GotoPlanDetailCommand = new AsyncCommand<object>(async obj =>
            {
                if (Plan == null)
                {
                    return;
                }
                await this.GoAsync(nameof(PlanDetailPage) + $"?PlanId={Plan.Id}&ByWeek={Plan.Cycle == 0}");
            });
            StartPlanTraningCommand = new AsyncCommand<object>(async obj =>
            {
                if (IsFinished)
                {
                    await this.Message("该计划已完成！");
                    return;
                }
                await this.GoAsync(nameof(TraningPlanPage) + $"?RunningPlanID={RunningPlan.Id}");
            });
            GotoExerciseCalendarLogCommand = new AsyncCommand(async delegate
            {
                await this.GoAsync(nameof(ExerciseCalendarLogPage));
            });
        }
        /// <summary>
        /// Init data
        /// </summary>
        private async Task InitSkills()
        {
            if (SkillDataCommon.Skills == null || SkillDataCommon.Skills.Count < 6)
            {
                if (!(await LoadNewVersionData()))
                {
                    await Shell.Current.DisplayToastAsync("加载训练动作失败！");
                }
            }
            Skills = SkillDataCommon.Skills.OrderBy(x => x.OrderNumber).ToObservableCollection();
            InitImage();
        }
        public async Task<bool> LoadNewVersionData()
        {
            //bool IsLogged = Utility.LoggedAccount != null && !Utility.LoggedAccount.JwtToken.IsEmpty();
            //if (IsLogged)
            //{
            //    SkillDataCommon.Skills = (await App.Database.GetAllAsync<SkillDTO>(x => x.OrderNumber <= 6 || x.OwnerId == Utility.LoggedAccount.Id)).ToObservableCollection();
            //}
            //else
            //{
            //    SkillDataCommon.Skills = (await App.Database.GetAllAsync<SkillDTO>(x => x.OrderNumber <= 6)).ToObservableCollection();
            //}
            //if (SkillDataCommon.Skills == null || SkillDataCommon.Skills.Count < 6)
            //{
            //    SkillDataCommon.Skills = Encoding.UTF8.GetString(XiDeng.Properties.Resources.XiDengSkillsDataJson).To<ObservableCollection<SkillDTO>>();
            //}
            //else
            //{
            //    await SkillDataCommon.Skills.ForEachAsync(async x => {
            //        x.SkillStyles = await App.Database.GetAllAsync<SkillStyleDTO>(st => st.SkillId == x.Id);
            //    });
            //}
            SkillDataCommon.Skills = Encoding.UTF8.GetString(XiDeng.Properties.Resources.XiDengSkillsDataJson).To<ObservableCollection<SkillDTO>>();
            return await Task.FromResult(SkillDataCommon.Skills != null);
        }
        private void InitImage()
        {
            BookIcon = Utility.GetImage("book_64");
        }
        public AsyncCommand<object> OnAppearingCommand { get; set; }
        public AsyncCommand<object> GotoPlanDetailCommand { get; set; }
        public AsyncCommand<object> StartPlanTraningCommand { get; set; }
        public AsyncCommand<object> ShareCommand { get; set; }
        public AsyncCommand GotoExerciseCalendarLogCommand { get; set; }
    }
}
