using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IProblemsService:IService
    {
        void AddProblems(Problems problems);
        void EditProblems(Problems problems);
        void DeleteProblems(Problems problems);
		IList<Problems> GetAllProblems();
        Problems GetProblemsById(int id);
		
        IList<Problems> GetAllProblemsByPatientId(int PatientID);
    }
}
