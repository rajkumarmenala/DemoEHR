using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class PatientHeightService : IPatientHeightService
    {
        private IPatientHeightRepository _repository;
        public PatientHeightService(IPatientHeightRepository repository)
        {
            _repository = repository;
        }
        public void AddPatientHeight(PatientHeight patientHeight)
        {
            patientHeight.LastModifiedDateUtc = DateTime.UtcNow;
            patientHeight.CreatedDateUtc = DateTime.UtcNow;
            patientHeight.LastModifiedBy = 1;
            _repository.Create(patientHeight);
        }

        public void DeletePatientHeight(PatientHeight patientHeight)
        {
            _repository.Delete(patientHeight);
        }

        public void EditPatientHeight(PatientHeight patientHeight)
        {
            patientHeight.LastModifiedDateUtc = DateTime.UtcNow;
            patientHeight.CreatedDateUtc = DateTime.UtcNow;
            patientHeight.LastModifiedBy = 1;
            _repository.Update(patientHeight);
        }

		public  IList<PatientHeight> GetAllPatientHeight()
		{
            return _repository.GetAll().ToList();
        }

        public  PatientHeight GetPatientHeightById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<PatientHeight> GetAllPatientHeightByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
