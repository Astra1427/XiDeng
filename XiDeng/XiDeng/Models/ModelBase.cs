using System;
using System.ComponentModel;

namespace XiDeng.Models
{
    public class ModelBase:INotifyPropertyChanged
    {
        //[PrimaryKey]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;
        /// <summary>
        /// false is need update
        /// </summary>
        public bool Updated { get; set; } = App.Config == null ? false : !App.Config.IsOffline;

        public ModelBase()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
