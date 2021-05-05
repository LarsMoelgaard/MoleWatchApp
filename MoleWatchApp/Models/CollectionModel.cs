using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using Xamarin.Forms;

namespace MoleWatchApp.Models
{
    public class CollectionModel
    {
        private IAPIService _api;

        public CollectionModel()
        {
            _api = APISingleton.GetAPI();
        }


        public void UploadPictureToDatabase(byte[] newDataBytes, int CollectionID)
        {


            PostPictureDTO NewPostPicture = new PostPictureDTO();
            
            NewPostPicture.Comment = new PictureCommentDTO();
            NewPostPicture.Info = new PictureInfoDTO();
            NewPostPicture.Data = new PictureDataDTO();

            NewPostPicture.Data.PictureData = newDataBytes;

            NewPostPicture.Comment.Comment = "";

            NewPostPicture.Info.DateOfUpload = DateTime.Now;
            NewPostPicture.Info.CollectionID = CollectionID;


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

        public void ChangeMarkingStatus(CollectionDTO CollectionToChange)
        {
            throw new NotImplementedException("Få det lavet!");
        }

        
        public void ChangeNotificationStatus(CollectionDTO CollectionToChange)
        {
            throw new NotImplementedException("Få det lavet!");
        }

        public void ChangeCollectionName(CollectionDTO CollectionToChange, string NewName)
        {
            throw new NotImplementedException("Få det lavet!");
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

