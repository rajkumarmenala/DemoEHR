using System;

namespace Monad.EHR.Domain.Entities
{
    public class Resource : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ResourceTypeId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}


