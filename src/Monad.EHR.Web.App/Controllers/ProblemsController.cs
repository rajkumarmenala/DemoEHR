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
    public class ProblemsController:Controller
    {
        private IProblemsService _problemsService;
        public ProblemsController(IProblemsService problemsService)
        {
            _problemsService = problemsService;
        }

        [HttpPost]
        [Route("AddProblems")]
        public IActionResult AddProblems([FromBody]ProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
				var problems = Mapper.Map<ProblemsViewModel, Problems>(model);
				problems.CreatedDateUtc = System.DateTime.UtcNow;
				problems.LastModifiedDateUtc = System.DateTime.UtcNow;
                problems.LastModifiedBy = 1;
                _problemsService.AddProblems(problems);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditProblems")]
        public IActionResult EditProblems([FromBody]EditProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
				var problems = Mapper.Map<EditProblemsViewModel, Problems>(model);
				problems.LastModifiedDateUtc = System.DateTime.UtcNow;
                problems.LastModifiedBy = 1;
                _problemsService.EditProblems(problems);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeleteProblems")]
        public IActionResult DeleteProblems([FromBody]EditProblemsViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var problems = Mapper.Map<EditProblemsViewModel, Problems>(model);
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
