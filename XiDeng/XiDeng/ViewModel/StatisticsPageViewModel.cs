using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Data;
using System.Linq;

namespace XiDeng.ViewModel
{
    class StatisticsPageViewModel:NotificationObject
    {
        #region Image sources
        private ImageSource statisticsBackImage;

        public ImageSource StatisticsBackImage
        {
            get { return statisticsBackImage; }
            set { statisticsBackImage = value; RaisePropertyChanged("StatisticsBackImage"); }
        }
        #endregion

        private string exerciseTotal;

        public string ExerciseTotal
        {
            get { return exerciseTotal; }
            set { exerciseTotal = value; RaisePropertyChanged("ExerciseTotal"); }
        }

        private string exerciseToDay;

        public string ExerciseToDay
        {
            get { return exerciseToDay; }
            set { exerciseToDay = value; RaisePropertyChanged("ExerciseToDay"); }
        }

        private int exerciseTotalDayCount;

        public int ExerciseTotalDayCount
        {
            get { return exerciseTotalDayCount; }
            set { exerciseTotalDayCount = value; RaisePropertyChanged("ExerciseTotalDayCount"); }
        }

        private int continuousExerciseDays;

        public int ContinuousExerciseDays
        {
            get { return continuousExerciseDays; }
            set { continuousExerciseDays = value;RaisePropertyChanged("ContinuousExerciseDays"); }
        }

        private int maxContinuousExerciseDays;

        public int MaxContinuousExerciseDays
        {
            get { return maxContinuousExerciseDays; }
            set { maxContinuousExerciseDays = value; RaisePropertyChanged("MaxContinuousExerciseDays"); }
        }

        private int exerciseGroupCount;

        public int ExerciseGroupCount
        {
            get { return exerciseGroupCount; }
            set { exerciseGroupCount = value; RaisePropertyChanged("ExerciseGroupCount"); }
        }

        private int skill1Count;

        public int Skill1Count
        {
            get { return skill1Count; }
            set { skill1Count = value; RaisePropertyChanged("Skill1Count"); }
        }

        private int skill2Count;

        public int Skill2Count
        {
            get { return skill2Count; }
            set { skill2Count = value; RaisePropertyChanged("Skill2Count"); }
        }

        private int skill3Count;

        public int Skill3Count
        {
            get { return skill3Count; }
            set { skill3Count = value; RaisePropertyChanged("Skill3Count"); }
        }

        private int skill4Count;

        public int Skill4Count
        {
            get { return skill4Count; }
            set { skill4Count = value; RaisePropertyChanged("Skill4Count"); }
        }

        private int skill5Count;

        public int Skill5Count
        {
            get { return skill5Count; }
            set { skill5Count = value; RaisePropertyChanged("Skill5Count"); }
        }

        private int skill6Count;

        public int Skill6Count
        {
            get { return skill6Count; }
            set { skill6Count = value; RaisePropertyChanged("Skill6Count"); }
        }

        public StatisticsPageViewModel()
        {
            StatisticsBackImage = Utility.GetImage("StatisticsBackImage");
            /*
             锻炼总时长 小时
            今日锻炼时长 分钟
            锻炼天数 
            已经连续锻炼天数 
            最大连续锻炼天数
            锻炼组数
            六艺十式共完成了多少次 1、2算一次
             */
            ExerciseTotal = (DataCommon.ExerciseLogs.Sum(a => a.ExerciseTime) / 60 / 60).ToString("0.00");
            ExerciseToDay = (DataCommon.ExerciseLogs.Where(a => a.ExerciseDateTime.Date == DateTime.Now.Date).Sum(a => a.ExerciseTime) / 60).ToString("0.00") ;
            var group = DataCommon.ExerciseLogs.GroupBy(a => a.ExerciseDateTime.ToString("yyyy-MM-dd")).ToList();
            ExerciseTotalDayCount = group.Count();
            bool IsFullContinue = true;
            int tempMaxCount = 0;
            for (int i = 0; i < group.Count; i++)
            {
                ContinuousExerciseDays++;
                tempMaxCount++;
                if (i == 0)
                {
                    continue;
                }
                DateTime dt = DateTime.Parse(group[i-1].Key);
                DateTime dt2 = DateTime.Parse(group[i].Key);

                if ((dt2 - dt).TotalDays > 1)
                {
                    IsFullContinue = false;
                    ContinuousExerciseDays = 1;
                    tempMaxCount = 1;
                }
                else
                {
                    if (tempMaxCount > MaxContinuousExerciseDays)
                    {
                        MaxContinuousExerciseDays = tempMaxCount;
                    }
                }
            }
            if (IsFullContinue)
            {
                ContinuousExerciseDays = group.Count;
                MaxContinuousExerciseDays = group.Count;
            }
            
            

            ExerciseGroupCount = DataCommon.ExerciseLogs.Sum(a=>a.ExerciseStandard.GroupsNumber);

            Skill1Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 1).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
            Skill2Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 2).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
            Skill3Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 3).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
            Skill4Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 4).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
            Skill5Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 5).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
            Skill6Count = DataCommon.ExerciseLogs.Where(a=>a.SkillID == 6).Sum(a=>a.ExerciseStandard.GroupsNumber* a.ExerciseStandard.Number);
        }
    }
}
