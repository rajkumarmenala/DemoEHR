using System;
using System.Collections.Generic;

namespace Monad.EHR.Domain.Entities
{
    public class Medications : BaseEntity
    {
        public string Name { get; set; }
public int Quantity { get; set; }
public DateTime BeginDate { get; set; }
public DateTime EndDate { get; set; }
public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
