using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _repository;
        public AddressService(IAddressRepository repository)
        {
            _repository = repository;
        }
        public void AddAddress(Address address)
        {
            address.LastModifiedDateUtc = DateTime.UtcNow;
            address.CreatedDateUtc = DateTime.UtcNow;
            address.LastModifiedBy = 1;
            _repository.Create(address);
        }

        public void DeleteAddress(Address address)
        {
            _repository.Delete(address);
        }

        public void EditAddress(Address address)
        {
            address.LastModifiedDateUtc = DateTime.UtcNow;
            address.CreatedDateUtc = DateTime.UtcNow;
            address.LastModifiedBy = 1;
            _repository.Update(address);
        }

		public  IList<Address> GetAllAddress()
		{
            return _repository.GetAll().ToList();
        }

        public  Address GetAddressById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<Address> GetAllAddressByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
