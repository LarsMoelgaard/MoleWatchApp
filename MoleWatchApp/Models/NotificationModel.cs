﻿using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;

namespace MoleWatchApp.Models
{
    /// <summary>
    /// Model klasse for notifikationViewModel 
    /// </summary>
    class NotificationModel
    {
        /// <summary>
        /// Reference til api-klassen fra Nuget-Pakken
        /// </summary>
        public IAPIService api { get; }

        /// <summary>
        /// Reference til patientmodellen 
        /// </summary>
        private IPatientModel patientModel;

        public NotificationModel()
        {
            api = APISingleton.GetAPI();
            patientModel = PatientModelSingleton.GetPatientModel();
        }

        /// <summary>
        /// Metoden ændre indstillinger fra notifikation for et valgt modermærke
        /// </summary>
        /// <param name="data"></param>
        public void UpdateNotification(NotificationData data)
        {
           //TODO opret forbindelse til API herfra og ændre indstilling for notifikation => kan muligvis undlades 
        }

        /// <summary>
        /// Metoden henter den valgte collections data 
        /// </summary>
        /// <returns></returns>
        public CollectionDTO GetCurrentCollection()
        {
            CollectionDTO collection = patientModel.CollectionOnPage;
            return collection;
        }
    }
}
