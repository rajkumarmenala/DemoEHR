using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class WeightService : IWeightService
    {
        private IWeightRepository _repository;
        public WeightService(IWeightRepository repository)
        {
            _repository = repository;
        }
        public void AddWeight(Weight weight)
        {
            weight.LastModifiedDateUtc = DateTime.UtcNow;
            weight.CreatedDateUtc = DateTime.UtcNow;
            weight.LastModifiedBy = 1;
            _repository.Create(weight);
        }

        public void DeleteWeight(Weight weight)
        {
            _repository.Delete(weight);
        }

        public void EditWeight(Weight weight)
        {
            weight.LastModifiedDateUtc = DateTime.UtcNow;
            weight.CreatedDateUtc = DateTime.UtcNow;
            weight.LastModifiedBy = 1;
            _repository.Update(weight);
        }

		public  IList<Weight> GetAllWeight()
		{
            return _repository.GetAll().ToList();
        }

        public  Weight GetWeightById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<Weight> GetAllWeightByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
