using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.Collections;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class AddCollectFolderPopupPageViewModel:BaseViewModel
    {
        private CollectionFolderDTO folder;
        public CollectionFolderDTO Folder
        {
            get { return folder; }
            set
            {
                folder = value;
                this.RaisePropertyChanged(nameof(Folder));
            }
        }

        public bool IsSubmitted { get; set; }
        public AddCollectFolderPopupPageViewModel()
        {
            Folder = new CollectionFolderDTO();
            AddCommand = new Command<object>(async delegate {
                await this.Try<object>(async o=> {
                    Folder.Id = Guid.NewGuid();
                    Folder.AccountId = Utility.LoggedAccount.Id;
                    var response = await ActionNames.Collection.CreateCollectFolder.PostAsync(Folder.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        //save to sqlite
                        int rows = await App.Database.InsertAsync(Folder);
                        await this.Message("添加成功!");
                        IsSubmitted = true;
                        await Shell.Current.Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await this.Message(response.Message);
                    }
                },null,true);
            });
        }
        public Command<object> AddCommand { get; set; }
    }
}
