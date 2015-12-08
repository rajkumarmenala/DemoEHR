using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class ProblemsViewModel
    {
         public string Description { get; set; }
public DateTime Date { get; set; }
public int PatientID { get; set; }

    }

    public class EditProblemsViewModel : ProblemsViewModel
    {
        [Required]
        [Display(Name = "Problems Id")]
        public int Id { get; set; }
    }

    public class DeleteProblemsViewModel : ProblemsViewModel
    {
        [Required]
        [Display(Name = "Problems Id")]
        public int Id { get; set; }
    }
}
