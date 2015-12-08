using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IWeightService:IService
    {
        void AddWeight(Weight weight);
        void EditWeight(Weight weight);
        void DeleteWeight(Weight weight);
		IList<Weight> GetAllWeight();
        Weight GetWeightById(int id);
		
        IList<Weight> GetAllWeightByPatientId(int PatientID);
    }
}
