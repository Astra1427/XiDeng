using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
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
        private bool isPublic;
        public bool IsPublic
        {
            get { return isPublic; }
            set
            {
                isPublic = value;
                this.RaisePropertyChanged(nameof(IsPublic));
            }
        }


        public AddCollectFolderPopupPageViewModel()
        {
            AddCommand = new Command<object>(async delegate {
                await this.Try<object>(async o=> {
                    await Task.Delay(3000);
                },null,true);
            });
        }
        public Command<object> AddCommand { get; set; }
    }
}
