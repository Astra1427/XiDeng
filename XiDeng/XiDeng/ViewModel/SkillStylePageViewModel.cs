using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XiDeng.Data;
using System.Linq;
using Xamarin.Forms;
using XiDeng.Views;

namespace XiDeng.ViewModel
{

    class SkillStylePageViewModel:NotificationObject
    {
        public int ID { get; set; }
        public SkillStylePageViewModel(int ID)
        {
            this.ID = ID;
            Init();
        
        }

        public Skill Skill { get; set; }

        private ObservableCollection<SkillStyle> skillStyles;

        public ObservableCollection<SkillStyle> SkillStyles
        {
            get { return skillStyles; }
            set { skillStyles = value; this.RaisePropertyChanged("SkillStyles"); }
        }


        /// <summary>
        /// Init data and controls
        /// </summary>
        public void Init()
        {
            try
            {
                
                Skill = DataCommon.Skills.Where(a => a.ID == ID).FirstOrDefault();
                SkillStyles = Skill.Styles;
                
            }
            catch (Exception ex)
            {
                //Error : Get data failed ,Please check the data valid!
                App.Current.MainPage.DisplayAlert("Error","获取数据失败，请检查数据是否存在","OK");
            }

        }
    }
}
