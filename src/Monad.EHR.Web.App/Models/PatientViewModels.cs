using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class PatientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }

    public class EditPatientViewModel : PatientViewModel
    {
        [Required]
        [Display(Name = "Patient Id")]
        public int Id { get; set; }
    }

    public class DeletePatientViewModel : PatientViewModel
    {
        [Required]
        [Display(Name = "Patient Id")]
        public int Id { get; set; }
    }
}
