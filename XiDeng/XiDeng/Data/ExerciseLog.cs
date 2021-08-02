using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Data
{
    public class ExerciseLog
    {
        public int ID { get; set; }
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        /// <summary>
        /// Exercise Project
        /// </summary>
        public int StyleID { get; set; }
        public string StyleName { get; set; }
        /// <summary>
        /// Exersice Date 
        /// </summary>
        public DateTime ExerciseDateTime { get; set; }
        /// <summary>
        /// Exercise Time
        /// </summary>
        public double ExerciseTime { get; set; }
        /// <summary>
        /// Groups Count
        /// </summary>
        public Standard ExerciseStandard { get; set; }
        /// <summary>
        /// Feeling
        /// </summary>
        public string Feeling { get; set; }
        /// <summary>
        /// Dis Feeling max char : 11
        /// </summary>
        public string DisFeeling { get; set; }
    }
}

