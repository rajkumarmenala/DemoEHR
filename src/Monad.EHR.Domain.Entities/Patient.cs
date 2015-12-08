using System;
using System.Collections.Generic;

namespace Monad.EHR.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
public string LastName { get; set; }
public DateTime DOB { get; set; }
public string SSN { get; set; }
public string Email { get; set; }
public string Phone { get; set; }


        public DateTime CreatedDateUtc { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
