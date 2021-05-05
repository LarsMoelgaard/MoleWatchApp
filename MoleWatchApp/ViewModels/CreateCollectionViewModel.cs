﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class CreateCollectionViewModel : BaseViewModel
    {
        private IPatientModel patientModelRef;
        private CollectionModel collectionModel;
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



        public CreateCollectionViewModel()
        {
            patientModelRef = PatientModelSingleton.GetPatientModel();
            collectionModel = new CollectionModel();

            UpdateCollectionPage();

            CameraButtonClicked = new Command(CameraButton_Clicked);
            GalleryButtonClicked = new Command(GalleryButton_Clicked);
            MarkCommand = new Command(MarkCollection);
        }

        private void UpdateCollectionPage()
        {
           CollectionTitle = patientModelRef.CollectionOnPage.CollectionName;

           if (patientModelRef.CollectionOnPage.PictureList.Count != 0)
           {
               

               DateText = patientModelRef.CollectionOnPage
                   .PictureList[patientModelRef.CollectionOnPage.PictureList.Count - 1]
                   .DateOfUpload.ToString();


               LastPictureID = patientModelRef.CollectionOnPage
                   .PictureList[patientModelRef.CollectionOnPage.PictureList.Count - 1].PictureID;

                NoImagesInCollection = false;

               Thread LoadPictureThread = new Thread(LoadLastPicture);
                LoadPictureThread.Start();

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

        private void MarkCollection()
        {
            if (patientModelRef.CollectionOnPage.IsMarked)
            {
                patientModelRef.CollectionOnPage.IsMarked = false;
                //TODO Send update til API vedrørende markering af samling.
                MarkCollectionImage = "NotFlagged.png";
            }
            else
            {
                patientModelRef.CollectionOnPage.IsMarked = true;
                //TODO Send update til API vedrørende markering af samling.
                MarkCollectionImage = "FlaggedCollection.png";
            }

        }

        private async void CameraButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() {AllowCropping = true});

            if (photo != null)
            {

                LastCollectionPhoto = ImageSource.FromStream(() =>
                {
                    Stream NewPhotoStream = photo.GetStream();

                    MemoryStream ms = new MemoryStream();

                    NewPhotoStream.CopyTo(ms);

                    byte[] imgByteArray = ms.ToArray();

                    NewPhotoStream.Seek(0, SeekOrigin.Begin);
                    collectionModel.UploadPictureToDatabase(imgByteArray, patientModelRef.CollectionOnPage.CollectionID);

                    return NewPhotoStream;
                });

                NoImagesInCollection = false;
            }

        }

        private async void GalleryButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                new Plugin.Media.Abstractions.PickMediaOptions() { });

            if (photo != null)
            {

                LastCollectionPhoto = ImageSource.FromStream(() =>
                {
                    Stream NewPhotoStream = photo.GetStream();

                    MemoryStream ms = new MemoryStream();

                    NewPhotoStream.CopyTo(ms);

                    byte[] imgByteArray = ms.ToArray();

                    NewPhotoStream.Seek(0, SeekOrigin.Begin);
                    collectionModel.UploadPictureToDatabase(imgByteArray, patientModelRef.CollectionOnPage.CollectionID);

                    return NewPhotoStream;
                });

                NoImagesInCollection = false;
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

        }
    }

