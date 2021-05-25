using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;

namespace MoleWatchApp.Extensions.DTO
{
    /// <summary>
    /// Klasse der indeholder dataen som en PictureInfoDTO sammen med et byte array som indeholder billeddata
    /// </summary>
    public class CompletePicture
    {
        public int PictureID { get; set; }

        public int CollectionID { get; set; }

        public DateTime DateOfUpload { get; set; }

        public byte[] PictureData { get; set; }

        public string PictureComment { get; set; }

        public CompletePicture(int pictureId, int collectionId, DateTime dateOfUpload)
        {
            PictureID = pictureId;
            CollectionID = collectionId;
            DateOfUpload = dateOfUpload;
            PictureData = new byte[1];
            PictureComment = "";
        }

        public CompletePicture(PictureInfoDTO picInfo)
        {
            PictureID = picInfo.PictureID;
            CollectionID = picInfo.CollectionID;
            DateOfUpload = picInfo.DateOfUpload;
            PictureData = new byte[1];
            PictureComment = "";
        }
    }
}
