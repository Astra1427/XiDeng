using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<CollectionFolderDTO> CollectionFolders
        {
            get { return collectionFolders; }
            set
            {
                collectionFolders = value;
                this.RaisePropertyChanged(nameof(CollectionFolders));
            }
        }
        public ImageSource FolderIcon { get; set; }
        public CollctFolderListPageViewModel()
        {
            FolderIcon = Utility.GetImage("layer_21_240");
            AppearingCommand = new Command<object>(async obj => {
                base.Appearing(obj);
                if (CollectionFolders != null)
                {
                    return;
                }
                await LoadFolders();
            });
            LoadFoldersCommand = new Command<object>(async delegate
            {
                await LoadFolders();
            });

            GotoFolderDetailCommand = new Command<object>(async obj=> {
                await this.GoAsync(nameof(CollectFolderDetailPage)+$"?FolderId={obj}");
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

        private async Task LoadFolders()
        {
            await this.Try<object>(async o => {

                await Task.Delay(200);
                var response = await (ActionNames.Collection.GetCollectionFolders + $"/{Utility.LoggedAccount.Id}/{Utility.LoggedAccount.Id}").GetStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.CollectionFolders = response.Content.To<ObservableCollection<CollectionFolderDTO>>();

                    //save data to sqlite [update&insert]
                    await App.Database.SaveAllAsync(this.CollectionFolders);

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                {
                    // Offline
                    //Load local data [SQLite]
                    this.CollectionFolders = (await App.Database.GetAllAsync<CollectionFolderDTO>(x => x.AccountId == Utility.LoggedAccount.Id && !x.IsRemoved)).ToObservableCollection();
                }
                else
                {
                    await this.Message($"获取收藏列表失败!\n{response.Message}");
                }
            }, null, true);
        }
        public Command<object> InitCommand { get; set; }
        public Command<object> LoadFoldersCommand { get; set; }
        public Command<object> GotoFolderDetailCommand { get; set; }
        public Command<object> GotoAddFolderCommand { get; set; }
        public new Command<object> AppearingCommand { get; set; }


    }
}
