using System;
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
    /// <summary>
    /// Model klasse for PictureList viewmodel 
    /// </summary>
    public class PictureListModel : IPictureListModel
    {
        /// <summary>
        /// Reference til api-klassen fra Nuget-Pakken
        /// </summary>
        private IAPIService api;

        /// <summary>
        /// Billedet der er valgt til at blive vist på siden
        /// </summary>
        public CompletePicture PictureOnPage { get; set; }

        /// <summary>
        /// Listen af alle billederne der er hentet ned fra API'en
        /// </summary>
        public ObservableCollection<CompletePicture> CompletePictureModelList { get; set; }

        /// <summary>
        /// Default constructor til modellen
        /// </summary>
        public PictureListModel()
        {
            api = APISingleton.GetAPI();
        }
        

        /// <summary>
        /// Metoden henter et specifikt billede via API'en 
        /// </summary>
        /// <param name="PictureID"></param>
        /// <returns></returns>
        public byte[] LoadSpecificPicture(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO(); 
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetPictureData", PictureRequest);

            return PictureFromApi.PictureData;
            
        }

        /// <summary>
        /// Metoden henter en kommentar til et specifikt billede via API'en 
        /// </summary>
        /// <param name="PictureID"></param>
        /// <returns></returns>
        public string LoadSpecificComment(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;

            PictureCommentDTO PictureFromApi = api.GetObject<PictureCommentDTO, PictureRequestDTO>("GetPictureComment", PictureRequest);

            return PictureFromApi.Comment;
        }


        /// <summary>
        /// Metoden sletter et specifikt billede fra samlingen 
        /// </summary>
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

        /// <summary>
        /// Metoden opdatere en kommentar til et specifikt billede 
        /// </summary>
        /// <param name="Comment"></param>
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

        /// <summary>
        /// Metoden henter et thumbnail billede for en specifik samling 
        /// </summary>
        /// <param name="PictureID"></param>
        /// <returns></returns>
        public byte[] LoadSpecificThumbnail(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetThumbnail", PictureRequest);

            return PictureFromApi.PictureData;

        }
    }
}
