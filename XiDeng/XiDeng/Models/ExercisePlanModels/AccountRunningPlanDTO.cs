using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Models.ExercisePlanModels
{
    public class AccountRunningPlanDTO : ModelBase
    {
        public Guid AccountId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime? StartTime { get; set; }
        public bool IsPause { get; set; }
    }
}
