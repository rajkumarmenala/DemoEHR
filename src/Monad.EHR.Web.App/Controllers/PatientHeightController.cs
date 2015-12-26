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
    public class PatientHeightController:Controller
    {
        private IPatientHeightService _patientHeightService;
        public PatientHeightController(IPatientHeightService patientHeightService)
        {
            _patientHeightService = patientHeightService;
        }

        [HttpPost]
        [Route("AddPatientHeight")]
        public IActionResult AddPatientHeight([FromBody]PatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
				var patientHeight = Mapper.Map<PatientHeightViewModel, PatientHeight>(model);
				patientHeight.CreatedDateUtc = System.DateTime.UtcNow;
				patientHeight.LastModifiedDateUtc = System.DateTime.UtcNow;
                patientHeight.LastModifiedBy = 1;
                _patientHeightService.AddPatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditPatientHeight")]
        public IActionResult EditPatientHeight([FromBody]EditPatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
				var patientHeight = Mapper.Map<EditPatientHeightViewModel, PatientHeight>(model);
				patientHeight.LastModifiedDateUtc = System.DateTime.UtcNow;
                patientHeight.LastModifiedBy = 1;
                _patientHeightService.EditPatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeletePatientHeight")]
        public IActionResult DeletePatientHeight([FromBody]EditPatientHeightViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var patientHeight = Mapper.Map<EditPatientHeightViewModel, PatientHeight>(model);
                _patientHeightService.DeletePatientHeight(patientHeight);
            }
            return new HttpStatusCodeResult(200);
        }

		[HttpGet]
        [Route("GetAllPatientHeights")]
        public IEnumerable<PatientHeight> GetAllPatientHeights()
        {
            return _patientHeightService.GetAllPatientHeight();
        }

        [HttpGet]
        [Route("GetPatientHeight")]
        public PatientHeight GetPatientHeight(int patientHeightId)
        {
            return _patientHeightService.GetPatientHeightById(patientHeightId);
        }

		
		[HttpGet]
        [Route("GetPatientHeightForPatient")]
        public IEnumerable<PatientHeight> GetPatientHeightForPatient(int patientId)
        {
            return _patientHeightService.GetAllPatientHeightByPatientId(patientId);
        }

    }
}
