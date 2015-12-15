using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{

    [Route("api/[controller]")]
    [Authorize(Policy = "TokenAuth")]
    public class MedicationsController : Controller
    {
        private IMedicationsService _medicationsService;
        public MedicationsController(IMedicationsService medicationsService)
        {
            _medicationsService = medicationsService;
        }

        [HttpPost]
        [Route("AddMedications")]
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
        public IEnumerable<Medications> GetAllMedicationss()
        {
            return _medicationsService.GetAllMedications();
        }

        [HttpGet]
        [Route("GetMedications")]
        public Medications GetMedications(int medicationsId)
        {
            return _medicationsService.GetMedicationsById(medicationsId);
        }


        [HttpGet]
        [Route("GetMedicationsForPatient")]
        public IEnumerable<Medications> GetMedicationsForPatient(int patientId)
        {
            return _medicationsService.GetAllMedicationsByPatientId(patientId);
        }

    }
}
