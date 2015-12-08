using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Models
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Designation { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ProfilePicture { get; set; }
        
    }
}
