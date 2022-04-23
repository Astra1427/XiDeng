using SQLite;
using System;
using System.ComponentModel;
using System.Linq;
using XiDeng.Models.SkillModels;

namespace XiDeng.Models.ExercisePlanModels
{
    public class PlanEachDayDTO : ModelBase
    {

        //[PrimaryKey]
        //public new Guid Id { get; set; }
        private Guid planId;
        public Guid PlanId
        {
            get { return planId; }
            set
            {
                planId = value;
                this.RaisePropertyChanged(nameof(PlanId));
            }
        }


        private Guid? styleID;
        public Guid? StyleID
        {
            get { return styleID; }
            set
            {
                styleID = value;
                this.RaisePropertyChanged(nameof(StyleID));
            }
        }


        private int dayNumber;
        public int DayNumber
        {
            get { return dayNumber; }
            set
            {
                dayNumber = value;
                this.RaisePropertyChanged(nameof(DayNumber));
            }
        }

        private int? groupNumber;
        public int? GroupNumber
        {
            get { return groupNumber; }
            set
            {
                groupNumber = value;
                this.RaisePropertyChanged(nameof(GroupNumber));
            }
        }

        private int? number;
        public int? Number
        {
            get { return number; }
            set
            {
                number = value;
                this.RaisePropertyChanged(nameof(Number));
            }
        }


        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                this.RaisePropertyChanged(nameof(Time));
            }
        }

        private bool isRestDay;
        public bool IsRestDay
        {
            get { return isRestDay; }
            set
            {
                isRestDay = value;
                this.RaisePropertyChanged(nameof(IsRestDay));
            }
        }


        [Newtonsoft.Json.JsonIgnore]
        private SkillStyleDTO style;

        [SQLite.Ignore]
        [Newtonsoft.Json.JsonIgnore]
        public SkillStyleDTO Style
        {
            get {
                if (style == null && StyleID.HasValue)
                {
                    style = SkillDataCommon.Skills.FirstOrDefault(x=>x.SkillStyles.Any(s=>s.Id == StyleID))?.SkillStyles?.FirstOrDefault(x=>x.Id == StyleID);
                }
                return style;
            }
            set { style = value; RaisePropertyChanged(nameof(Style)); }
        }


        private string disDayNumber;
        public string DisDayNumber
        {
            get { return disDayNumber; }
            set
            {
                disDayNumber = value;
                this.RaisePropertyChanged(nameof(DisDayNumber));
            }
        }
        private int orderNumber;
        public int OrderNumber
        {
            get { return orderNumber; }
            set
            {
                orderNumber = value;
                this.RaisePropertyChanged(nameof(OrderNumber));
            }
        }


        public override string ToString()
        {
            if (IsRestDay || Style == null)
            {
                return "休息日";
            }

            if (!Style.TraningType)
            {
                return $"{GroupNumber} 组 {Number}次";
            }
            else
            {
                return $"{GroupNumber} 组 {Number}秒";
            }

        }

    }
}
