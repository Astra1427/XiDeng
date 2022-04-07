using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class EditAccountNamePopupPageViewModel : BaseViewModel
    {
        private string newName;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                this.RaisePropertyChanged(nameof(NewName));
            }
        }
        public bool IsSubmitted { get; set; }
        public EditAccountNamePopupPageViewModel()
        {
            NewName = Utility.LoggedAccount.Name;
            SubmitCommand = new Command<object>(async delegate
            {
                if (NewName.IsEmpty())
                {
                    await this.Message("姓名不能为空！");
                    return;
                }
                await this.Try<object>(async o=> {
                    IsSubmitted = true;
                    var response = await (ActionNames.Account.EditAccountName + $"?newName={NewName}").PostAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        await this.Message("修改成功");
                        //save to local

                        Utility.LoggedAccount.Name = NewName;
                        FileHelper.WriteFile(FileHelper.LoginInfoFile,Utility.LoggedAccount.ToJson());

                        await Shell.Current.Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await this.Message("修改失败\n" + response.Message);
                    }
                },null,true);
            });
        }
        public Command<object> SubmitCommand { get; set; }

    }
}
