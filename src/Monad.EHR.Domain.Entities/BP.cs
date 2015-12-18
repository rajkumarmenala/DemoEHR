using System;
using System.Collections.Generic;

namespace Monad.EHR.Domain.Entities
{
    public class BP : BaseEntity
    {
        public int Systolic { get; set; }
public int Diastolic { get; set; }
public DateTime Date { get; set; }
public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
