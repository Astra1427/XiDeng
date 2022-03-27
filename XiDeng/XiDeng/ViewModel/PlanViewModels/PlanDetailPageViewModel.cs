using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class PlanDetailPageViewModel : BaseViewModel
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

        private List<IGrouping<int,PlanEachDayDTO>> groupPlanActions;
        public List<IGrouping<int,PlanEachDayDTO>> GroupPlanActions
        {
            get { return groupPlanActions; }
            set
            {
                groupPlanActions = value;
                this.RaisePropertyChanged(nameof(GroupPlanActions));
            }
        }

        private string publishPlanText;
        public string PublishPlanText
        {
            get { return publishPlanText; }
            set
            {
                publishPlanText = value;
                this.RaisePropertyChanged(nameof(PublishPlanText));
            }
        }


        public Guid PlanId { get; set; }
        public PlanDetailPageViewModel(Guid planId)
        {
            this.PlanId = planId;
            OnAppearingCommand = new Command<object>(obj=> {
                //await Init(PlanId);
            });
            DeletePlanCommand = new Command<object>(async obj => {
                await this.Try(async o => {

                    //remove from database
                    var response = await (ActionNames.ExercisePlan.DeletePlan + $"?planId={PlanId}").PostAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        await this.Message("删除成功");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        //offline
                    }
                    else
                    {
                        await this.Message("失败：\n请检查网络连接。");
                        return;
                    }

                    //remove from sqlite
                    this.Plan.IsRemoved = true;

                    App.Database.ExercisePlans.Update(this.Plan);
                    await App.Database.SaveChangesAsync();

                }, obj, true);
            });

            GotoUpdatePlanCommand = new Command<object>(async delegate {
                await Shell.Current.GoToAsync(nameof(UpdatePlanPage)+$"?PlanJson={this.Plan.ToJson()}");
            });
            PublishPlanCommand = new Command<object>(async delegate {

                var response = await (ActionNames.ExercisePlan.PublishOrCancelPlan + $"?PlanId={Plan.Id}").PostAsync();
                if (response.IsSuccessStatusCode)
                {
                    await this.Message(this.PublishPlanText == "发布" ? "发布成功！\n 待审核通过后即可在公共计划列表中看见。" : "操作成功！");

                    this.Plan.PublishStatus = Plan.PublishStatus == 1 || Plan.PublishStatus == 2 ? 3 : 2;
                    App.Database.ExercisePlans.Update(this.Plan);
                    await App.Database.SaveChangesAsync();

                    this.PublishPlanText = this.PublishPlanText == "发布" ? "取消发布" : "发布";

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                {
                    await this.Message(response.Message);
                    return;
                }
                else
                {
                    await this.Message(response.Message);
                    return;
                }


            });


            StartPlanCommand = new Command<object>(async obj=> {
                await this.Try(async o => {
                    
                    var model = await App.Database.AccountRunningPlans.FirstOrDefaultAsync(x=>x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
                    if (model == null)
                    {
                        model = new AccountRunningPlanDTO
                        {
                            Id = Guid.NewGuid(),
                            AccountId = Utility.LoggedAccount.Id,
                            CreateTime = DateTime.Now,
                            IsPause = false,
                            IsRemoved = false,
                            PlanId = Plan.Id,
                            StartTime = DateTime.Now.Date,
                        };
                    }
                    model.StartTime = DateTime.Now.Date;
                    model.IsPause = false;

                    
                    ResponseModel response = await ActionNames.ExercisePlan.StartPlan.PostAsync(model.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        //success
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        //offline
                    }
                    else
                    {
                        await this.Message("失败，请连接网络！");
                        return;
                    }

                    //pause other plan
                    var otherPlans = await App.Database.AccountRunningPlans.Where(x=>x.AccountId == Utility.LoggedAccount.Id && x.Id != model.Id).ToListAsync();
                    otherPlans.ForEach(x => x.IsPause = true) ;
                    int rows = await App.Database.AccountRunningPlans.AddOrUpdateRangeAsync(otherPlans);
                    //start this plan
                    rows += await App.Database.AccountRunningPlans.AddOrUpdateAsync(model);

                    await this.Message($"Insert rows:{rows}");
                    IsStarted = rows > 0;
                },obj,true);
            });

            PausePlanCommand = new Command<object>(async obj=> {
                await this.Try(async o=> {
                    var response = await (ActionNames.ExercisePlan.PausePlan + $"?PlanId={Plan.Id}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        //offline
                    }
                    else
                    {
                        await this.Message("失败：\n请连接网络。");
                        return;
                    }

                    var model = await App.Database.AccountRunningPlans.FirstOrDefaultAsync(x=>x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
                    model.IsPause = true;
                    App.Database.AccountRunningPlans.Update(model);
                    int rows = await App.Database.SaveChangesAsync();
                    await this.Message($"Update rows:{rows}");
                    if (rows > 0)
                    {
                        IsStarted = false;
                    }
                },obj,true);
            });

            RestartPlanCommand = new Command<object>(async obj=> {
                await this.Try(async o=> {
                    var response = await (ActionNames.ExercisePlan.RestartPlan+$"?PlanId={Plan.Id}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        await this.Message("失败：\n请链接网路。");
                        return;
                    }
                    //update sqlite
                    var model = await App.Database.AccountRunningPlans.FirstOrDefaultAsync(x => x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
                    model.IsPause = false;
                    model.StartTime = DateTime.Now.Date;
                    App.Database.AccountRunningPlans.Update(model);
                    int rows = await App.Database.SaveChangesAsync();
                    await this.Message($"Update rows:{rows}");
                    if (rows > 0)
                    {
                        IsStarted = true;
                    }

                },obj,true);
            });
        }
        private void UpdateGroupList()
        {
            //need optimize
            this.GroupPlanActions = this.Plan.PlanEachDays.GroupBy(x => x.DayNumber).OrderBy(x => x.Key).ToList();

        }
        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
            set
            {
                isStarted = value;
                this.RaisePropertyChanged(nameof(IsStarted));
            }
        }
        private bool isOwner;
        public bool IsOwner
        {
            get { return isOwner; }
            set
            {
                isOwner = value;
                this.RaisePropertyChanged(nameof(IsOwner));
            }
        }

        public async Task Init()
        {
            //Load from local database
            this.Plan = await App.Database.ExercisePlans.FirstOrDefaultAsync(x => x.Id == PlanId);
            if (this.Plan != null)
            {
                this.Plan.PlanEachDays = await App.Database.PlanEachDays.Where(x => x.ExercisePlanDTOId == Plan.Id).ToListAsync();
            }
            //await this.Message("数据丢失！");
            //await ShellApp.Current.GoToAsync("../");
            //return;


            //load from cloud database

            await this.Try(async obj => {

                var response = await (ActionNames.ExercisePlan.GetPlanByID + $"?planId={Plan.Id}").GetStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Plan = response.Content.To<ExercisePlanDTO>();
                }
                else
                {
                    await this.Message($"加载数据失败：\n{response.Message}");
                    if (Shell.Current.Title != "登录")
                    {
                        await Shell.Current.GoToAsync("../");
                    }
                }
            }, new object(), true);


            IsOwner = Plan.AccountId == Utility.LoggedAccount.Id;

            var model = await App.Database.AccountRunningPlans.FirstOrDefaultAsync(x => x.PlanId == Plan.Id && x.AccountId == Utility.LoggedAccount.Id);
            IsStarted = model == null ? false : !model.IsPause;
            UpdateGroupList();


            PublishPlanText = this.Plan.PublishStatus == 1 || this.plan.PublishStatus == 2 ? "取消发布" : "发布";

        }


        public Command<object> OnAppearingCommand { get; set; }
        public Command<object> GotoUpdatePlanCommand { get; set; }
        public Command<object> PublishPlanCommand { get; set; }
        public Command<object> DeletePlanCommand { get; set; }
        public Command<object> StartPlanCommand { get; set; }
        public Command<object> PausePlanCommand { get; set; }
        public Command<object> RestartPlanCommand { get; set; }
    }
}
