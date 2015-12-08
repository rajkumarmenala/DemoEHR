using System;
using System.Collections.Generic;

namespace Monad.EHR.Domain.Entities
{
    public class Weight : BaseEntity
    {
        public DateTime Date { get; set; }
public Decimal Wt { get; set; }
public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
