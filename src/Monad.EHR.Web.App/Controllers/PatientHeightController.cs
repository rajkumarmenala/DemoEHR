using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{

    [Route("api/[controller]")]
    public class PatientHeightController : Controller
    {
        private IPatientHeightService _patientHeightService;
        public PatientHeightController(IPatientHeightService patientHeightService)
        {
            _patientHeightService = patientHeightService;
        }


        [HttpPost]
        [Route("AddPatientHeight")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.AddPatientHeight")]
        public IActionResult AddPatientHeight([FromBody]PatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patientHeight = new PatientHeight
                {
                    Height = model.Height,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    CreatedDateUtc = System.DateTime.UtcNow,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _patientHeightService.AddPatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        [Route("EditPatientHeight")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.EditPatientHeight")]
        public IActionResult EditPatientHeight([FromBody]EditPatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patientHeight = new PatientHeight
                {
                    Id = model.Id,
                    Height = model.Height,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _patientHeightService.EditPatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeletePatientHeight")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.DeletePatientHeight")]
        public IActionResult DeletePatientHeight([FromBody]EditPatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patientHeight = new PatientHeight
                {
                    Id = model.Id,
                    Height = model.Height,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _patientHeightService.DeletePatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        [Route("GetAllPatientHeights")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.GetAllPatientHeights")]
        public IEnumerable<PatientHeight> GetAllPatientHeights()
        {
            return _patientHeightService.GetAllPatientHeight();
        }

        [HttpGet]
        [Route("GetPatientHeight")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.GetPatientHeight")]

        public PatientHeight GetPatientHeight(int patientHeightId)
        {
            return _patientHeightService.GetPatientHeightById(patientHeightId);
        }


        [HttpGet]
        [Route("GetPatientHeightForPatient")]
        [Authorize(Policy = "TokenAuth", Roles = "PatientHeight.GetPatientHeightForPatient")]
        public IEnumerable<PatientHeight> GetPatientHeightForPatient(int patientId)
        {
            return _patientHeightService.GetAllPatientHeightByPatientId(patientId);
        }

    }
}
