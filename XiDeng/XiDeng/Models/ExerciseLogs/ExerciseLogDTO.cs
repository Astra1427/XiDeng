using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XiDeng.Models.SkillModels;

namespace XiDeng.Models.ExerciseLogs
{
    public class ExerciseLogDTO:ModelBase
    {
        [SQLite.Ignore]
        public string SkillName { get; set; }
        public Guid AccountId { get; set; }
        /// <summary>
        /// Exercise Project
        /// </summary>
        public Guid StyleId { get; set; }
        /// <summary>
        /// Exersice Date 
        /// </summary>
        public DateTime ExerciseDateTime { get; set; }
        /// <summary>
        /// Exercise Time
        /// </summary>
        public long ExerciseTime { get; set; }
        /// <summary>
        /// Feeling
        /// </summary>
        public string Feeling { get; set; }
        /// <summary>
        /// Dis Feeling max char : 11
        /// </summary>
        [SQLite.Ignore]
        public string DisFeeling { get; set; }
        public int GroupNumber { get; set; }
        public int Number { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        private SkillStyleDTO style;

        [SQLite.Ignore]
        [Newtonsoft.Json.JsonIgnore]
        public SkillStyleDTO Style
        {
            get
            {
                if (style == null)
                {
                    style = SkillDataCommon.Skills.FirstOrDefault(x => x.SkillStyles.Any(s => s.Id == StyleId)).SkillStyles.FirstOrDefault(x => x.Id == StyleId);
                }
                return style;
            }
            set { style = value; RaisePropertyChanged(nameof(Style)); }
        }

        public override string ToString()
        {
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
