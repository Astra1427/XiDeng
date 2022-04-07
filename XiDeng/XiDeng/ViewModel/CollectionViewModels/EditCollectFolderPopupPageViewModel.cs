using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.Collections;
using Rg.Plugins.Popup.Extensions;

namespace XiDeng.ViewModel.CollectionViewModels
{
    public class EditCollectFolderPopupPageViewModel : BaseViewModel
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
        public EditCollectFolderPopupPageViewModel(CollectionFolderDTO _folder)
        {
            this.Folder = _folder;
            EditCommand = new Command<object>(async obj =>
            {
                await this.Try(async o =>
                {
                    this.IsSubmitted = true;
                    var response = await ActionNames.Collection.UpdateCollectFolder.PostAsync(Folder.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        //save to sqlite
                        await App.Database.SaveAsync(this.Folder);
                        await Shell.Current.Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await $"修改失败\n{response.Message}".Message();
                    }
                }, obj, true);
            });

        }
        public Command<object> EditCommand { get; set; }
    }
}
