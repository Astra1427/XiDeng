using Microsoft.EntityFrameworkCore;
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
using XiDeng.Views.CollectionViews;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class CollctFolderListPageViewModel:BaseViewModel
    {
        private ObservableCollection<CollectionFolderDTO> collectionFolders;
        public ObservableCollection<CollectionFolderDTO> CollectionFolder
        {
            get { return collectionFolders; }
            set
            {
                collectionFolders = value;
                this.RaisePropertyChanged(nameof(CollectionFolder));
            }
        }
        public ImageSource FolderIcon { get; set; }
        public CollctFolderListPageViewModel()
        {
            FolderIcon = Utility.GetImage("layer_21_240");
            LoadFoldersCommand = new Command<object>(async delegate
            {
                await this.Try<object>(async o=> {

                    await Task.Delay(200);
                    var response = await (ActionNames.Collection.GetCollectionFolders + $"/{Utility.LoggedAccount.Id}/{Utility.LoggedAccount.Id}").GetStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.CollectionFolder = response.Content.To<ObservableCollection<CollectionFolderDTO>>();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        // Offline
                        //Load local data [SQLite]
                        this.CollectionFolder = (await App.Database.CollectionFolders.Where(x => x.AccountId == Utility.LoggedAccount.Id).ToListAsync()).ToObservableCollection();
                    }
                    else
                    {
                        await this.Message($"获取收藏列表失败!\n{response.Message}");
                    }
                },null,true);
            });

            GotoFolderDetailCommand = new Command<object>(async obj=> {
                await Shell.Current.GoToAsync(nameof(CollectFolderDetailPage));
            });
            GotoAddFolderCommand = new Command<object>(async delegate {
                if (App.Config.IsOffline)
                {
                    await this.Message("当前处于离线模式,请联网后再执行此操作。");
                    return;
                }
                await Shell.Current.Navigation.PushPopupAsync(new AddCollectFolderPopupPage());
            });

        }


        public Command<object> InitCommand { get; set; }
        public Command<object> LoadFoldersCommand { get; set; }
        public Command<object> GotoFolderDetailCommand { get; set; }
        public Command<object> GotoAddFolderCommand { get; set; }


    }
}
