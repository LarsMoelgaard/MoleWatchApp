using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;

namespace MoleWatchApp.ViewModels
{
    public class CollectionListViewModel : BaseViewModel
    {
        


        
        private IPatientModel patientModelRef;
        
        
        private ObservableCollection<CollectionWithThumbnail> collectionWithPicture;


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

        public CollectionListViewModel()
        {
            patientModelRef = PatientModelSingleton.GetPatientModel();




            ObservableCollection<CollectionWithThumbnail> TempCollections = new ObservableCollection<CollectionWithThumbnail>();

            foreach (CollectionDTO Collection in patientModelRef.CurrentPatientData.CollectionList)
            {
                TempCollections.Add(new CollectionWithThumbnail(Collection));
            }

            CollectionWithPicture = TempCollections;

            Thread LoadPicturesThread = new Thread(LoadThumbnails);
            LoadPicturesThread.Start();
        }

        public void LoadThumbnails()
        {



        }
    }
}
