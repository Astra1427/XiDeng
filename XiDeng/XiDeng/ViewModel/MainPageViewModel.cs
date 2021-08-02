using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Views;

namespace XiDeng.ViewModel
{
    class MainPageViewModel:NotificationObject
    {
        #region Init Image
        private ImageSource bookIcon;

        public ImageSource BookIcon
        {
            get { return bookIcon; }
            set { bookIcon = value;this.RaisePropertyChanged("BookIcon"); }
        }

        #endregion

        private ObservableCollection<Skill> skills;

        public ObservableCollection<Skill> Skills
        {
            get { return skills; }
            set { skills = value; this.RaisePropertyChanged("Skills"); }
        }
        

        public MainPageViewModel()
        {
            Init();
        }
        /// <summary>
        /// Init data and controls
        /// </summary>
        private void Init()
        {
            Skills = DataCommon.Skills;
            InitImage();
        }

        private void InitImage()
        {
            BookIcon = Utility.GetImage("book_64");
        }

    }
}
