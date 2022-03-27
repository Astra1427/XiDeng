using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.ViewModel;

namespace XiDeng.DataTest
{
    public class Skill
    {
        public int ID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Description { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Img { get; set; }
        public ObservableCollection<SkillStyle> Styles { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public DelegateCommand SkillCommand { get { return new DelegateCommand() { ExecuteAction = new Action<object>(SkillFunc) }; } }

        private async void SkillFunc(object obj)
        {
            await Shell.Current.GoToAsync($"StylePage?id={this.ID}");
        }

    }

    public class SkillStyle
    {
        public int ID { get; set; }
        public int SkillID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Img1 { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Img2 { get; set; }
        public string DisplayVideoUri { get; set; }
        public string VideoUri { get; set; }
        public string LocalVideoUri { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string TraningPart { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string ActionDescription { get; set; }
        /// <summary>
        /// 解析
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string Analysis { get; set; }
        /// <summary>
        /// 稳扎稳打
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string SlowSteady { get; set; }
        /// <summary>
        /// 初级标准
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Standard PrimaryStandard { get; set; }
        /// <summary>
        /// 中级标准
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Standard IntermediateStandard { get; set; }
        /// <summary>
        /// 升级标准
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Standard UpgradeStandard { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DelegateCommand SkillStyleCommand { get { return new DelegateCommand { ExecuteAction = new Action<object>(SkillStyleFunc) }; } }

        private async void SkillStyleFunc(object obj)
        {
            await Shell.Current.GoToAsync($"SkillStyleDetailPage?SkillID={this.SkillID}&SkillStyleID={this.ID}");
        }

        
    }


    public class Standard:BaseViewModel
    {

        private int groupsNumber;

        /// <summary>
        /// 组数
        /// </summary>
        public int GroupsNumber
        {
            get { return groupsNumber; }
            set { groupsNumber = value;this.RaisePropertyChanged("GroupsNumber"); }
        }
        private int number;

        /// <summary>
        /// 次数
        /// </summary>
        public int Number
        {
            get { return number; }
            set { number = value; 
                
                this.RaisePropertyChanged("Number"); }
        }

        /// <summary>
        /// 0为双侧，1为单侧，单侧的训练项目需要*2
        /// </summary>
        public int IsSingle { get; set; }

        /// <summary>
        /// 训练类型，0为组数，1为时间
        /// </summary>
        public int TraningType { get; set; }

        public override string ToString()
        {
            if (TraningType == 0)
            {
                return $"{GroupsNumber} 组 {Number}次";
            }
            else
            {
                return $"{GroupsNumber} 组 {Number}秒";
            }

        }

    }

}
