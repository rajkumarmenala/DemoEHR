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
    public class AddressController:Controller
    {
        private IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [Route("AddAddress")]
        public IActionResult AddAddress([FromBody]AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
				var address = Mapper.Map<AddressViewModel, Address>(model);
				address.CreatedDateUtc = System.DateTime.UtcNow;
				address.LastModifiedDateUtc = System.DateTime.UtcNow;
                address.LastModifiedBy = 1;
                _addressService.AddAddress(address);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("EditAddress")]
        public IActionResult EditAddress([FromBody]EditAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
				var address = Mapper.Map<EditAddressViewModel, Address>(model);
				address.LastModifiedDateUtc = System.DateTime.UtcNow;
                address.LastModifiedBy = 1;
                _addressService.EditAddress(address);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("DeleteAddress")]
        public IActionResult DeleteAddress([FromBody]EditAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
			    var address = Mapper.Map<EditAddressViewModel, Address>(model);
                _addressService.DeleteAddress(address);
            }
            return new HttpStatusCodeResult(200);
        }

		[HttpGet]
        [Route("GetAllAddresss")]
        public IEnumerable<Address> GetAllAddresss()
        {
            return _addressService.GetAllAddress();
        }

        [HttpGet]
        [Route("GetAddress")]
        public Address GetAddress(int addressId)
        {
            return _addressService.GetAddressById(addressId);
        }

		
		[HttpGet]
        [Route("GetAddressForPatient")]
        public IEnumerable<Address> GetAddressForPatient(int patientId)
        {
            return _addressService.GetAllAddressByPatientId(patientId);
        }

    }
}
