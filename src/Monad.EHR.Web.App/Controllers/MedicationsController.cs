using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{

    [Route("api/[controller]")]
    public class MedicationsController : Controller
    {
        private IMedicationsService _medicationsService;
        public MedicationsController(IMedicationsService medicationsService)
        {
            _medicationsService = medicationsService;
        }


        [HttpPost]
        [Route("AddMedications")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.AddMedications")]
        public IActionResult AddMedications([FromBody]MedicationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medications = new Medications
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    BeginDate = model.BeginDate,
                    EndDate = model.EndDate,
                    PatientID = model.PatientID,

                    CreatedDateUtc = System.DateTime.UtcNow,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _medicationsService.AddMedications(medications);
            }
            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("EditMedications")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.EditMedications")]
        public IActionResult EditMedications([FromBody]EditMedicationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medications = new Medications
                {
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = model.Quantity,
                    BeginDate = model.BeginDate,
                    EndDate = model.EndDate,
                    PatientID = model.PatientID,
                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _medicationsService.EditMedications(medications);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteMedications")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.DeleteMedications")]
        public IActionResult DeleteMedications([FromBody]EditMedicationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medications = new Medications
                {
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = model.Quantity,
                    BeginDate = model.BeginDate,
                    EndDate = model.EndDate,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _medicationsService.DeleteMedications(medications);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        [Route("GetAllMedicationss")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.GetAllMedicationss")]
        public IEnumerable<Medications> GetAllMedicationss()
        {
            return _medicationsService.GetAllMedications();
        }

        [HttpGet]
        [Route("GetMedications")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.GetMedications")]
        public Medications GetMedications(int medicationsId)
        {
            return _medicationsService.GetMedicationsById(medicationsId);
        }


        [HttpGet]
        [Route("GetMedicationsForPatient")]
        [Authorize(Policy = "TokenAuth", Roles = "Medications.GetMedicationsForPatient")]
        public IEnumerable<Medications> GetMedicationsForPatient(int patientId)
        {
            return _medicationsService.GetAllMedicationsByPatientId(patientId);
        }

    }
}
