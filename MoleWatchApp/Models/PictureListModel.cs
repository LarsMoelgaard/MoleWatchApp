using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;

namespace MoleWatchApp.Models
{
    public class PictureListModel
    {
        private IAPIService api;

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

    }
}
