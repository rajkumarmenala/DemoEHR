using System;

namespace Monad.EHR.Domain.Entities
{
    public class Problems : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
