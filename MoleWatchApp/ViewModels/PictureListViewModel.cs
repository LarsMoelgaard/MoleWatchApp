using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class PictureListViewModel : BaseViewModel
    {
        private IPatientModel patientModelRef;
        private PictureListModel PictureListModel;
        private bool isPicturesFullyLoaded;



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



        public PictureListViewModel() 
        {
            PictureListModel = new PictureListModel();
            patientModelRef = PatientModelSingleton.GetPatientModel();
            IsPicturesFullyLoaded = false;

            CompletePictureList = new ObservableCollection<CompletePicture>();

            ObservableCollection<CompletePicture> TempPictureList = new ObservableCollection<CompletePicture>();
            foreach (PictureInfoDTO PictureToAdd in patientModelRef.CollectionOnPage.PictureList)
            {
                TempPictureList.Add(new CompletePicture(PictureToAdd));
            }

            CompletePictureList = TempPictureList;


            Thread t1 = new Thread(LoadPictureData);
            t1.Start();
        }

        public void LoadPictureData()
        {

            ObservableCollection<CompletePicture> tempCollection = new ObservableCollection<CompletePicture>();

            foreach (var VARIABLE in CompletePictureList)
            {
                tempCollection.Add(VARIABLE);
            }

            foreach (CompletePicture PictureInCollection in tempCollection)
            {
               byte[] PictureData = PictureListModel.LoadSpecificPicture(PictureInCollection.PictureID);

               PictureInCollection.PictureData = PictureData;
            }

            
            CompletePictureList = tempCollection;
        }

    }
}
