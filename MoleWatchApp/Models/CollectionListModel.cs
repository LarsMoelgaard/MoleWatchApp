using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DTO;
using SkiaSharp;

namespace MoleWatchApp.Models
{
    /// <summary>
    /// Model klasse for CollectionListViewModel 
    /// </summary>
    public class CollectionListModel
    {
        /// <summary>
        /// Reference til api-klassen fra Nuget-Pakken
        /// </summary>
        private IAPIService api;

        /// <summary>
        /// Default constructor til CollectionListModellen
        /// </summary>
        public CollectionListModel()
        {
            api = APISingleton.GetAPI();

        }

        /// <summary>
        /// Metoden henter en specifik samling med tilhørende thumbnail 
        /// </summary>
        /// <param name="PictureID"></param>
        /// <returns></returns>
        public byte[] LoadSpecificCollectionThumbnail(int PictureID)
        {
            PictureRequestDTO PictureRequest = new PictureRequestDTO();
            PictureRequest.PictureID = PictureID;
            PictureDataDTO PictureFromApi = api.GetObject<PictureDataDTO, PictureRequestDTO>("GetThumbnail", PictureRequest);

            return PictureFromApi.PictureData;
        }
    }
}
