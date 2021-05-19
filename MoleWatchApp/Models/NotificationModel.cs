using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
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
        }

        public void UpdateNotification(NotificationData data)
        {
           //TODO opret forbindelse til API herfra og ændre indstilling for notifikation => kan muligvis undlades 
        }

        public string GetName()
        {
            patientModel = PatientModelSingleton.GetPatientModel();
            string collectionName = patientModel.CollectionOnPage.CollectionName;
            return collectionName;
        }
    }
}
