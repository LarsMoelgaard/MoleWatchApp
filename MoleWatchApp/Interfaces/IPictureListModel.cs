using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Models;

namespace MoleWatchApp.Interfaces
{
    public interface IPictureListModel
    {
        CompletePicture PictureOnPage { get; set; }

        ObservableCollection<CompletePicture> CompletePictureModelList { get; set; }

        byte[] LoadSpecificPicture(int PictureID);


        string LoadSpecificComment(int PictureID);

        void DeleteSpecificPicture();

        void UpdatePictureComment(string Comment);


        byte[] LoadSpecificThumbnail(int PictureID);
    }
}
