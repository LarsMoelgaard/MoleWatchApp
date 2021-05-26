using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;
using Xamarin.Forms;


namespace MoleWatchApp.Models
{
    /// <summary>
    /// Model klasse til CollectionViewModel 
    /// </summary>
    public class CollectionModel : ICollectionModel
    {
        /// <summary>
        /// Reference til api-klassen fra Nuget-Pakken
        /// </summary>
        private IAPIService _api;

        /// <summary>
        /// Property til at kunne hente og opdatere den rigtige samling på siden.
        /// </summary>
        public CollectionDTO CollectionOnPage { get; private set; }
        
        /// <summary>
        /// Constructor til collectionModel
        /// </summary>
        /// <param name="CollectionOnPage">Den collection som siden skal vise</param>
        public CollectionModel(CollectionDTO CollectionOnPage) 
        {
            _api = APISingleton.GetAPI();
            this.CollectionOnPage = CollectionOnPage;
        }

        /// <summary>
        /// Metoden oploader et nyt billede til databasen via API'en 
        /// </summary>
        /// <param name="newDataBytes"></param>
        /// <param name="pictureInfo"></param>
        public void UploadPictureToDatabase(byte[] newDataBytes, PictureInfoDTO pictureInfo)
        {
            PostPictureDTO NewPostPicture = new PostPictureDTO();
            
            NewPostPicture.Comment = new PictureCommentDTO();
            NewPostPicture.Info = pictureInfo;
            NewPostPicture.Data = new PictureDataDTO();

            NewPostPicture.Data.PictureData = newDataBytes;

            NewPostPicture.Comment.Comment = "";


            try
            {
                _api.PostObject<PostPictureDTO>("NewPicture", NewPostPicture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            //TODO indsæt try/catch
        }


        /// <summary>
        /// Metodne ændre et modermærkes status 
        /// </summary>
        public void ChangeMarkingStatus()
        {
            ChangeCollectionMarkingDTO ChangeMarkingDTO = new ChangeCollectionMarkingDTO();
            ChangeMarkingDTO.CollectionID = CollectionOnPage.CollectionID;
            ChangeMarkingDTO.IsMarked = CollectionOnPage.IsMarked;

            string outputFromAPI =_api.PostObject<ChangeCollectionMarkingDTO>
                ("ChangeCollectionMarking", ChangeMarkingDTO);

        }

        /// <summary>
        /// Metoden opdatere indstillingerne for hyppigheden af notifikationer for et modermærke
        /// </summary>
        /// <param name="CollectionToChange"></param>
        public void ChangeNotificationStatus(CollectionDTO CollectionToChange)
        {
            //TODO Implementer forbindelse til API - måske dette skal undlades? 
        }


        /// <summary>
        /// Metoden ændre navnet på et modermærke på databasen 
        /// </summary>
        /// <param name="NewName"></param>
        public void ChangeCollectionName(string NewName)
        {
            ChangeCollectionNameDTO NameChangeDTO = new ChangeCollectionNameDTO();
            NameChangeDTO.CollectionName = NewName;
            NameChangeDTO.CollectionID = CollectionOnPage.CollectionID;
            _api.PostObject<ChangeCollectionNameDTO>
                ("ChangeCollectionName", NameChangeDTO);
        }

        /// <summary>
        /// Metoden sletter et modermærke fra databasen 
        /// </summary>
        /// <param name="CollectionToChange"></param>
        /// <param name="patient"></param>
        public void DeleteCollection(CollectionDTO CollectionToChange, PatientInfoDTO patient)
        {
            CollectionRequestDTO DeleteCollectionRequest = new CollectionRequestDTO();
            DeleteCollectionRequest.CollectionID = CollectionToChange.CollectionID;
            DeleteCollectionRequest.PatientID = patient.PatientID;

           string result = _api.PostObject<CollectionRequestDTO>("DeleteCollection", DeleteCollectionRequest);
        }

        /// <summary>
        /// Metoden henter det sidst tilføjede billede for et modermærke 
        /// </summary>
        /// <param name="PictureID"></param>
        /// <returns></returns>
        public byte[] LoadLastPicutreFromApi(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;

            PictureDataDTO  PictureFromApi = _api.GetObject<PictureDataDTO, PictureRequestDTO>("GetPictureData", PictureRequest);

            return PictureFromApi.PictureData;
        }
    }
}

