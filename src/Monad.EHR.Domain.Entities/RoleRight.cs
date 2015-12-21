using System;

namespace Monad.EHR.Domain.Entities
{
    public class RoleRight:BaseEntity
    {
        public string RoleId { get; set; }
        public int ActivityId { get; set; }
        public int ResourceId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
