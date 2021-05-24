using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    /// <summary>
    /// Model kalsse for ContactDoctor viewmodel 
    /// </summary>
    public class ContactDoctorModel
    {
        private IAPIService api;
        private IPatientModel patientModel;

        public ContactDoctorModel()
        {
            api = APISingleton.GetAPI();
            patientModel = PatientModelSingleton.GetPatientModel();
        }


        /// <summary>
        /// Metoden henter information på brugerens tilknyttede doktor 
        /// </summary>
        /// <returns></returns>
        public DoctorContactInfoDTO GetDoctorInfo()
        {
            DoctorContactInfoRequestDTO doctorRequest = new DoctorContactInfoRequestDTO();
            doctorRequest.DoctorContactID = patientModel.CurrentPatient.clinic;

            DoctorContactInfoDTO doctorinfo = new DoctorContactInfoDTO();

            doctorinfo = api.GetObject<DoctorContactInfoDTO,DoctorContactInfoRequestDTO>("GetMedicalPracticeContactInfo", doctorRequest);
            return doctorinfo;
        }
    }
}
