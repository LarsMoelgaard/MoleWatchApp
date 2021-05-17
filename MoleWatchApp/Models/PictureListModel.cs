using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class PictureListModel : IPictureListModel
    {
        private IAPIService api;

        public CompletePicture PictureOnPage { get; set; }

        public PictureListModel()
        {
            api = APISingleton.GetAPI();
        }

        public byte[] LoadSpecificPicture(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO(); 
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetPictureData", PictureRequest);

            return PictureFromApi.PictureData;
            
        }


        public string LoadSpecificComment(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;

            PictureCommentDTO PictureFromApi = api.GetObject<PictureCommentDTO, PictureRequestDTO>("GetPictureComment", PictureRequest);

            return PictureFromApi.Comment;
        }

        public void DeleteSpecificPicture()
        {
            if (PictureOnPage != null)
            {
                PictureRequestDTO PictureRequest = new PictureRequestDTO();
                PictureRequest.PictureID = PictureOnPage.PictureID;

                string Return = api.PostObject<PictureRequestDTO>("DeletePicture", PictureRequest);
            }


        }
    }
}
