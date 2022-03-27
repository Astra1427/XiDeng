using System;

namespace XiDeng.Models.Collections
{
    public class ExercisePlanCollectionDTO:ModelBase
    {
        public Guid ExercisePlanId { get; set; }
        public Guid CollectionFolderId { get; set; }
    }
}
