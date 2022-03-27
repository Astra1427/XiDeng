using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XiDeng.Command;
using XiDeng.Common;

namespace XiDeng.Models.SkillModels
{
    public class SkillStyleDTO:ModelBase
    {
        //[PrimaryKey]
        //public new Guid Id { get; set; }
        public Guid SkillId { get; set; }
        public string Name { get; set; } 
        public string Img1Url { get; set; }
        [SQLite.Ignore]
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Img1 => Uri.CheckSchemeName(Img1Url) ? ImageSource.FromUri(new Uri(Img1Url)) : Utility.GetImage(Img1Url);
        public string Img2Url { get; set; }
        [SQLite.Ignore]
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Img2 => Uri.CheckSchemeName(Img2Url) ? ImageSource.FromUri(new Uri(Img2Url)) : Utility.GetImage(Img2Url);
        public string VideoUrl { get; set; }
        public string TraningPart { get; set; }
        public string ActionDescription { get; set; }
        public string Analysis { get; set; }
        public string SlowSteady { get; set; }
        public long OrderNumber { get; set; }
        public new DateTime CreateTime { get; set; }

        /// <summary>
        /// False is the number of groups
        /// True is the time
        /// </summary>
        public bool TraningType { get; set; }
        /// <summary>
        /// ture is single
        /// false is double
        /// </summary>
        public bool IsSingle { get; set; }


        [SQLite.Ignore]
        public ObservableCollection<StandardDTO> Standards { get; set; }

        public string SkillName { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public DelegateCommand SkillStyleCommand { get { return new DelegateCommand { ExecuteAction = new Action<object>(SkillStyleFunc) }; } }

        private async void SkillStyleFunc(object obj)
        {
            await Shell.Current.GoToAsync($"SkillStyleDetailPage?SkillID={this.SkillId}&SkillStyleID={this.Id}");
        }
        public SkillStyleDTO()
        {

        }
    }
}
