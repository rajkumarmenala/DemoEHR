using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{
    [Route("api/[controller]")]
    public class ProblemsController : Controller
    {
        private IProblemsService _problemsService;
        public ProblemsController(IProblemsService problemsService)
        {
            _problemsService = problemsService;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("AddProblems")]
        public IActionResult AddProblems([FromBody]ProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var problems = new Problems
                {
                    Description = model.Description,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    CreatedDateUtc = System.DateTime.UtcNow,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _problemsService.AddProblems(problems);
            }
            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("EditProblems")]
        public IActionResult EditProblems([FromBody]EditProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var problems = new Problems
                {
                    Id = model.Id,
                    Description = model.Description,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _problemsService.EditProblems(problems);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteProblems")]
        public IActionResult DeleteProblems([FromBody]EditProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var problems = new Problems
                {
                    Id = model.Id,
                    Description = model.Description,
                    Date = model.Date,
                    PatientID = model.PatientID,

                    // CreatedDateUtc = model.CreatedDateUtc,
                    LastModifiedDateUtc = System.DateTime.UtcNow,
                    LastModifiedBy = 1
                };
                _problemsService.DeleteProblems(problems);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        [Route("GetAllProblemss")]
        public IEnumerable<Problems> GetAllProblemss()
        {
            return _problemsService.GetAllProblems();
        }

        [HttpGet]
        [Route("GetProblems")]
        public Problems GetProblems(int problemsId)
        {
            return _problemsService.GetProblemsById(problemsId);
        }


        [HttpGet]
        [Route("GetProblemsForPatient")]
        public IEnumerable<Problems> GetProblemsForPatient(int patientId)
        {
            return _problemsService.GetAllProblemsByPatientId(patientId);
        }

    }
}
