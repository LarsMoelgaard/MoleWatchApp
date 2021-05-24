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
        private CollectionListModel collectionListModel;
        private IPatientModel patientModelRef;
        private ObservableCollection<CollectionWithThumbnail> collectionWithPicture;


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

        public ICommand ExistingCollectionListClicked
        {
            get
            {
                return new Command<CollectionDTO>((x) => GoToExistingCollection(x));
            }
        }

        public Command UpdateCollections { get; }

        #endregion

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
