
using System;

namespace Monad.EHR.Domain.Entities
{
    public class ActivityRole : BaseEntity
    {
        public int ActivityID { get; set; }
        public string RoleID { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
