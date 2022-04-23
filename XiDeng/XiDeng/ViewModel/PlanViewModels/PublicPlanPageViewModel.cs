using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.CollectionViews;
using XiDeng.Views.PlanViews;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Linq;
using XiDeng.Models.Collections;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
namespace XiDeng.ViewModel.PlanViewModels
{
    public class PublicPlanPageViewModel : BaseViewModel
    {
        private ObservableCollection<ExercisePlanDTO> plans;
        public ObservableCollection<ExercisePlanDTO> Plans
        {
            get { return plans; }
            set
            {
                plans = value;
                this.RaisePropertyChanged(nameof(Plans));
            }
        }
        public int pageIndex = 0;
        public int pageSize = 15;
        private int orderPriority;
        public int OrderPriority
        {
            get { return orderPriority; }
            set
            {
                orderPriority = value;
                this.RaisePropertyChanged(nameof(OrderPriority));
            }
        }
        public bool IsAppearing { get; set; }
        public bool IsLoadPlan { get; set; }
        public PublicPlanPageViewModel()
        {
            MessagingCenter.Subscribe<object, Tuple<Guid, bool, int>>(this, "UpdateCollect", (s, e) =>
            {
                UpdateCollect(e.Item1, e.Item2, e.Item3);
            });
            AppearingCommand = new AsyncCommand<object>(async obj =>
            {
                IsAppearing = true;
                await base.Appearing(obj);
                if (this.Plans != null)
                {
                    IsAppearing = false;
                    return;
                }
                Plans = new ObservableCollection<ExercisePlanDTO>();
                pageIndex = 0;
                await LoadPlans();
                IsAppearing = false;
            });
            LoadPlansCommand = new AsyncCommand<object>(async obj =>
            {
                if (IsAppearing)
                {
                    return;
                }
                IsLoadPlan = true;
                LoadMoreText = "加载中...";
                if (this.Plans == null)
                    this.Plans = new ObservableCollection<ExercisePlanDTO>();
                else
                    this.Plans.Clear();
                Plans = new ObservableCollection<ExercisePlanDTO>();
                pageIndex = 0;
                await LoadPlans();
                IsLoadPlan = false;
            });
            GotoPlanDetailCommand = new AsyncCommand<object>(async obj =>
            {
                if (obj is ExercisePlanDTO plan)
                {
                    await this.GoAsync(nameof(PlanDetailPage) + $"?PlanId={plan.Id}&ByWeek={plan.Cycle == 0}");
                }
            });
            GotoCollectionFolderPopupPageCommand = new AsyncCommand<object>(async obj =>
            {
                if (obj is Guid planId)
                {
                    var popup = new CollectPopupPage(planId);
                    await Shell.Current.Navigation.PushPopupAsync(popup);
                    bool? isCollect = await popup.PopupClosedTask;
                    UpdateCollect(planId, isCollect);
                }
            });
            LoadMoreCommand = new AsyncCommand<object>(async obj =>
            {
                if (IsAppearing || IsLoadPlan || IsLoadMore || LoadMoreText == "到底了")
                {
                    return;
                }
                IsLoadMore = true;
                LoadMoreText = "加载中...";
                if (Plans == null)
                {
                    Plans = new ObservableCollection<ExercisePlanDTO>();
                }
                await LoadPlans(false);
                IsLoadMore = false;
            });
        }
        private string loadMoreText = "加载中...";
        public string LoadMoreText
        {
            get { return loadMoreText; }
            set
            {
                loadMoreText = value;
                this.RaisePropertyChanged(nameof(LoadMoreText));
            }
        }
        public bool IsLoadMore { get; set; }
        private async Task LoadPlans(bool isAnimate = true)
        {
            await this.Try<object>(async o =>
            {
                await Task.Delay(200);
                var response = await (ActionNames.ExercisePlan.GetAllPublishPlansByPage + $"?pageIndex={pageIndex++}&pageSize={pageSize}&orderPriority={OrderPriority}").GetStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var ps = response.Content.To<ObservableCollection<ExercisePlanDTO>>();
                    
                    if (ps == null || ps.Count == 0)
                    {
                        pageIndex--;
                        LoadMoreText = "到底了";
                        return;
                    }
                    if (ps.Count < pageSize)
                    {
                        LoadMoreText = "到底了";
                    }
                    //防止初始Scroll Position 等于最后一个Item的位置
                    if (Plans == null || Plans.Count == 0)
                    {
                        Plans = ps;
                    }
                    else
                    {
                        Plans.AddRange(ps);
                    }
                    var folders = (await App.Database.GetAllAsync<CollectionFolderDTO>(f => f.AccountId == Utility.LoggedAccount.Id)).Select(x => x.Id);
                    await Plans.ForEachAsync(async x =>
                    {
                        x.IsCollect = (await App.Database.GetAsync<ExercisePlanCollectionDTO>(epc => epc.ExercisePlanId == x.Id && folders.Contains(epc.CollectionFolderId))) != null;
                    });
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.SeeOther){
                    await Shell.Current.DisplayToastAsync("当前处于离线模式");
                }
                else
                {
                    await this.Message(response.Message);
                }
            }, null, isAnimate);
        }
        private void UpdateCollect(Guid planId, bool? isCollect = null, int? collectCount = null)
        {
            var plan = Plans.FirstOrDefault(x => x.Id == planId);
            if (plan == null)
            {
                return;
            }
            if (collectCount.HasValue)
            {
                plan.CollectionCount = collectCount.Value;
                plan.IsCollect = isCollect.Value;
            }
            else if (isCollect.HasValue)
            {
                if (plan.IsCollect)
                {
                    if (!isCollect.Value)
                    {
                        //之前收藏了，但这次操作后没有收藏，则-1
                        plan.CollectionCount--;
                    }
                }
                else
                {
                    if (isCollect.Value)
                    {
                        //之前没收藏，但这次操作后收藏了，则+1
                        plan.CollectionCount++;
                    }
                }
                plan.IsCollect = isCollect.Value;
            }
        }
        public AsyncCommand<object> LoadPlansCommand { get; set; }
        public AsyncCommand<object> LoadMoreCommand { get; set; }
        public AsyncCommand<object> GotoPlanDetailCommand { get; set; }
        public AsyncCommand<object> GotoCollectionFolderPopupPageCommand { get; set; }
        public new AsyncCommand<object> AppearingCommand { get; set; }
    }
}
