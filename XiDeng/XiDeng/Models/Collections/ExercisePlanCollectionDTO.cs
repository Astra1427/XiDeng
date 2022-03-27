using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Models.Collections
{
    public class ExercisePlanCollectionDTO:ModelBase
    {
        public Guid ExercisePlanId { get; set; }
        public Guid CollectionFolderId { get; set; }
    }
}
