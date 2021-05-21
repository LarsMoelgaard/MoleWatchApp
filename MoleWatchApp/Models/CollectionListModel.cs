using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using SkiaSharp;

namespace MoleWatchApp.Models
{
    public class CollectionListModel
    {
        private IAPIService api;

        public CollectionListModel()
        {
            api = APISingleton.GetAPI();

        }

        public byte[] LoadSpecificCollectionThumbnail(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetThumbnail", PictureRequest);

            return PictureFromApi.PictureData;
        }


    }
}
