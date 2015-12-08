using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class MedicationsService : IMedicationsService
    {
        private IMedicationsRepository _repository;
        public MedicationsService(IMedicationsRepository repository)
        {
            _repository = repository;
        }
        public void AddMedications(Medications medications)
        {
            medications.LastModifiedDateUtc = DateTime.UtcNow;
            medications.CreatedDateUtc = DateTime.UtcNow;
            medications.LastModifiedBy = 1;
            _repository.Create(medications);
        }

        public void DeleteMedications(Medications medications)
        {
            _repository.Delete(medications);
        }

        public void EditMedications(Medications medications)
        {
            medications.LastModifiedDateUtc = DateTime.UtcNow;
            medications.CreatedDateUtc = DateTime.UtcNow;
            medications.LastModifiedBy = 1;
            _repository.Update(medications);
        }

		public  IList<Medications> GetAllMedications()
		{
            return _repository.GetAll().ToList();
        }

        public  Medications GetMedicationsById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<Medications> GetAllMedicationsByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
