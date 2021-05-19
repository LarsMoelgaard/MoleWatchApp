using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    class NotificationModel
    {
        public IAPIService api { get; }
        private IPatientModel patientModel;

        public NotificationModel()
        {
            api = APISingleton.GetAPI();
            patientModel = PatientModelSingleton.GetPatientModel();
        }

        public void UpdateNotification(NotificationData data)
        {
           //TODO opret forbindelse til API herfra og ændre indstilling for notifikation => kan muligvis undlades 
        }

        public CollectionDTO GetCurrentCollection()
        {
            CollectionDTO collection = patientModel.CollectionOnPage;
            return collection;
        }
    }
}
