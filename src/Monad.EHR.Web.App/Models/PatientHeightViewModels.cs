using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class PatientHeightViewModel
    {
         public Decimal Height { get; set; }
public DateTime Date { get; set; }
public int PatientID { get; set; }

    }

    public class EditPatientHeightViewModel : PatientHeightViewModel
    {
        [Required]
        [Display(Name = "PatientHeight Id")]
        public int Id { get; set; }
    }

    public class DeletePatientHeightViewModel : PatientHeightViewModel
    {
        [Required]
        [Display(Name = "PatientHeight Id")]
        public int Id { get; set; }
    }
}
