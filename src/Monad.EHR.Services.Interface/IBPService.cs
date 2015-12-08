using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IBPService:IService
    {
        void AddBP(BP bP);
        void EditBP(BP bP);
        void DeleteBP(BP bP);
		IList<BP> GetAllBP();
        BP GetBPById(int id);
		
        IList<BP> GetAllBPByPatientId(int PatientID);
    }
}
