using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using MoleWatchApp.Views;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    /// <summary>
    /// ViewModel for collectionList view 
    /// </summary>
    public class CollectionListViewModel : BaseViewModel
    {
        #region Properties mm
        /// <summary>
        /// Reference til viewmodellen's Model
        /// </summary>
        private CollectionListModel collectionListModel;

        /// <summary>
        /// Reference til Patientmodellen så data kan hentes/opdateres korrekt
        /// </summary>
        private IPatientModel patientModelRef;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private ObservableCollection<CollectionWithThumbnail> collectionWithPicture;

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
        /// Observable collection af CollectionWithThumbnail, der kan fungere som links til de forskellige collection med et billede i småt format til identifikation
        /// </summary>
        public ObservableCollection<CollectionWithThumbnail> CollectionWithPicture
        {
            get
            {
                return collectionWithPicture;
            }
            set
            {
                collectionWithPicture = value;
                this.OnPropertyChanged();
            }
        }



        /// <summary>
        /// Kommando der eksekveres når en specifik collection bliver trykket på.
        /// </summary>
        public ICommand ExistingCollectionListClicked
        {
            get
            {
                return new Command<CollectionDTO>((x) => GoToExistingCollection(x));
            }
        }



        /// <summary>
        /// Kommando der eksekveres når siden indlæses
        /// </summary>
        public Command UpdateCollections { get; }

        #endregion

        /// <summary>
        /// Default constructor til viewmodellen
        /// </summary>
        public CollectionListViewModel()
        {
            patientModelRef = PatientModelSingleton.GetPatientModel();
            collectionListModel = new CollectionListModel();
            UpdateCollections = new Command(LoadCollections);
        }


        /// <summary>
        /// Metoden henter alle collectioner som tilhører brugeren 
        /// </summary>
        private void LoadCollections()
        {
            ObservableCollection<CollectionWithThumbnail> TempCollections = new ObservableCollection<CollectionWithThumbnail>();

            foreach (CollectionDTO Collection in patientModelRef.CurrentPatientData.CollectionList)
            {
                TempCollections.Add(new CollectionWithThumbnail(Collection));
            }

            CollectionWithPicture = TempCollections;

            Thread LoadPicturesThread = new Thread(LoadThumbnails);
            LoadPicturesThread.Start();
        }


        /// <summary>
        /// Metoden henter alle collectionerne med alle de nyeste billeder til hvert modermærke
        /// </summary>
        private async void LoadThumbnails()
        {
            BaseIsBusy = true;
            await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

            ObservableCollection<CollectionWithThumbnail> tempCollection = new ObservableCollection<CollectionWithThumbnail>();

            foreach (CollectionWithThumbnail ExistingCollection in CollectionWithPicture)
            {
                tempCollection.Add(ExistingCollection);
            }

            foreach (CollectionWithThumbnail Collection in tempCollection)
            {
                if (Collection.Collection.PictureList.Count!= 0)
                {
                    byte[] PictureData = collectionListModel.LoadSpecificCollectionThumbnail(Collection.Collection.PictureList[Collection.Collection.PictureList.Count-1].PictureID);


                    Collection.CollectionPictureData = ConvertByteArrayToImageSource(PictureData);
                }
                else
                {
                    Collection.CollectionPictureData = "no_picture_icon.png";
                }


            }

            CollectionWithPicture = tempCollection;
            BaseIsBusy = false;


        }

        /// <summary>
        /// Metoden åbner en valgt collection og viser denne i CreateCikkectionView'et 
        /// </summary>
        /// <param name="existingCollection"></param>
        private async void GoToExistingCollection(CollectionDTO existingCollection)
        {
            if (!BaseIsBusy)
            {
                BaseIsBusy = true;
                await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

                patientModelRef.LoadExistingCollection(existingCollection);

                await Shell.Current.GoToAsync($"{nameof(CreateCollectionView)}");

                BaseIsBusy = false;
            }

        }


        /// <summary>
        /// Metoden laver et byte array om til et imagesource 
        /// </summary>
        /// <param name="PictureData"></param>
        /// <returns></returns>
        private ImageSource ConvertByteArrayToImageSource(byte[] PictureData)
        {

            ImageSource NewPhoto = ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream(PictureData);

                return ms;
            });
            return NewPhoto;
        }

    }
}
