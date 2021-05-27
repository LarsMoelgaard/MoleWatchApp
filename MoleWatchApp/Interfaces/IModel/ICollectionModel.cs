using DataClasses.DTO;

namespace MoleWatchApp.Interfaces.IModel
{
    public interface ICollectionModel
    {
        CollectionDTO CollectionOnPage { get; }

        void UploadPictureToDatabase(byte[] newDataBytes, PictureInfoDTO pictureInfo);

        void ChangeMarkingStatus();

        void ChangeNotificationStatus(CollectionDTO CollectionToChange);

        void ChangeCollectionName(string NewName);

        void DeleteCollection(CollectionDTO CollectionToChange, PatientInfoDTO patient);

        byte[] LoadLastPicutreFromApi(int PictureID);

        byte[] LoadLastPictureThumbnail(int PictureID);
    }
}