using System.Collections.ObjectModel;
using MoleWatchApp.Extensions.DTO;

namespace MoleWatchApp.Interfaces.IModel
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
