using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IMedicationsService:IService
    {
        void AddMedications(Medications medications);
        void EditMedications(Medications medications);
        void DeleteMedications(Medications medications);
		IList<Medications> GetAllMedications();
        Medications GetMedicationsById(int id);
		
        IList<Medications> GetAllMedicationsByPatientId(int PatientID);
    }
}
