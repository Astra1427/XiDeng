using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.Collections;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class CollectPopupPageViewModel:BaseViewModel
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


        public CollectPopupPageViewModel()
        {
            InitCommand = new Command<object>(async delegate {
                //load from cloud
                var response = await (ActionNames.Collection.GetCollectionFolders+$"/{Utility.LoggedAccount.Id}/{Utility.LoggedAccount.Id}").GetStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.CollectionFolders = response.Content.To<ObservableCollection<CollectionFolderDTO>>();
                }
                else
                {
                    await this.Message("获取收藏夹列表失败！");
                }
            });
            ToggleCollectionFolderCommand = new Command<object>(delegate {
                IsAddCollectionFolder = !IsAddCollectionFolder;
            });
        }

        public Command<object> InitCommand { get; set; }
        public Command<object> ToggleCollectionFolderCommand { get; set; }


    }
}
