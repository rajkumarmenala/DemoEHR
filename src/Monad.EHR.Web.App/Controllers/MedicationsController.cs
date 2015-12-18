using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Collections.Generic;
using AutoMapper;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;

namespace Monad.EHR.Web.App.Controllers
{
    [Route("api/[controller]")]
    public class MedicationsController:Controller
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
				var medications = Mapper.Map<MedicationsViewModel, Medications>(model);
				medications.CreatedDateUtc = System.DateTime.UtcNow;
				medications.LastModifiedDateUtc = System.DateTime.UtcNow;
                medications.LastModifiedBy = 1;
                _medicationsService.AddMedications(medications);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditMedications")]
        public IActionResult EditMedications([FromBody]EditMedicationsViewModel model)
        {
            if (ModelState.IsValid)
            {
				var medications = Mapper.Map<EditMedicationsViewModel, Medications>(model);
				medications.LastModifiedDateUtc = System.DateTime.UtcNow;
                medications.LastModifiedBy = 1;
                _medicationsService.EditMedications(medications);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeleteMedications")]
        public IActionResult DeleteMedications([FromBody]EditMedicationsViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var medications = Mapper.Map<EditMedicationsViewModel, Medications>(model);
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
