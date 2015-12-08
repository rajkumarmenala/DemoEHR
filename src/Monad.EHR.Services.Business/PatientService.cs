using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class PatientService : IPatientService
    {
        private IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }
        public void AddPatient(Patient patient)
        {
            patient.LastModifiedDateUtc = DateTime.UtcNow;
            patient.CreatedDateUtc = DateTime.UtcNow;
            patient.LastModifiedBy = 1;
            _repository.Create(patient);
        }

        public void DeletePatient(Patient patient)
        {
            _repository.Delete(patient);
        }

        public void EditPatient(Patient patient)
        {
            patient.LastModifiedDateUtc = DateTime.UtcNow;
            patient.CreatedDateUtc = DateTime.UtcNow;
            patient.LastModifiedBy = 1;
            _repository.Update(patient);
        }

		public  IList<Patient> GetAllPatient()
		{
            return _repository.GetAll().ToList();
        }

        public  Patient GetPatientById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        

       
    }
}
