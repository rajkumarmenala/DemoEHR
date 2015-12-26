using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Collections.Generic;
using AutoMapper;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;

namespace Monad.EHR.Web.App.Controllers
{
    [Route("api/[controller]")]
	[Authorize(Policy = "Bearer")]
    public class PatientController:Controller
    {
        private IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Route("AddPatient")]
        public IActionResult AddPatient([FromBody]PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
				var patient = Mapper.Map<PatientViewModel, Patient>(model);
				patient.CreatedDateUtc = System.DateTime.UtcNow;
				patient.LastModifiedDateUtc = System.DateTime.UtcNow;
                patient.LastModifiedBy = 1;
                _patientService.AddPatient(patient);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditPatient")]
        public IActionResult EditPatient([FromBody]EditPatientViewModel model)
        {
            if (ModelState.IsValid)
            {
				var patient = Mapper.Map<EditPatientViewModel, Patient>(model);
				patient.LastModifiedDateUtc = System.DateTime.UtcNow;
                patient.LastModifiedBy = 1;
                _patientService.EditPatient(patient);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeletePatient")]
        public IActionResult DeletePatient([FromBody]EditPatientViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var patient = Mapper.Map<EditPatientViewModel, Patient>(model);
                _patientService.DeletePatient(patient);
            }
            return new HttpStatusCodeResult(200);
        }

		[HttpGet]
        [Route("GetAllPatients")]
        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientService.GetAllPatient();
        }

        [HttpGet]
        [Route("GetPatient")]
        public Patient GetPatient(int patientId)
        {
            return _patientService.GetPatientById(patientId);
        }

		

    }
}
