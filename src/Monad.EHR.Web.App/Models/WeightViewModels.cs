using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class WeightViewModel
    {
         public DateTime Date { get; set; }
public Decimal Wt { get; set; }
public int PatientID { get; set; }

    }

    public class EditWeightViewModel : WeightViewModel
    {
        [Required]
        [Display(Name = "Weight Id")]
        public int Id { get; set; }
    }

    public class DeleteWeightViewModel : WeightViewModel
    {
        [Required]
        [Display(Name = "Weight Id")]
        public int Id { get; set; }
    }
}
