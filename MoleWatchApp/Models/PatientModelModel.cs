using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    /// <summary>
    /// Model klasse til PatientModelViewModel 
    /// </summary>
    public class PatientModelModel : IPatientModel
    {
        public PatientInfoDTO CurrentPatient { get; set; }
        public PatientDataDTO CurrentPatientData { get; set; }
        public CollectionDTO CollectionOnPage { get; private set; }
        public IAPIService api { get; }



        public PatientModelModel()
        {
            api = APISingleton.GetAPI();
        }

        /// <summary>
        /// Metoden henter en eksisterende collection via API'en 
        /// </summary>
        /// <param name="Collection"></param>
        public void LoadExistingCollection(CollectionDTO Collection)
        {
            CollectionRequestDTO collectionRequest = new CollectionRequestDTO();

            collectionRequest.CollectionID = Collection.CollectionID;

            collectionRequest.PatientID = CurrentPatient.PatientID;

            CollectionOnPage = api.GetObject<CollectionDTO, CollectionRequestDTO>("GetCollection",collectionRequest);

        }

        /// <summary>
        /// Metoden opretter en ny collection og oploader den til databasen via API'en 
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public int LoadNewCollection(CollectionDTO Collection)
        {
            
            Collection.PictureList = new List<PictureInfoDTO>();

            string ID = api.PostObject<CollectionDTO>("NewCollection", Collection);

            
            Collection.CollectionID = Convert.ToInt32(ID);

            CollectionOnPage = Collection;

            return Collection.CollectionID;
        }


        /// <summary>
        /// Metoden opdatere en eksisterende collection 
        /// </summary>
        /// <param name="UpdatedCollection"></param>
        public void UpdateCollection(CollectionDTO UpdatedCollection)
        {
            CollectionDTO OldCollectionDTO = CurrentPatientData.CollectionList.
                Where(i => i.CollectionID == UpdatedCollection.CollectionID).First();

            int indexPosition = CurrentPatientData.CollectionList.IndexOf(OldCollectionDTO);

            CurrentPatientData.CollectionList[indexPosition] = UpdatedCollection;
        }

        /// <summary>
        /// Metoden sletter en collection i databasen via API'en 
        /// </summary>
        public void RemoveCollection()
        {
            CollectionDTO OldCollectionDTO = CurrentPatientData.CollectionList.
                Where(i => i.CollectionID == CollectionOnPage.CollectionID).First();

            int indexPosition = CurrentPatientData.CollectionList.IndexOf(OldCollectionDTO);
            CurrentPatientData.CollectionList.RemoveAt(indexPosition);
        }
    }

}
