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
    public class PatientController : Controller
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
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB = model.DOB,
                    SSN = model.SSN,
                    Email = model.Email,
                    Phone = model.Phone,

                    CreatedDateUtc = System.DateTime.UtcNow,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
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
                var patient = new Patient
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB = model.DOB,
                    SSN = model.SSN,
                    Email = model.Email,
                    Phone = model.Phone,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
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
                var patient = new Patient
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB = model.DOB,
                    SSN = model.SSN,
                    Email = model.Email,
                    Phone = model.Phone,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
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
