using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DataClasses.DTO;
using FFImageLoading.Forms.Args;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using MoleWatchApp.Views;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{

    /// <summary>
    /// ViewModel for CreateCollectionView 
    /// </summary>
    public class CreateCollectionViewModel : BaseViewModel
    {
        #region Properties mm 
        /// <summary>
        /// Reference til PatientModellens model så alt kan opdateres og hentes korrekt.
        /// </summary>
        private IPatientModel patientModelRef;

        /// <summary>
        /// Reference til Viewmodellen's model
        /// </summary>
        private ICollectionModel collectionModel;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string dateText;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string markCollectionImage;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string collectionTitle;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private bool noImagesInCollection = true;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private ImageSource lastCollectionPhoto;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private int LastPictureID;


        /// <summary>
        /// Property som sætter billedets source på siden
        /// </summary>
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


        /// <summary>
        /// Databinded property som styrer hvorvidt activity-indicatoren kører.
        /// </summary>
        public bool BaseIsBusy
        {
            get
            {
                return base.IsBusy;
            }
            set
            {
                base.IsBusy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property som styrer teksten som er under billedet, som indeholder datoen for det sidste billede som blev uploadet
        /// </summary>
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

        /// <summary>
        /// Bool der fortæller om der ikke er nogen billeder i samlingen
        /// </summary>
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


        /// <summary>
        /// Databinded property som styrer, ikonet der bliver vist sammen med "Markér samling"-knappen 
        /// </summary>
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

        /// <summary>
        /// Property der styrer titlen på CreateCollection-viewet.
        /// </summary>
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


        /// <summary>
        /// Kommando der eksekveres når "markér samling"-knappen bliver trykket på
        /// </summary>
        public Command MarkCommand { get; }


        /// <summary>
        /// Kommando der eksekveres når kamera-knappen trykkes på
        /// </summary>
        public Command CameraButtonClicked { get; }

        /// <summary>
        /// Kommando der eksekveres når galleri-knappen trykkes på
        /// </summary>
        public Command GalleryButtonClicked { get; }


        /// <summary>
        /// Kommando der eksekveres når "Vis bileder for samling"-knappen trykkes på
        /// </summary>
        public Command ShowPictureCollectionCommand { get; }


        /// <summary>
        /// Kommando der eksekveres når "SletCollection"-knappen i pop-up-menuen bliver trykket på
        /// </summary>
        public Command DeleteCollectionCommand { get; }



        /// <summary>
        /// Kommando der eksekveres når "ændre navn"-knappen i pop-up-menuen bliver trykket på
        /// </summary>
        public ICommand ChangeNameCommand
        {
            get
            {
                return new Command<string>((x) => ChangeNameOnCollection(x));
            }
        }

        #endregion
        
        /// <summary>
        /// Default constructor til viewmodellen
        /// </summary>
        public CreateCollectionViewModel()
        {
            BaseIsBusy = false;
            patientModelRef = PatientModelSingleton.GetPatientModel();


            collectionModel = new CollectionModel(patientModelRef.CollectionOnPage);

            UpdateCollectionPage();
            ShowPictureCollectionCommand = new Command(ShowPictureCollection);
            CameraButtonClicked = new Command(CameraButton_Clicked);
            GalleryButtonClicked = new Command(GalleryButton_Clicked);
            MarkCommand = new Command(MarkCollection);
            DeleteCollectionCommand = new Command(DeleteCollection);
        }


        /// <summary>
        /// Metoden åbner PictureListView for at vise en liste med alle billeder af det valgte modermærke 
        /// </summary>
        private async void ShowPictureCollection()
        {
            if (!NoImagesInCollection)
            {
                await Shell.Current.GoToAsync($"{nameof(PictureListView)}");
            }
            
        }


        /// <summary>
        /// Metoden opdatere CreateCollectionView
        /// </summary>
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
               BaseIsBusy = false;
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
        /// <summary>
        /// Metoden markere det valgt modermærke og indikere for brugeren at særlig opmærksomhed er påkrævet 
        /// </summary>
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


        /// <summary>
        /// Metoden åbner for kameraet på telefonen og lader brugeren tage et billede til samlingen 
        /// </summary>
        private async void CameraButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() {AllowCropping = true});

            if (photo != null)
            {
                BaseIsBusy = true;
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
                BaseIsBusy = false;
            }

        }
        /// <summary>
        /// Metoden åbner for brugerens galleri og lader brugeren vælge et billede at tilføje til samlingen 
        /// </summary>
        private async void GalleryButton_Clicked()
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                new Plugin.Media.Abstractions.PickMediaOptions() { });

            if (photo != null)
            {
                BaseIsBusy = true;
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
                BaseIsBusy = false;
            }
        }

        /// <summary>
        ///Metoden hentes det sidste billede som er tilføjet til samlingen og sætter dette som det billede der vises på siden 
        /// </summary>
        private async void LoadLastPicture()
        {
            BaseIsBusy = true;
            await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

            LastCollectionPhoto = ImageSource.FromStream(() =>
            {
                byte[] loadedBytes = collectionModel.LoadLastPicutreFromApi(LastPictureID);

                MemoryStream ms = new MemoryStream(loadedBytes);

                return ms;
            });

            BaseIsBusy = false;
        }


        /// <summary>
        /// Metoden lader brugeren ændre navn på samlingen 
        /// </summary>
        /// <param name="name"></param>
        private void ChangeNameOnCollection(string name)
        {
            CollectionDTO tempCollection = collectionModel.CollectionOnPage;
            tempCollection.CollectionName = name;
            patientModelRef.UpdateCollection(tempCollection);
            CollectionTitle = name;
            collectionModel.ChangeCollectionName(name);
        }


        /// <summary>
        /// Metoden lader brugeren slette den valgte samling af modermærker 
        /// </summary>
        private async void DeleteCollection()
        {
            collectionModel.DeleteCollection(patientModelRef.CollectionOnPage, patientModelRef.CurrentPatient);
            patientModelRef.RemoveCollection();
            await Shell.Current.GoToAsync("..");
        }
    }
}

