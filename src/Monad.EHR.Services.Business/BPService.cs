using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class BPService : IBPService
    {
        private IBPRepository _repository;
        public BPService(IBPRepository repository)
        {
            _repository = repository;
        }
        public void AddBP(BP bP)
        {
            bP.LastModifiedDateUtc = DateTime.UtcNow;
            bP.CreatedDateUtc = DateTime.UtcNow;
            bP.LastModifiedBy = 1;
            _repository.Create(bP);
        }

        public void DeleteBP(BP bP)
        {
            _repository.Delete(bP);
        }

        public void EditBP(BP bP)
        {
            bP.LastModifiedDateUtc = DateTime.UtcNow;
            bP.CreatedDateUtc = DateTime.UtcNow;
            bP.LastModifiedBy = 1;
            _repository.Update(bP);
        }

		public  IList<BP> GetAllBP()
		{
            return _repository.GetAll().ToList();
        }

        public  BP GetBPById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<BP> GetAllBPByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
