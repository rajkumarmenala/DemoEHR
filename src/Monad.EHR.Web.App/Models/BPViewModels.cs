using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class BPViewModel
    {
         public int Systolic { get; set; }
public int Diastolic { get; set; }
public DateTime Date { get; set; }
public int PatientID { get; set; }

    }

    public class EditBPViewModel : BPViewModel
    {
        [Required]
        [Display(Name = "BP Id")]
        public int Id { get; set; }
    }

    public class DeleteBPViewModel : BPViewModel
    {
        [Required]
        [Display(Name = "BP Id")]
        public int Id { get; set; }
    }
}
