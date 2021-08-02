using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XiDeng.ViewModel
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        }
    }
}
