using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Models;

namespace MoleWatchApp.Interfaces
{
    public interface IPatientModel
    {
         PatientInfoDTO CurrentPatient { get; set; }
         PatientDataDTO CurrentPatientData { get; set; }
         CollectionDTO CollectionOnPage { get; }


        IAPIService api { get; }



        void LoadExistingCollection(CollectionDTO Collection);


        int LoadNewCollection(CollectionDTO Collection);

        void UpdateCollection(CollectionDTO collection);

        void RemoveCollection();

    }
}
