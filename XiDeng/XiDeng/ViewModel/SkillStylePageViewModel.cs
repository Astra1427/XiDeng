using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XiDeng.Data;
using System.Linq;
using Xamarin.Forms;
using XiDeng.Views;
using XiDeng.Models.SkillModels;
using XiDeng.Common;

namespace XiDeng.ViewModel
{

    class SkillStylePageViewModel:BaseViewModel
    {
        public Guid ID { get; set; }
        public SkillStylePageViewModel(Guid ID)
        {
            this.ID = ID;
            Init();
        
        }

        public SkillDTO Skill { get; set; }

        private ObservableCollection<SkillStyleDTO> skillStyles;

        public ObservableCollection<SkillStyleDTO> SkillStyles
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
                
                Skill = SkillDataCommon.Skills.Where(a => a.Id == ID).FirstOrDefault();
                SkillStyles = Skill.SkillStyles.ToObservableCollection();
                
            }
            catch (Exception ex)
            {
                //Error : Get data failed ,Please check the data valid!
                App.Current.MainPage.DisplayAlert("Error","获取数据失败，请检查数据是否存在","OK");
            }

        }
    }
}
