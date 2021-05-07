using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using MoleWatchApp.Views;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class CreateCollectionViewModel : BaseViewModel
    {
        private IPatientModel patientModelRef;
        private ICollectionModel collectionModel;
        private string dateText;
        private string markCollectionImage;
        private string collectionTitle;
        private bool noImagesInCollection = true;
        private ImageSource lastCollectionPhoto;
        private int LastPictureID;

        #region DatabindedProperties
        public ImageSource LastCollectionPhoto
        {
            get
            {
                return lastCollectionPhoto;
            }
            set
            {
                lastCollectionPhoto = value;
                this.OnPropertyChanged();
            }
        }

        public string DateText
        {
            get
            {
                return dateText;
            }
            set
            {
                dateText = value;
                this.OnPropertyChanged();
            }
        }

        public bool NoImagesInCollection
        {
            get
            {
                return noImagesInCollection;
            }
            set
            {
                noImagesInCollection = value;
                this.OnPropertyChanged();
            }
        }


        public string MarkCollectionImage
        {
            get
            {
                return markCollectionImage;
            }
            set
            {
                markCollectionImage = value;
                this.OnPropertyChanged();
            }
        }

        public string CollectionTitle
        {
            get
            {
                return collectionTitle;
            }
            set
            {
                collectionTitle = value;
                this.OnPropertyChanged();
            }
        }



        #endregion

        public Command MarkCommand { get; }

        public Command CameraButtonClicked { get; }
        public Command GalleryButtonClicked { get; }

        public Command ShowPictureCollectionCommand { get; }

        public Command DeleteCollectionCommand { get; }

        public ICommand ChangeNameCommand
        {
            get
            {
                return new Command<string>((x) => ChangeNameOnCollection(x));
            }
        }

        public CreateCollectionViewModel()
        {
            patientModelRef = PatientModelSingleton.GetPatientModel();


            collectionModel = new CollectionModel(patientModelRef.CollectionOnPage);

            UpdateCollectionPage();
            ShowPictureCollectionCommand = new Command(ShowPictureCollection);
            CameraButtonClicked = new Command(CameraButton_Clicked);
            GalleryButtonClicked = new Command(GalleryButton_Clicked);
            MarkCommand = new Command(MarkCollection);
            DeleteCollectionCommand = new Command(DeleteCollection);
        }

        private async void ShowPictureCollection()
        {
            await Shell.Current.GoToAsync($"{nameof(PictureListView)}");
        }

        private void UpdateCollectionPage()
        {
           CollectionTitle = collectionModel.CollectionOnPage.CollectionName;

           if (collectionModel.CollectionOnPage.PictureList.Count != 0)
           {
               DateText = collectionModel.CollectionOnPage
                   .PictureList[collectionModel.CollectionOnPage.PictureList.Count - 1]
                   .DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                       CultureInfo.CreateSpecificCulture("da-DA"));


                LastPictureID = collectionModel.CollectionOnPage
                   .PictureList[collectionModel.CollectionOnPage.PictureList.Count - 1].PictureID;

                NoImagesInCollection = false;

               Thread LoadPictureThread = new Thread(LoadLastPicture);
                LoadPictureThread.Start();

           }
           else
           {
               NoImagesInCollection = true;
               DateText = "Ingen billeder i samling";
           }


           if (!collectionModel.CollectionOnPage.IsMarked)
           {
               MarkCollectionImage = "NotFlagged.png";
           }
           else
           {
               MarkCollectionImage = "FlaggedCollection.png";
           }

        }

        private void MarkCollection()
        {
            if (collectionModel.CollectionOnPage.IsMarked)
            {
                collectionModel.CollectionOnPage.IsMarked = false;
                
                collectionModel.ChangeMarkingStatus();
                
                MarkCollectionImage = "NotFlagged.png";
            }
            else
            {
                collectionModel.CollectionOnPage.IsMarked = true;

                collectionModel.ChangeMarkingStatus();
                
                MarkCollectionImage = "FlaggedCollection.png";
            }

            patientModelRef.UpdateCollection(collectionModel.CollectionOnPage);
        }

        private async void CameraButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() {AllowCropping = true});

            if (photo != null)
            {
                PictureInfoDTO picToUpload = new PictureInfoDTO();

                byte[] imgByteArray = new byte[0];

                Stream NewPhotoStream = photo.GetStream();

                MemoryStream ms = new MemoryStream();

                NewPhotoStream.CopyTo(ms);

                imgByteArray = ms.ToArray();

                NewPhotoStream.Seek(0, SeekOrigin.Begin);

                DateTime UploadTime = DateTime.Now;

                picToUpload.CollectionID = collectionModel.CollectionOnPage.CollectionID;
                picToUpload.DateOfUpload = UploadTime;

                DateText = UploadTime.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                    CultureInfo.CreateSpecificCulture("da-DA"));


                collectionModel.UploadPictureToDatabase(imgByteArray, picToUpload);

                NoImagesInCollection = false;

                LastCollectionPhoto = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(imgByteArray);
                });
            }

        }

        private async void GalleryButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                new Plugin.Media.Abstractions.PickMediaOptions() { });

            if (photo != null)
            {
                PictureInfoDTO picToUpload = new PictureInfoDTO();

                byte[] imgByteArray = new byte[0];

                Stream NewPhotoStream = photo.GetStream();

                MemoryStream ms = new MemoryStream();

                NewPhotoStream.CopyTo(ms);

                imgByteArray = ms.ToArray();

                NewPhotoStream.Seek(0, SeekOrigin.Begin);

                DateTime UploadTime = DateTime.Now;

                picToUpload.CollectionID = collectionModel.CollectionOnPage.CollectionID;
                picToUpload.DateOfUpload = UploadTime;

                DateText = UploadTime.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                    CultureInfo.CreateSpecificCulture("da-DA"));


                collectionModel.UploadPictureToDatabase(imgByteArray, picToUpload);

                NoImagesInCollection = false;

                LastCollectionPhoto = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(imgByteArray);
                });
            }
        }


        private void LoadLastPicture()
        {
            
            LastCollectionPhoto = ImageSource.FromStream(() =>
            {
                byte[] loadedBytes = collectionModel.LoadLastPicutreFromApi(LastPictureID);

                MemoryStream ms = new MemoryStream(loadedBytes);

                return ms;
            });
        }

        private void ChangeNameOnCollection(string name)
        {
            CollectionDTO tempCollection = collectionModel.CollectionOnPage;
            tempCollection.CollectionName = name;
            patientModelRef.UpdateCollection(tempCollection);
            CollectionTitle = name;
            collectionModel.ChangeCollectionName(name);
        }

        private async void DeleteCollection()
        {
            collectionModel.DeleteCollection(patientModelRef.CollectionOnPage, patientModelRef.CurrentPatient);
            patientModelRef.RemoveCollection();
            await Shell.Current.GoToAsync("..");
        }
       }
    }

