using System;

namespace Monad.EHR.Domain.Entities
{
    public class UserActivity : BaseEntity
    {
        public int ActivityID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}

