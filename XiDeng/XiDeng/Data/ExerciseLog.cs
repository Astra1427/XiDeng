using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XiDeng.DataTest;
using XiDeng.Models;
using XiDeng.Models.SkillModels;

namespace XiDeng.Data
{
    public class ExerciseLog:ModelBase
    {
        public string SkillName { get; set; }
        /// <summary>
        /// Exercise Project
        /// </summary>
        public Guid StyleID { get; set; }
        /// <summary>
        /// Exersice Date 
        /// </summary>
        public DateTime ExerciseDateTime { get; set; }
        /// <summary>
        /// Exercise Time
        /// </summary>
        public double ExerciseTime { get; set; }
        /// <summary>
        /// Feeling
        /// </summary>
        public string Feeling { get; set; }
        /// <summary>
        /// Dis Feeling max char : 11
        /// </summary>
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
                if (style == null )
                {
                    style = SkillDataCommon.Skills.FirstOrDefault(x => x.SkillStyles.Any(s => s.Id == StyleID)).SkillStyles.FirstOrDefault(x => x.Id == StyleID);
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

