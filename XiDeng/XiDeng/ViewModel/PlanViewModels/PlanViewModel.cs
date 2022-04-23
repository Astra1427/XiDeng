using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.PlanViews;
namespace XiDeng.ViewModel.PlanViewModels
{
    public class PlanViewModel:BaseViewModel
    {
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
        private bool byDay = true;
        public bool ByDay
        {
            get { return byDay; }
            set
            {
                byDay = value;
                if (value)
                {
                    this.Plan.Cycle = 1;
                }
                this.RaisePropertyChanged(nameof(ByDay));
            }
        }
        private bool byWeek = false;
        public bool ByWeek
        {
            get { return byWeek; }
            set
            {
                byWeek = value;
                if (value)
                {
                    this.Plan.Cycle = 0;
                    DayNumber = 7;
                }
                this.RaisePropertyChanged(nameof(ByWeek));
            }
        }
        private int dayNumber = 1;
        public int DayNumber
        {
            get { return dayNumber; }
            set
            {
                dayNumber = value;
                this.RaisePropertyChanged(nameof(DayNumber));
            }
        }
        private ImageSource cover;
        public ImageSource Cover
        {
            get { return cover; }
            set
            {
                cover = value;
                this.RaisePropertyChanged(nameof(Cover));
            }
        }
        public PlanViewModel(ExercisePlanDTO _plan = null)
        {
            GotoAddActionPageCommand = new Command<object>(async delegate {
                if (DayNumber <= 0)
                {
                    await this.Message("天数必须大于0!");
                    return;
                }
                await this.GoAsync(nameof(AddActionPage) + $"?PlanID={Plan.Id}&DayNumber={DayNumber}&ByWeek={ByWeek}");
            });
            AddSubmitCommand = new Command<object>(async obj =>
            {
                if (!await CheckPlanData())
                {
                    return;
                }
                await this.Try(async o =>
                {
                    ExercisePlanDTO newPlan = new ExercisePlanDTO()
                    {
                        Id = Plan.Id,
                        AccountId = Utility.LoggedAccount.Id,
                        Cycle = ByDay ? 1 : 0,
                        CollectionCount = 0,
                        IsPublic = Plan.IsPublic,
                        IsLoop = Plan.IsLoop,
                        Name = Plan.Name,
                        Description = Plan.Description,
                        CoverUrl = null,
                        PlanEachDays = AddActionPageViewModel.planActions.Where(x => x.DayNumber <= DayNumber).OrderBy(x=>x.DayNumber).ThenBy(x=>x.OrderNumber).ToObservableCollection(),
                        AuthorImg = Utility.LoggedAccount.PhotoUrl,
                        AuthorName = Utility.LoggedAccount.Name
                    };
                    //if online
                    //submit to datbase and save to native
                    bool Updated = false;
                    var response = await ActionNames.ExercisePlan.AddPlan.PostAsync(newPlan.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        //update plan to local data
                        await this.Message("提交成功!");
                        Updated = true;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        //offline 
                        await this.Message("提交成功！\n请注意：\n当前处于离线模式，所有操作都不会上传云端！");
                    }
                    else
                    {
                        await this.Message(response.Message);
                        return;
                    }
                    //save to native
                    newPlan.Updated = Updated;
                    int rows = await App.Database.SaveAsync(newPlan);
#if DEBUG
                    await this.Message("Save new plan rows:" + rows);
#endif
                    newPlan.PlanEachDays.ForEach(x => x.Updated = Updated);
                    rows = await App.Database.database.InsertAllAsync(newPlan.PlanEachDays);
#if DEBUG
                     await this.Message("Save actions rows:" + rows);
#endif
                    await this.GoAsync("..");
                }, obj, true);
            });
            UpdateSubmitCommand = new Command<object>(async obj=> {
                if(!await CheckPlanData())
                {
                    return;
                }
                await this.Try(async o=> {
                    ExercisePlanDTO newPlan = new ExercisePlanDTO
                    {
                        Id = Plan.Id,
                        AccountId = Utility.LoggedAccount.Id,
                        Cycle = ByDay ? 1 : 0,
                        CollectionCount = 0,
                        IsPublic = Plan.IsPublic,
                        IsLoop = Plan.IsLoop,
                        Name = Plan.Name,
                        Description = Plan.Description,
                        CoverUrl = null,
                        PlanEachDays = AddActionPageViewModel.planActions.Where(x => x.DayNumber <= DayNumber).ToObservableCollection()
                    };
                    //if online 
                    //submit to database and save to native 
                    bool Updated = false;
                    var response = await ActionNames.ExercisePlan.UpdatePlan.PostAsync(newPlan.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        Updated = true;
                        await this.Message("修改成功!");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        //offline
                        await this.Message("修改成功！\n请注意：\n当前处于离线模式，所有操作都不会上传云端！");
                    }
                    else
                    {
                        await this.Message("失败：\n"+response.Message);
                        return;
                    }
                    //save to native
                    newPlan.Updated = Updated;
                    int rows = await App.Database.SaveAsync(newPlan);
#if DEBUG
                    await this.Message(rows.ToString());
#endif
                    
                    rows = await App.Database.DeleteAllAsync(await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == newPlan.Id));
#if DEBUG
                    await this.Message("Delete rows:"+rows);
#endif
                    newPlan.PlanEachDays.ForEach(x=>x.Updated = Updated);
                    rows = await App.Database.SaveAllAsync(newPlan.PlanEachDays);
#if DEBUG
                    await this.Message("Save rows:"+rows);
#endif
                    await this.GoAsync("..");
                },obj,true);
            });
            if (_plan != null && this.Plan == null)
            {
                this.Plan = _plan;
                DayNumber = this.Plan.DayNumber.HasValue ? this.Plan.DayNumber.Value : 0;
                AddActionPageViewModel.planActions = _plan.PlanEachDays;
                this.ByWeek = _plan.Cycle == 0;
                return;
            }
            this.Plan = new ExercisePlanDTO()
            {
                Id = Guid.NewGuid(),
                AccountId = Utility.LoggedAccount.Id,
            };
        }
        private async Task<bool> CheckPlanData()
        {
            if (Plan.Name.IsEmpty())
            {
                await this.Message("计划名不能为空!");
                return false;
            }
            if (Plan.Description.IsEmpty())
            {
                await this.Message("描述不能为空!");
                return false;
            }
            if (AddActionPageViewModel.planActions == null || AddActionPageViewModel.planActions.Count == 0)
            {
                await this.Message("请设置动作!");
                return false;
            }
            var distinctPlanEachDay = AddActionPageViewModel.planActions.Select(x => x.DayNumber).Distinct().OrderBy(x => x);
            if (distinctPlanEachDay.Count() < DayNumber)
            {
                //get no action day
                string noActionDays = "";
                int index = 0;
                for (int i = 0; i < DayNumber; i++)
                {
                    if (!distinctPlanEachDay.Any(x => x == (i + 1)))
                    {
                        noActionDays += $"\n第{(i + 1)}天";
                    }
                }
                await this.Message("以下天数还未设置动作:" + noActionDays);
                return false;
            }
            return true;
        }
        public Command<object> GotoAddActionPageCommand { get; set; }
        public Command<object> AddSubmitCommand { get; set; }
        public Command<object> UpdateSubmitCommand { get; set; }
    }
}
