using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IAddressService:IService
    {
        void AddAddress(Address address);
        void EditAddress(Address address);
        void DeleteAddress(Address address);
		IList<Address> GetAllAddress();
        Address GetAddressById(int id);
		
        IList<Address> GetAllAddressByPatientId(int PatientID);
    }
}
