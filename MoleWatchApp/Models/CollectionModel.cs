using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;

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
            NewPostPicture.Info.DateOfUpload = DateTime.Now;
            
            NewPostPicture.Info.CollectionID = CollectionID;
            

            //TODO Upload NewPostPicture to API
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


    }
}

