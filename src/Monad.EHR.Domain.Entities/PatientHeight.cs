using System;

namespace Monad.EHR.Domain.Entities
{
    public class PatientHeight : BaseEntity
    {
        public Decimal Height { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
