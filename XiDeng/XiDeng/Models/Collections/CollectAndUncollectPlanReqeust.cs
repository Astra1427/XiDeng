using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Models.Collections
{
    public class CollectAndUncollectPlanReqeust
    {
        public Guid PlanId { get; set; }
        public IEnumerable<Guid> CollectFolderIds { get; set; }
        public IEnumerable<Guid> UncollectFolderIds { get; set; }
    }
}
