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
    public class WeightController:Controller
    {
        private IWeightService _weightService;
        public WeightController(IWeightService weightService)
        {
            _weightService = weightService;
        }

        [HttpPost]
        [Route("AddWeight")]
        public IActionResult AddWeight([FromBody]WeightViewModel model)
        {
            if (ModelState.IsValid)
            {
				var weight = Mapper.Map<WeightViewModel, Weight>(model);
				weight.CreatedDateUtc = System.DateTime.UtcNow;
				weight.LastModifiedDateUtc = System.DateTime.UtcNow;
                weight.LastModifiedBy = 1;
                _weightService.AddWeight(weight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditWeight")]
        public IActionResult EditWeight([FromBody]EditWeightViewModel model)
        {
            if (ModelState.IsValid)
            {
				var weight = Mapper.Map<EditWeightViewModel, Weight>(model);
				weight.LastModifiedDateUtc = System.DateTime.UtcNow;
                weight.LastModifiedBy = 1;
                _weightService.EditWeight(weight);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeleteWeight")]
        public IActionResult DeleteWeight([FromBody]EditWeightViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var weight = Mapper.Map<EditWeightViewModel, Weight>(model);
                _weightService.DeleteWeight(weight);
            }
            return new HttpStatusCodeResult(200);
        }

		[HttpGet]
        [Route("GetAllWeights")]
        public IEnumerable<Weight> GetAllWeights()
        {
            return _weightService.GetAllWeight();
        }

        [HttpGet]
        [Route("GetWeight")]
        public Weight GetWeight(int weightId)
        {
            return _weightService.GetWeightById(weightId);
        }

		
		[HttpGet]
        [Route("GetWeightForPatient")]
        public IEnumerable<Weight> GetWeightForPatient(int patientId)
        {
            return _weightService.GetAllWeightByPatientId(patientId);
        }

    }
}
