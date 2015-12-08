using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Services.Business
{
    public class ProblemsService : IProblemsService
    {
        private IProblemsRepository _repository;
        public ProblemsService(IProblemsRepository repository)
        {
            _repository = repository;
        }
        public void AddProblems(Problems problems)
        {
            problems.LastModifiedDateUtc = DateTime.UtcNow;
            problems.CreatedDateUtc = DateTime.UtcNow;
            problems.LastModifiedBy = 1;
            _repository.Create(problems);
        }

        public void DeleteProblems(Problems problems)
        {
            _repository.Delete(problems);
        }

        public void EditProblems(Problems problems)
        {
            problems.LastModifiedDateUtc = DateTime.UtcNow;
            problems.CreatedDateUtc = DateTime.UtcNow;
            problems.LastModifiedBy = 1;
            _repository.Update(problems);
        }

		public  IList<Problems> GetAllProblems()
		{
            return _repository.GetAll().ToList();
        }

        public  Problems GetProblemsById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

           
        public IList<Problems> GetAllProblemsByPatientId(int PatientID)
	    {
		   return _repository.GetAll().Where(x => x.Id == PatientID).ToList();
	    }

       
    }
}
