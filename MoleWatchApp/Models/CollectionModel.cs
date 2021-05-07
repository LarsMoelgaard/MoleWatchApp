using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using Xamarin.Forms;

namespace MoleWatchApp.Models
{
    public class CollectionModel : ICollectionModel
    {
        private IAPIService _api;
        public CollectionDTO CollectionOnPage { get; private set; }

        public CollectionModel(CollectionDTO CollectionOnPage) 
        {
            _api = APISingleton.GetAPI();
            this.CollectionOnPage = CollectionOnPage;
            
        }


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

        public void ChangeMarkingStatus()
        {
            ChangeCollectionMarkingDTO ChangeMarkingDTO = new ChangeCollectionMarkingDTO();
            ChangeMarkingDTO.CollectionID = CollectionOnPage.CollectionID;
            ChangeMarkingDTO.IsMarked = CollectionOnPage.IsMarked;

            
            string outputFromAPI =_api.PostObject<ChangeCollectionMarkingDTO>
                ("ChangeCollectionMarking", ChangeMarkingDTO);

        }

        
        public void ChangeNotificationStatus(CollectionDTO CollectionToChange)
        {
            throw new NotImplementedException("Få det lavet!");
        }

        public void ChangeCollectionName(string NewName)
        {
            ChangeCollectionNameDTO NameChangeDTO = new ChangeCollectionNameDTO();
            NameChangeDTO.CollectionName = NewName;
            NameChangeDTO.CollectionID = CollectionOnPage.CollectionID;
            _api.PostObject<ChangeCollectionNameDTO>
                ("ChangeCollectionName", NameChangeDTO);
        }

        public void DeleteCollection(CollectionDTO CollectionToChange, PatientInfoDTO patient)
        {
            CollectionRequestDTO DeleteCollectionRequest = new CollectionRequestDTO();
            DeleteCollectionRequest.CollectionID = CollectionToChange.CollectionID;
            DeleteCollectionRequest.PatientID = patient.PatientID;

           string result = _api.PostObject<CollectionRequestDTO>("DeleteCollection", DeleteCollectionRequest);
        }

        public byte[] LoadLastPicutreFromApi(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;

            PictureDataDTO  PictureFromApi = _api.GetObject<PictureDataDTO, PictureRequestDTO>("GetPictureData", PictureRequest);

            return PictureFromApi.PictureData;
        }


    }
}

