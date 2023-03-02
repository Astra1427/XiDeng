using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.Collections;
using Rg.Plugins.Popup.Extensions;
using XiDeng.Views.CollectionViews;
using System.Linq;
using System.Threading.Tasks;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class CollectPopupPageViewModel : BaseViewModel
    {
        private ObservableCollection<CollectionFolderDTO> collectionFolders;
        public ObservableCollection<CollectionFolderDTO> CollectionFolders
        {
            get { return collectionFolders; }
            set
            {
                collectionFolders = value;
                this.RaisePropertyChanged(nameof(CollectionFolders));
            }
        }


        private bool isAddCollectionFolder;
        public bool IsAddCollectionFolder
        {
            get { return isAddCollectionFolder; }
            set
            {
                isAddCollectionFolder = value;
                this.RaisePropertyChanged(nameof(IsAddCollectionFolder));
            }
        }
        public bool IsSubmitted { get; set; }

        public CollectPopupPageViewModel(Guid planId)
        {

            InitCommand = new Command<object>(async delegate
            {
                await Init(planId);
            });

            GotoAddFolderCommand = new Command<object>(async delegate
            {
                var popUp = new AddCollectFolderPopupPage();
                await Shell.Current.Navigation.PushPopupAsync(popUp);
                if (await popUp.PopupClosedTask)
                {
                    InitCommand?.Execute(null);
                }
            });
            SubmitCommand = new Command<object>(async delegate
            {
                await this.Try<object>(async o =>
                {
#if DEBUG
                    await this.Message($"Added:{CollectionFolders.Count(x => x.IsAdded)}\nDeleted:{CollectionFolders.Count(x => x.IsDeleted)}");
#endif
                    var model = new CollectAndUncollectPlanReqeust
                    {
                        PlanId = planId,
                        CollectFolderIds = CollectionFolders.Where(x => x.IsAdded).Select(x => x.Id),
                        UncollectFolderIds = CollectionFolders.Where(x => x.IsDeleted).Select(x => x.Id)
                    };
                    var response = await ActionNames.Collection.CollectAndUncollectPlan.PostAsync(model.ToJson());

                    if (response.IsSuccessStatusCode)
                    {
                        //success

                        //save to sqlite

                        //set "Uncollect"  remove "uncollect" and "conflict"
                        var deletedDatas = await App.Database.GetAllAsync<ExercisePlanCollectionDTO>(x =>
                        x.ExercisePlanId == planId
                        && (
                                model.UncollectFolderIds.Contains(x.CollectionFolderId)
                                || model.CollectFolderIds.Contains(x.CollectionFolderId)//conflict
                            )
                        );

                        int rows = await App.Database.DeleteAllAsync(deletedDatas);
#if DEBUG
                        await this.Message($"Deleted : {rows}");
#endif

                        //set "Collect"
                        rows = await App.Database.InsertAllAsync(model.CollectFolderIds.Select(x => new ExercisePlanCollectionDTO
                        {
                            Id = Guid.NewGuid(),
                            CollectionFolderId = x,
                            ExercisePlanId = planId,
                            Updated = true,
                        }));
#if DEBUG
                        await this.Message($"Added : {rows}");
#endif

                        IsSubmitted = true;
                        await Shell.Current.Navigation.PopPopupAsync();
                    }

                }, null, true);
            });

            TapFolderCommand = new Command<object>(obj =>
            {
                if (obj is Guid folderId)
                {
                    CollectionFolderDTO folder = CollectionFolders.FirstOrDefault(x => x.Id == folderId);
                    if (folder != null)
                    {
                        folder.IsSelected = !folder.IsSelected;
                    }
                }
            });
        }
        public async Task Init(Guid planId)
        {
            if (planId == Guid.Empty)
            {
                await this.Message("数据丢失。请刷新后重试!");
                await Shell.Current.Navigation.PopPopupAsync();
                return;
            }
            await this.Try<object>(async o =>
            {
                //load from local
                this.CollectionFolders = (await App.Database.GetAllAsync<CollectionFolderDTO>(x => x.AccountId == Utility.LoggedAccount.Id)).ToObservableCollection();
                if (CollectionFolders == null)
                {
                    await this.Message("获取收藏夹列表失败！");
                    await Shell.Current.Navigation.PopPopupAsync();
                    return;
                }
                foreach (var item in CollectionFolders)
                {

                    //第一次设置使用小写 isSelected 可以避免进入if语句
                    item.isSelected = (await App.Database.GetAsync<ExercisePlanCollectionDTO>(epc => epc.ExercisePlanId == planId && epc.CollectionFolderId == item.Id)) != null;
                }

            }, null, true);
        }
        public Command<object> InitCommand { get; set; }
        public Command<object> GotoAddFolderCommand { get; set; }
        public Command<object> SubmitCommand { get; set; }
        public Command<object> TapFolderCommand { get; set; }


    }
}
