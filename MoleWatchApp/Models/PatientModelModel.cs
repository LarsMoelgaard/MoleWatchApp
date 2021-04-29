using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;

namespace MoleWatchApp.Models
{
    public class PatientModelModel
    {
        public PatientInfoDTO CurrentPatient { get; private set; }
        public IAPIService api;


        public PatientModelModel(PatientInfoDTO CurrentPatientInfo)
        {
            this.CurrentPatient = CurrentPatientInfo;
            api = APISingleton.GetAPI();
        }




    }

}
