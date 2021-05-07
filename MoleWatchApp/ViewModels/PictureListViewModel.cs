using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
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




        public PictureListViewModel() 
        {
            PictureListModel = new PictureListModel();
            patientModelRef = PatientModelSingleton.GetPatientModel();

            CompletePictureList = new ObservableCollection<CompletePicture>();

            ObservableCollection<CompletePicture> TempPictureList = new ObservableCollection<CompletePicture>();
            foreach (PictureInfoDTO PictureToAdd in patientModelRef.CollectionOnPage.PictureList)
            {
                TempPictureList.Add(new CompletePicture(PictureToAdd));
            }

            CompletePictureList = TempPictureList;

            Thread CollectPictureDataThread = new Thread(LoadPictureData);
            CollectPictureDataThread.Start();
        }

        public void LoadPictureData()
        {
            foreach (CompletePicture PictureInCollection in CompletePictureList)
            {
               byte[] PictureData = PictureListModel.LoadSpecificPicture(PictureInCollection.PictureID);

               PictureInCollection.PictureData = PictureData;
            }
        }
    }
}
