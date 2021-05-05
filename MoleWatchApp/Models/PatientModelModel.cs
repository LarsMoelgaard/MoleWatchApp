using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class PatientModelModel : IPatientModel
    {
        public PatientInfoDTO CurrentPatient { get; set; }
        public CollectionDTO CollectionOnPage { get; private set; }
        public IAPIService api { get; }



        public PatientModelModel()
        {
            api = APISingleton.GetAPI();
        }

        public void LoadExistingCollection(CollectionDTO Collection)
        {
            CollectionRequestDTO collectionRequest = new CollectionRequestDTO();

            collectionRequest.CollectionID = Collection.CollectionID;

            collectionRequest.PatientID = CurrentPatient.PatientID;

            CollectionOnPage = api.GetObject<CollectionDTO, CollectionRequestDTO>("GetCollection",collectionRequest);

        }

        public void LoadNewCollection(CollectionDTO Collection)
        {
            //TODO POST NEW COLLECTION
            Collection.PictureList = new List<PictureInfoDTO>();

            api.PostObject<CollectionDTO>("NewCollection", Collection);

            CollectionOnPage = Collection;
        }

    }

}
