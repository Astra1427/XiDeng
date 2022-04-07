using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.AccountViews;
using XiDeng.Views.CollectionViews;
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

        private List<IGrouping<int, PlanEachDayDTO>> groupPlanActions;
        public List<IGrouping<int, PlanEachDayDTO>> GroupPlanActions
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
            DeletePlanCommand = new Command<object>(async obj =>
            {
                if (!await this.YesMessage("确定删除这个计划？"))
                {
                    return;
                }
                await this.Try(async o =>
                {

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

                    int rows = await App.Database.UpdateAsync(this.Plan);

                    await this.Message(rows.ToString());
                    if (rows > 0)
                    {
                        IsStarted = true;
                    }
                }, obj, true);
            });

            GotoUpdatePlanCommand = new Command<object>(async delegate
            {
                await this.GoAsync(nameof(UpdatePlanPage) + $"?PlanJson={this.Plan.ToJson()}");
            });
            PublishPlanCommand = new Command<object>(async delegate
            {

                var response = await (ActionNames.ExercisePlan.PublishOrCancelPlan + $"?PlanId={Plan.Id}").PostAsync();
                if (response.IsSuccessStatusCode)
                {
                    await this.Message(this.PublishPlanText == "发布" ? "发布成功！\n 待审核通过后即可在公共计划列表中看见。" : "操作成功！");

                    this.Plan.PublishStatus = Plan.PublishStatus == 1 || Plan.PublishStatus == 2 ? 3 : 2;
                    int row = await App.Database.UpdateAsync(this.Plan);
#if DEBUG
                    await this.Message(row.ToString());
#endif
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


            StartPlanCommand = new Command<object>(async obj =>
            {
                await this.Try(async o =>
                {

                    var model = await App.Database.GetAsync<AccountRunningPlanDTO>(x => x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
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
                    var otherPlans = await App.Database.GetAllAsync<AccountRunningPlanDTO>(x => x.AccountId == Utility.LoggedAccount.Id && x.Id != model.Id);
                    otherPlans.ForEach(x => x.IsPause = true);
                    int otherRows = await App.Database.SaveAllAsync(otherPlans);
                    //start this plan
                    int rows = await App.Database.SaveAsync(model);

                    await this.Message($"Insert rows:{rows}\nOther:{otherRows}");
                    IsStarted = rows > 0;
                }, obj, true);
            });

            PausePlanCommand = new Command<object>(async obj =>
            {
                await this.Try(async o =>
                {
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

                    var model = await App.Database.GetAsync<AccountRunningPlanDTO>(x => x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
                    model.IsPause = true;
                    int rows = await App.Database.UpdateAsync(model);
                    await this.Message($"Update rows:{rows}");
                    if (rows > 0)
                    {
                        IsStarted = false;
                    }
                }, obj, true);
            });

            RestartPlanCommand = new Command<object>(async obj =>
            {
                await this.Try(async o =>
                {
                    var response = await (ActionNames.ExercisePlan.RestartPlan + $"?PlanId={Plan.Id}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        await this.Message("失败：\n请链接网路。");
                        return;
                    }
                    //update sqlite
                    var model = await App.Database.GetAsync<AccountRunningPlanDTO>(x => x.AccountId == Utility.LoggedAccount.Id && x.PlanId == Plan.Id);
                    model.IsPause = false;
                    model.StartTime = DateTime.Now.Date;
                    int rows = await App.Database.UpdateAsync(model);
                    await this.Message($"Update rows:{rows}");
                    if (rows > 0)
                    {
                        IsStarted = true;
                    }

                }, obj, true);
            });
            GotoCollectPopupPageCommand = new Command<object>(async obj =>
            {
                var popup = new CollectPopupPage(planId);
                await Shell.Current.Navigation.PushPopupAsync(popup);

                bool? isCollect = await popup.PopupClosedTask;
                if (!isCollect.HasValue)
                {
                    return;
                }

                if (Plan.IsCollect)
                {
                    if (!isCollect.Value)
                    {
                        Plan.CollectionCount--;
                    }
                }
                else
                {
                    if (isCollect.Value)
                    {
                        plan.CollectionCount++;
                    }
                }

                Plan.IsCollect = isCollect.Value;

                if (Plan.IsCollect)
                {
                    await this.Try(async o =>
                    {
                        await App.Database.SaveAsync(this.Plan);
                        await App.Database.DeleteAllAsync<PlanEachDayDTO>(x => x.PlanId == this.PlanId);
                        await App.Database.InsertAllAsync(this.Plan.PlanEachDays);
                    }, obj, false);
                }

                MessagingCenter.Send<object, Tuple<Guid, bool, int>>(this, "UpdateCollect", new Tuple<Guid, bool, int>(PlanId, Plan.IsCollect, Plan.CollectionCount));

            });

            RefreshCommand = new Command<object>(async delegate
            {
                await Task.Delay(200);
                await this.Try<object>(async o =>
                {
                    await LoadPlanFromCloud();
                    if (Plan == null)
                    {
                        await this.Message("计划数据丢失");
                        return;
                    }
                    await SetPlan();
                }, null, true);
            });

            GotoAuthorVisitorPageCommand = new Command<object>(async delegate {
                await this.GoAsync(nameof(VisitorPage)+$"?AuthorId={this.Plan.AccountId}");
            });
            //OnAppearingCommand?.Execute(null);
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
            if (this.Plan != null)
            {
                return;
            }
            await Task.Delay(200);
            await this.Try<object>(async obj =>
            {
                //Load from local database
                this.Plan = await App.Database.GetAsync<ExercisePlanDTO>(x => x.Id == PlanId);

                //await this.Message("数据丢失！");
                //await ShellApp.Current.GoAsync("../");
                //return;


                //load from cloud database
                if (Plan == null)
                {
                    await LoadPlanFromCloud();
                }

                if (Plan == null)
                {
                    await this.Message("计划数据丢失");
                    return;
                }
                await SetPlan();

            }, null, true);
        }

        private async Task LoadPlanFromCloud()
        {
            var response = await (ActionNames.ExercisePlan.GetPlanByID + $"?planId={PlanId}").GetStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Plan = response.Content.To<ExercisePlanDTO>();
                
            }
            else
            {
                await this.Message($"加载数据失败：\n{response.Message}");
                if (Shell.Current.Title != "登录")
                {
                    await this.GoAsync("../");
                }
                return;
            }

        }
        private async Task SetPlan()
        {
            if (this.Plan.PlanEachDays == null || this.Plan.PlanEachDays.Count == 0)
            {
                this.Plan.PlanEachDays = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == Plan.Id)).ToObservableCollection();
            }

            IsOwner = Plan.AccountId == Utility.LoggedAccount.Id;

            var model = await App.Database.GetAsync<AccountRunningPlanDTO>(x => x.PlanId == Plan.Id && x.AccountId == Utility.LoggedAccount.Id);
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
        public Command<object> GotoCollectPopupPageCommand { get; set; }
        public Command<object> RefreshCommand { get; set; }
        public Command<object> GotoAuthorVisitorPageCommand { get; set; }
    }
}
