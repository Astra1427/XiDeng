using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        //private ObservableCollection<PlanEachDayDTO> planEachDays;
        //[SQLite.Ignore]
        //public ObservableCollection<PlanEachDayDTO> PlanEachDays
        //{
        //    get { return planEachDays; }
        //    set
        //    {
        //        planEachDays = value;
        //        this.RaisePropertyChanged(nameof(PlanEachDays));
        //    }
        //}
        private IList<PlanEachDayDTO> planEachDays;
        public IList<PlanEachDayDTO> PlanEachDays
        {
            get {
                if (planEachDays == null)
                {
                    planEachDays = App.Database.PlanEachDays.Where(x=>x.ExercisePlanDTOId == this.Id).ToList();
                }
                return planEachDays; 
            }
            set { planEachDays = value; }
        }


        [Newtonsoft.Json.JsonIgnore]
        private int? dayNumber;
        [Newtonsoft.Json.JsonIgnore]
        [NotMapped]
        public int? DayNumber
        {
            get {

                if (dayNumber == null)
                {
                    dayNumber = PlanEachDays?.OrderByDescending(x => x.DayNumber).FirstOrDefault()?.DayNumber ?? 0;
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

        public bool IsCollect { get; set; }
        [NotMapped]
        public ImageSource CollectIcon => Utility.GetImage(IsCollect ? "star_3_240" : "star_5_240");

    }
}
