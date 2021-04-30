using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class CreateCollectionViewModel : BaseViewModel
    {
        private IPatientModel patientModelRef;

        private string dateText;
        private string markCollectionImage;
        private string collectionTitle;
        private bool noImagesInCollection = true;
        private ImageSource lastCollectionPhoto;

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

        public Command ShowPictureCollectionCommand { get; }



        public CreateCollectionViewModel()
        {
            patientModelRef = PatientModelSingleton.GetPatientModel();

            UpdateCollectionPage();

            CameraButtonClicked = new Command(CameraButton_Clicked);
        }

        private void UpdateCollectionPage()
        {
           CollectionTitle = patientModelRef.CollectionOnPage.CollectionName;

           if (patientModelRef.CollectionOnPage.PictureList.Count != 0)
           {
               DateText = patientModelRef.CollectionOnPage.PictureList[patientModelRef.CollectionOnPage.PictureList.Count]
                   .DateOfUpload.ToShortDateString();
               NoImagesInCollection = false;
           }
           else
           {
               NoImagesInCollection = true;
               DateText = "Ingen billeder i samling";
           }


           if (!patientModelRef.CollectionOnPage.IsMarked)
           {
               MarkCollectionImage = "NotFlagged.png";
           }
           else
           {
               MarkCollectionImage = "FlaggedCollection.png";
           }

        }

        private async void CameraButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                LastCollectionPhoto = ImageSource.FromStream(() => { return photo.GetStream(); });
            }

        }
    }
}
