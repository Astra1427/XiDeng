using System;
using System.Collections.Generic;
using System.Text;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;

namespace XiDeng.Models.AccountModels
{
    public class VisitAccountInfoDTO
    {
        public AccountDTO Account { get; set; }
        /// <summary>
        /// Only Plan. [PlanEachDays] is not included.
        /// </summary>
        public IEnumerable<ExercisePlanDTO> PublishPlans { get; set; }
        /// <summary>
        /// Only Folder. [ExercisePlanCollections] is not included.
        /// </summary>
        public IEnumerable<CollectionFolderDTO> PublicFolders { get; set; }
    }
}
