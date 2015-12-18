using System;
using System.Collections.Generic;

namespace Monad.EHR.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Line1 { get; set; }
public string Line2 { get; set; }
public string City { get; set; }
public string State { get; set; }
public string Zip { get; set; }
public DateTime BeginDate { get; set; }
public DateTime EndDate { get; set; }
public int PatientID { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
