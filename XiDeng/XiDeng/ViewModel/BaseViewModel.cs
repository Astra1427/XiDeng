using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;

namespace XiDeng.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isRefresh ;
        public bool IsRefresh
        {
            get { return isRefresh; }
            set
            {
                isRefresh = value;
                this.RaisePropertyChanged(nameof(IsRefresh));
                IsE = !value;
            }
        }
        private bool isE = true;
        public bool IsE
        {
            get { return isE; }
            set
            {
                isE = value;
                this.RaisePropertyChanged(nameof(IsE));
            }
        }


        public async Task Try<T>(Func<T,Task> func,T obj,bool isRefresh)
        {
            try
            {
                
                IsRefresh = isRefresh;
                await func.Invoke(obj);
            }
            catch (Exception ex)
            {
                //logger
                await this.Message(ex.ToString());
            }
            finally
            {
                IsRefresh = false;
            }
        }

        public Command<object> BackCommand => new Command<object>(async obj=> {
            await Shell.Current.GoToAsync("../");
        });

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        }
    }
}
