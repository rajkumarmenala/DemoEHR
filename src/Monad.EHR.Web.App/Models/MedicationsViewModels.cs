using System;
using System.ComponentModel.DataAnnotations;

namespace Monad.EHR.Web.App.Models
{
    public class MedicationsViewModel
    {
         public string Name { get; set; }
public int Quantity { get; set; }
public DateTime BeginDate { get; set; }
public DateTime EndDate { get; set; }
public int PatientID { get; set; }

    }

    public class EditMedicationsViewModel : MedicationsViewModel
    {
        [Required]
        [Display(Name = "Medications Id")]
        public int Id { get; set; }
    }

    public class DeleteMedicationsViewModel : MedicationsViewModel
    {
        [Required]
        [Display(Name = "Medications Id")]
        public int Id { get; set; }
    }
}
