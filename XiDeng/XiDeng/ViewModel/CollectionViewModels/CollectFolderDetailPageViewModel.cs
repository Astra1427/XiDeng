using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.CollectionViews;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class CollectFolderDetailPageViewModel:BaseViewModel
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
        private CollectionFolderDTO collectFolder;
        public CollectionFolderDTO CollectFolder
        {
            get { return collectFolder; }
            set
            {
                collectFolder = value;
                this.RaisePropertyChanged(nameof(CollectFolder));
            }
        }



        public CollectFolderDetailPageViewModel(Guid folderId)
        {
            this.Plans = new ObservableCollection<ExercisePlanDTO>();
            LoadPlansCommand = new Command<object>(async obj=> {
                this.Plans.Clear();
                await Load(this.CollectFolder.Id);
            });
            GotoPlanDetailCommand = new Command<object>(async obj => {
                if (obj is Guid planId)
                {
                    var plan = await App.Database.GetAsync<ExercisePlanDTO>(x => x.Id == planId);
                    if (plan == null)
                    {
                        await this.Message("该计划不存在!");
                        return;
                    }
                    await this.GoAsync(nameof(PlanDetailPage) + $"?PlanId={planId}&ByWeek={plan.Cycle == 0}");
                }
            });


            GotoEditCommand = new Command<object>(async obj=> {
                var popup = new EditCollectFolderPopupPage(this.CollectFolder);
                await Shell.Current.Navigation.PushPopupAsync(popup);
                if (await popup.PopupClosedTask)
                {
                    //refresh the folder
                    this.CollectFolder = await App.Database.GetAsync<CollectionFolderDTO>(x => x.Id == this.CollectFolder.Id);
                }
            });
            DeleteCommand = new Command<object>(async obj=> {
                if (!await this.YesMessage("确定删除这个收藏夹？"))
                {
                    return;
                }
                await this.Try(async o=> {
                    var response = await (ActionNames.Collection.RemoveCollectFolder+$"?folderId={this.CollectFolder.Id}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        //refresh sqlite
                        this.CollectFolder.IsRemoved = true;
                        int row = await App.Database.SaveAsync(this.CollectFolder);
                        await this.Message("删除成功");
                        await this.GoAsync("../");
                    }
                    else
                    {
                        await this.Message($"删除失败\n{response.Message}");
                    }

                },obj,true);
            });

        }
        public bool IsOwner { get; set; }
        public async Task Load(Guid folderId)
        {
            this.CollectFolder = await App.Database.GetAsync<CollectionFolderDTO>(x => x.Id == folderId);
            if (CollectFolder == null || folderId == Guid.Empty)
            {
                await this.Message("数据丢失");
                await this.GoAsync("../");
                return;
            }

            await this.Try<object>(async o =>
            {
                await Task.Delay(200);
                var response = await ActionNames.ExercisePlan.GetPlansByCollectionFolder.GetStringAsync(paras: folderId.ToString());
                ObservableCollection<ExercisePlanDTO> ps = null;

                if (response.IsSuccessStatusCode)
                {

                    ps = response.Content.To<ObservableCollection<ExercisePlanDTO>>();
                    //save database data to sqlite

                    //await App.Database.SaveAllAsync(ps);
                    foreach (var item in ps)
                    {
                        item.Updated = true;

                        await App.Database.SaveAsync(item);

                        await App.Database.DeleteAllAsync<PlanEachDayDTO>(x => x.PlanId == item.Id);

                        await App.Database.InsertAllAsync(item.PlanEachDays);
                    }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                {
                    await this.Message(response.Message);
                    //load offline data
                    ps = new ObservableCollection<ExercisePlanDTO>((await App.Database.GetAllAsync<ExercisePlanDTO>(x => !x.IsRemoved && x.AccountId == Utility.LoggedAccount.Id)).OrderBy(x => x.CreateTime));

                    foreach (ExercisePlanDTO plan in ps)
                    {
                        plan.PlanEachDays = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == plan.Id && !x.IsRemoved)).ToObservableCollection();
                    }
                    this.Plans = ps;
                }
                else
                {
                    await this.Message(response.Message);
                }

                this.IsOwner = this.CollectFolder.AccountId == Utility.LoggedAccount.Id;

                var planOfFolder = await App.Database.GetAllAsync<ExercisePlanCollectionDTO>(x => x.CollectionFolderId == this.CollectFolder.Id);


                await planOfFolder.ForEachAsync(async x => {
                    var plan = await App.Database.GetAsync<ExercisePlanDTO>(e => e.Id == x.ExercisePlanId);
                    if (plan != null)
                    {
                        this.Plans.Add(plan);
                    }
                });
            }, null, true);
        }

        public Command<object> LoadPlansCommand { get; set; }
        public Command<object> GotoPlanDetailCommand { get; set; }
        public Command<object> GotoEditCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }
        public Command<object> ShareCommand { get; set; }

    }
}
