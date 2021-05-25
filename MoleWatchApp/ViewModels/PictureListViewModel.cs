using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Viewmodel for PictureListView 
    /// </summary>
    public class PictureListViewModel : BaseViewModel
    {
        #region Properties mm 
        /// <summary>
        /// Reference til patientModellen for at kunne hente/opdatere den relevante information.
        /// </summary>
        private IPatientModel patientModelRef;

        /// <summary>
        /// Reference til viewmodellens Model / Datalag
        /// </summary>
        private IPictureListModel PictureListModel;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private bool isPicturesFullyLoaded;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string pageTitle;

        /// <summary>
        /// Databinded property der fortæller om viewmodellen er i gang med at eksekvere en trådspecifik handling.
        /// </summary>
        public bool BaseIsBusy
        {
            get
            {
                return this.IsBusy;
            }
            set
            {
                this.IsBusy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private ObservableCollection<CompletePicture> completePictureList;


        /// <summary>
        /// Databinded property, som opdaterer listen af billederne på skærmen
        /// </summary>
        public ObservableCollection<CompletePicture> CompletePictureList
        {
            get
            {
                return completePictureList;
            }
            set
            {
                completePictureList = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Databinded property til titlen på viewet.
        /// </summary>
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                pageTitle = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Bool der siger noget om hvorledes alle billederne er blevet loaded ind på siden.
        /// </summary>
        public bool IsPicturesFullyLoaded
        {
            get
            {
                return isPicturesFullyLoaded;
            }
            set
            {
                isPicturesFullyLoaded = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Kommando som eksekveres når siden bliver vist.
        /// </summary>
        public Command UpdateTableOnAppearingCommand { get; }

        /// <summary>
        /// Kommando der eksekveres når der trykkes på et specifikt billede i en samling.
        /// </summary>
        public ICommand OpenFullPictureView
        {
            get
            {
                return new Command<int>((x) => OpenPictureView(x));
            }
        }

        #endregion
        /// <summary>
        /// Default constructor til viewmodellen
        /// </summary>
        public PictureListViewModel()
        {
            PictureListModel = PictureListModelSingleton.GetPictureListModel();
            patientModelRef = PatientModelSingleton.GetPatientModel();
            IsPicturesFullyLoaded = false;

            UpdateTableOnAppearingCommand = new Command(PageAppearing);
            CompletePictureList = new ObservableCollection<CompletePicture>();

            

            ObservableCollection<CompletePicture> TempPictureList = new ObservableCollection<CompletePicture>();
           
            patientModelRef.LoadExistingCollection(patientModelRef.CollectionOnPage); //Gør således at de sidste billeder også bliver loaded

            
            foreach (PictureInfoDTO PictureToAdd in patientModelRef.CollectionOnPage.PictureList)
            {
                TempPictureList.Add(new CompletePicture(PictureToAdd));
            }

            CompletePictureList = TempPictureList;
            PictureListModel.CompletePictureModelList = CompletePictureList;

            PageTitle = patientModelRef.CollectionOnPage.CollectionName;

            Thread t1 = new Thread(LoadPictureData);
            t1.Start();
        }

        /// <summary>
        /// Metoden henter alle billeder for et modermærke 
        /// </summary>
        public async void LoadPictureData()
        {
            BaseIsBusy = true;
            await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

            ObservableCollection<CompletePicture> tempCollection = new ObservableCollection<CompletePicture>();

            foreach (var ExistingPicture in CompletePictureList)
            {
                tempCollection.Add(ExistingPicture);
            }

            foreach (CompletePicture PictureInCollection in tempCollection)
            {
               byte[] PictureData = PictureListModel.LoadSpecificThumbnail(PictureInCollection.PictureID);
               string PictureComment = PictureListModel.LoadSpecificComment(PictureInCollection.PictureID);
               PictureInCollection.PictureData = PictureData;
               PictureInCollection.PictureComment = PictureComment;
            }

            CompletePictureList = tempCollection;
            BaseIsBusy = false;
        }


        /// <summary>
        /// Metoden åbner det valgte billede i sit eget view (Full picture view) 
        /// </summary>
        /// <param name="PictureId"></param>
        private void OpenPictureView(int PictureId)
        {
            CompletePicture CompletePictureOnFullPage = CompletePictureList.
                Where(i => i.PictureID == PictureId).First();
            

            PictureListModel.PictureOnPage = CompletePictureOnFullPage;


            Shell.Current.GoToAsync($"{nameof(FullPictureView)}");
        }


        /// <summary>
        /// Metoden kaldes når siden vises og opdatere listen med billeder 
        /// </summary>
        private void PageAppearing()
        {
            if (CompletePictureList != null)
            {
                CompletePictureList = PictureListModel.CompletePictureModelList;
            }
        }
    }
}
