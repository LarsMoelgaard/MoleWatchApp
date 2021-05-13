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
    public class PictureListViewModel : BaseViewModel
    {
        private IPatientModel patientModelRef;
        private IPictureListModel PictureListModel;
        private bool isPicturesFullyLoaded;
        private string pageTitle;



        

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

        private ObservableCollection<CompletePicture> completePictureList;
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

        public ICommand OpenFullPictureView
        {
            get
            {
                return new Command<int>((x) => OpenPictureView(x));
            }
        }

        public PictureListViewModel()
        {
            PictureListModel = PictureListModelSingleton.GetPictureListModel();
            patientModelRef = PatientModelSingleton.GetPatientModel();
            IsPicturesFullyLoaded = false;

            CompletePictureList = new ObservableCollection<CompletePicture>();

            ObservableCollection<CompletePicture> TempPictureList = new ObservableCollection<CompletePicture>();
            foreach (PictureInfoDTO PictureToAdd in patientModelRef.CollectionOnPage.PictureList)
            {
                TempPictureList.Add(new CompletePicture(PictureToAdd));
            }

            CompletePictureList = TempPictureList;
            PageTitle = patientModelRef.CollectionOnPage.CollectionName;

            Thread t1 = new Thread(LoadPictureData);
            t1.Start();
        }

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
               byte[] PictureData = PictureListModel.LoadSpecificPicture(PictureInCollection.PictureID);
               string PictureComment = PictureListModel.LoadSpecificComment(PictureInCollection.PictureID);
               PictureInCollection.PictureData = PictureData;
               PictureInCollection.PictureComment = PictureComment;
            }

            CompletePictureList = tempCollection;
            //BaseIsBusy = false;
        }

        private void OpenPictureView(int PictureId)
        {
            CompletePicture CompletePictureOnFullPage = CompletePictureList.
                Where(i => i.PictureID == PictureId).First();
            

            PictureListModel.PictureOnPage = CompletePictureOnFullPage;


            Shell.Current.GoToAsync($"{nameof(FullPictureView)}");
        }

    }
}
