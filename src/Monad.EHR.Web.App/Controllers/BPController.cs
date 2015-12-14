using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{

    [Route("api/[controller]")]
    public class BPController : Controller
    {
        private IBPService _bPService;
        public BPController(IBPService bPService)
        {
            _bPService = bPService;
        }


        [HttpPost]
        [Route("AddBP")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.AddBP")]

        public IActionResult AddBP([FromBody]BPViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bP = new BP
                {
                    Systolic = model.Systolic,
                    Diastolic = model.Diastolic,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    CreatedDateUtc = System.DateTime.UtcNow,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _bPService.AddBP(bP);
            }
            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        [Route("EditBP")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.EditBP")]
        public IActionResult EditBP([FromBody]EditBPViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bP = new BP
                {
                    Id = model.Id,
                    Systolic = model.Systolic,
                    Diastolic = model.Diastolic,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _bPService.EditBP(bP);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeleteBP")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.DeleteBP")]
        public IActionResult DeleteBP([FromBody]EditBPViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bP = new BP
                {
                    Id = model.Id,
                    Systolic = model.Systolic,
                    Diastolic = model.Diastolic,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _bPService.DeleteBP(bP);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        [Route("GetAllBPs")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.GetAllBPs")]
        public IEnumerable<BP> GetAllBPs()
        {
            return _bPService.GetAllBP();
        }

        [HttpGet]
        [Route("GetBP")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.GetBP")]
        public BP GetBP(int bPId)
        {
            return _bPService.GetBPById(bPId);
        }

        [HttpGet]
        [Route("GetBPForPatient")]
        [Authorize(Policy = "TokenAuth", Roles = "BP.GetBPForPatient")]
        public IEnumerable<BP> GetBPForPatient(int patientId)
        {
            return _bPService.GetAllBPByPatientId(patientId);
        }

    }
}
