using DataClasses.DTO;

namespace MoleWatchApp.Interfaces
{
    public interface ICollectionModel
    {
        CollectionDTO CollectionOnPage { get; }

        void UploadPictureToDatabase(byte[] newDataBytes, PictureInfoDTO pictureInfo);

        void ChangeMarkingStatus();

        void ChangeNotificationStatus(CollectionDTO CollectionToChange);

        void ChangeCollectionName(CollectionDTO CollectionToChange, string NewName);

        void DeleteCollection(CollectionDTO CollectionToChange);

        byte[] LoadLastPicutreFromApi(int PictureID);
    }
}