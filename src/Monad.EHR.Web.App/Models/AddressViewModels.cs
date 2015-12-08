using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class AddressViewModel
    {
         public string Line1 { get; set; }
public string Line2 { get; set; }
public string City { get; set; }
public string State { get; set; }
public string Zip { get; set; }
public DateTime BeginDate { get; set; }
public DateTime EndDate { get; set; }
public int PatientID { get; set; }

    }

    public class EditAddressViewModel : AddressViewModel
    {
        [Required]
        [Display(Name = "Address Id")]
        public int Id { get; set; }
    }

    public class DeleteAddressViewModel : AddressViewModel
    {
        [Required]
        [Display(Name = "Address Id")]
        public int Id { get; set; }
    }
}
