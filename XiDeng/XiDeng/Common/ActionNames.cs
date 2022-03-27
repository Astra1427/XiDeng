using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Common
{
    public static class ActionNames
    {
        #region Account Controller

        public static class Account
        {
            public static readonly string SendRegisterEmail = "account/SendRegisterEmail";
            public static readonly string Authenticate = "account/authenticate";
            public static readonly string RefreshToken = "account/refresh-token";
            public static readonly string Register = "account/Register";
            public static readonly string SendForgotPasswordEmail = "account/SendForgotPasswordEmail";
            public static readonly string ResetPassword = "account/ResetPassword";

        }

        #endregion

        #region ExercisePlan Controller
        public static class ExercisePlan
        {
            public static readonly string GetAllPlans = "ExercisePlan/GetAllPlans";
            public static readonly string AddPlan = "ExercisePlan/AddPlan";
            /// <summary>
            /// POST : ExercisePlanDTO model
            /// </summary>
            public static readonly string UpdatePlan = "ExercisePlan/UpdatePlan";
            /// <summary>
            /// Guid planId
            /// </summary>
            public static readonly string DeletePlan = "ExercisePlan/DeletePlan";
            /// <summary>
            /// POST : AccountRunningPlanDTO model
            /// </summary>
            public static readonly string StartPlan = "ExercisePlan/StartPlan";
            /// <summary>
            /// POST : Guid planId
            /// </summary>
            public static readonly string PausePlan = "ExercisePlan/PausePlan";
            /// <summary>
            /// POST : Guid PlanId
            /// </summary>
            public static readonly string RestartPlan = "ExercisePlan/RestartPlan";
            /// <summary>
            /// POST : Guid PlanId
            /// </summary>
            public static readonly string PublishOrCancelPlan = "ExercisePlan/PublishOrCancelPlan";
            /// <summary>
            /// GET : int pageIndex,int pageSize,int orderPriority
            /// </summary>
            public static readonly string GetAllPublishPlansByPage = "ExercisePlan/GetAllPublishPlansByPage";
            /// <summary>
            /// GET : string planName
            /// </summary>
            public static readonly string SearchPublishPlansByName = "ExercisePlan/SearchPublishPlansByName";
            /// <summary>
            /// GET : Guid planId
            /// </summary>
            public static readonly string GetPlanByID = "ExercisePlan/GetPlanByID";
            /// <summary>
            /// POST : Guid planId
            /// </summary>
            public static readonly string CollectPlan = "ExercisePlan/CollectPlan";
            /// <summary>
            /// POST : Guid planId
            /// </summary>
            public static readonly string UncollectPlan = "ExercisePlan/UncollectPlan";

            /// <summary>
            /// GET : Guid folderId
            /// </summary>
            public static readonly string GetPlansByCollectionFolder = "ExercisePlan/GetPlansByCollectionFolder";


        }
        #endregion

        #region Skill Controller
        public static class Skill
        {
            public static readonly string GetSkillsWithDefault = "Skill/GetSkillsWithDefault";
            public static readonly string GetSkills = "Skill/GetSkills";

        }
        #endregion


        #region Synchronization Controller
        public static class Synchronization
        {
            /// <summary>
            /// POST : SynchronizationDTO model
            /// </summary>
            public static readonly string LocalToCloud = "Synchronization/LocalToCloud";
            /// <summary>
            /// Get
            /// </summary>
            public static readonly string CouldToLocal = "Synchronization/CouldToLocal";
            
        }

        #endregion


        #region Collection
        public static class Collection
        {
            /// <summary>
            /// POST : Guid folderId,Guid planId
            /// </summary>
            public static readonly string CollectPlan = "Collection/CollectPlan";
            /// <summary>
            /// POST : Guid folderId,Guid planId
            /// </summary>
            public static readonly string UncollectPlan = "Collection/UncollectPlan";
            /// <summary>
            /// GET : Guid reqeustAccountId, Guid purposeAccountId
            /// </summary>
            public static readonly string GetCollectionFolders = "Collection/GetCollectionFolders";
        }
        #endregion
    }
}
