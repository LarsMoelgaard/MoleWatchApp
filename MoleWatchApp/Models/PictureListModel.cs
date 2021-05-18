﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<CompletePicture> CompletePictureModelList { get; set; }

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



                CompletePicture OldPicture = CompletePictureModelList.
                    Where(i => i.PictureID == PictureOnPage.PictureID).First();

                int indexPosition = CompletePictureModelList.IndexOf(OldPicture);
                CompletePictureModelList.RemoveAt(indexPosition);
            }


        }

        public void UpdatePictureComment(string Comment)
        {
            if (PictureOnPage != null)
            {
                PictureCommentDTO NewPictureComment = new PictureCommentDTO();
                NewPictureComment.Comment = Comment;
                NewPictureComment.PictureID = PictureOnPage.PictureID;

                string ReturnMessage = api.PostObject<PictureCommentDTO>("NewPictureComment", NewPictureComment);

                CompletePicture OldPicture = CompletePictureModelList.
                    Where(i => i.PictureID == PictureOnPage.PictureID).First();

                int indexPosition = CompletePictureModelList.IndexOf(OldPicture);
                CompletePictureModelList[indexPosition].PictureComment = Comment;
            }

        }


        public byte[] LoadSpecificThumbnail(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetThumbnail", PictureRequest);

            return PictureFromApi.PictureData;

        }
    }
}
