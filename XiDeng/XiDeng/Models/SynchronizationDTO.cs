using System;
using System.Collections.Generic;
using System.Text;
using XiDeng.Models.AccountModels;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;

namespace XiDeng.Models
{
    public class SynchronizationDTO
    {
        public AccountDTO Account { get; set; }
        public IEnumerable<ExercisePlanDTO> ExercisePlans { get; set; }
        public SkillDTO Skill { get; set; }
        public IEnumerable<AccountRunningPlanDTO> RunningPlans { get; set; }
        /// <summary>
        /// Only CTL
        /// </summary>
        public IEnumerable<CollectionFolderDTO> CollectionFolders { get; set; }
        /// <summary>
        /// Only CTL
        /// </summary>
        public IEnumerable<ExercisePlanDTO> PlansOfCollectionFolders { get; set; }
    }
}
