using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XiDeng.Common;

namespace XiDeng.Models.ExercisePlanModels
{
    public class ExercisePlanDTO : ModelBase
    {
        
        //public new Guid Id { get; set; }
        private Guid accountId;
        public Guid AccountId
        {
            get { return accountId; }
            set
            {
                accountId = value;
                this.RaisePropertyChanged(nameof(AccountId));
            }
        }

        
        private int cycle;
        /// <summary>
        /// 0:周 1:天
        /// </summary>
        public int Cycle
        {
            get { return cycle; }
            set
            {
                cycle = value;
                this.RaisePropertyChanged(nameof(Cycle));
            }
        }

        private int collectionCount;
        public int CollectionCount
        {
            get { return collectionCount; }
            set
            {
                collectionCount = value;
                this.RaisePropertyChanged(nameof(CollectionCount));
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

        private bool isLoop;
        public bool IsLoop
        {
            get { return  isLoop; }
            set
            {
                 isLoop = value;
                this.RaisePropertyChanged(nameof(IsLoop));
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public string DisIsLoop => IsLoop ? "循环" : "单次";

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                this.RaisePropertyChanged(nameof(Description));
            }
        }

        private string coverUrl;
        public string CoverUrl
        {
            get { return coverUrl; }
            set
            {
                coverUrl = value;
                this.RaisePropertyChanged(nameof(CoverUrl));
            }
        }

        private ObservableCollection<PlanEachDayDTO> planEachDays;
        [SQLite.Ignore]
        public ObservableCollection<PlanEachDayDTO> PlanEachDays
        {
            get {return planEachDays; }
            set
            {
                planEachDays = value;
                this.RaisePropertyChanged(nameof(PlanEachDays));
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        private int? dayNumber;
        [Newtonsoft.Json.JsonIgnore]
        public int? DayNumber
        {
            get {

                if (dayNumber == null)
                {
                    dayNumber = PlanEachDays?.OrderByDescending(x => x.DayNumber).FirstOrDefault()?.DayNumber;
                }
                return dayNumber; 
            }
            set
            {
                dayNumber = value;
                this.RaisePropertyChanged(nameof(DayNumber));
            }
        }

        private int publishStatus = 3;
        /// <summary>
        /// 0 : prevent
        /// 1 : pass
        /// 2 : checking
        /// 3 : no publish
        /// </summary>
        public int PublishStatus
        {
            get { return publishStatus; }
            set
            {
                publishStatus = value;
                this.RaisePropertyChanged(nameof(PublishStatus));
            }
        }

        private bool isCollect;
        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public bool IsCollect
        {
            get { return isCollect; }
            set
            {
                isCollect = value;
                CollectIcon = Utility.GetImage(value ? "star_3_240" : "star_5_240");
                this.RaisePropertyChanged(nameof(IsCollect));
            }
        }
        private ImageSource collectIcon;
        [SQLite.Ignore]
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource CollectIcon
        {
            get { return collectIcon; }
            set
            {
                
                collectIcon = value;
                this.RaisePropertyChanged(nameof(CollectIcon));
            }
        }
        public string AuthorImg { get; set; }
        public string AuthorName { get; set; }
    }
}
