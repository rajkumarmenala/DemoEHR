using System;

namespace Monad.EHR.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}

