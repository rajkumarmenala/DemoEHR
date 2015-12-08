using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IPatientHeightService:IService
    {
        void AddPatientHeight(PatientHeight patientHeight);
        void EditPatientHeight(PatientHeight patientHeight);
        void DeletePatientHeight(PatientHeight patientHeight);
		IList<PatientHeight> GetAllPatientHeight();
        PatientHeight GetPatientHeightById(int id);
		
        IList<PatientHeight> GetAllPatientHeightByPatientId(int PatientID);
    }
}
