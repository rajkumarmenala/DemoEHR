using AutoMapper;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Web.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App
{
    public class AutoMapperBootStrapper
    {
        public static void Bootstrap()
        {
            ConfigurePageMappings();
        }

        private static void ConfigurePageMappings()
        {
            MapperModel<Patient, PatientViewModel>();
            MapperModel<Patient, EditPatientViewModel>();

            MapperModel<Address, AddressViewModel>();
            MapperModel<Address, EditAddressViewModel>();

            MapperModel<BP, BPViewModel>();
            MapperModel<BP, EditBPViewModel>();

            MapperModel<Medications, MedicationsViewModel>();
            MapperModel<Medications, EditMedicationsViewModel>();

            MapperModel<PatientHeight, PatientHeightViewModel>();
            MapperModel<PatientHeight, EditPatientHeightViewModel>();

            MapperModel<Problems, ProblemsViewModel>();
            MapperModel<Problems, EditProblemsViewModel>();

            MapperModel<Weight, WeightViewModel>();
            MapperModel<Weight, EditWeightViewModel>();
            


        }
        private static void MapperModel<T, Z>()
        {
            Mapper.CreateMap<T, Z>();
            Mapper.CreateMap<Z, T>();
        }
    }

}
