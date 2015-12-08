using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IPatientService:IService
    {
        void AddPatient(Patient patient);
        void EditPatient(Patient patient);
        void DeletePatient(Patient patient);
		IList<Patient> GetAllPatient();
        Patient GetPatientById(int id);
		
    }
}
