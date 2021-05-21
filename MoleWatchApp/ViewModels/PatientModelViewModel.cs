﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using MoleWatchApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class PatientModelViewModel : BaseViewModel, IPatientViewModel
    {
        private ILogin loginModel;
        private IPatientModel patientModelClass;


        #region Properties, commands mm
        private string patientPicture;
        
        private string rotatePlaceholder;
        private string newPinAdded;
        private string plusIcon;
        private string checkmark;
        private bool isAnimationPlaying;
        private bool IsPatientMale;
        private bool IsPatientFrontFacing;
        private bool CreateCollectionInProgress;
        private ObservableCollection<CollectionDTO> patientCollection;

        #region DatabindingProperties

        

        public ObservableCollection<CollectionDTO> PatientCollection
        {
            get
            {
                return patientCollection;
            }
            set
            {
                patientCollection = value;
                this.OnPropertyChanged();
            }
        }

        public string Checkmark
        {

            get
            {
                return checkmark;
            }
            set
            {
                checkmark = value;
                this.OnPropertyChanged();
            }
        }
        public string PlusIcon
        {

            get
            {
                return plusIcon;
            }
            set
            {
                plusIcon = value;
                this.OnPropertyChanged();
            }
        }
        public string NewPinAdded
        {

            get
            {
                return newPinAdded;
            }
            set
            {
                newPinAdded = value;
                this.OnPropertyChanged();
            }
        }
        public string PatientPicture
        {
        
            get
            {
                return patientPicture;
            }
            set
            {
                patientPicture = value;
                this.OnPropertyChanged();
            }
        }
        public string RotatePlaceholder
        {

            get
            {
                return rotatePlaceholder;
            }
            set
            {
                rotatePlaceholder = value;
                this.OnPropertyChanged();
            }
        }
        public bool IsAnimationPlaying
        {
            get
            {
                return isAnimationPlaying;
            }
            set
            {
                isAnimationPlaying = value;
                this.OnPropertyChanged();
            }
        }

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

        public Command RotateClicked { get; }
        public Command PlusClicked { get; }
        public Command BackCommand { get; }
        public Command OnPageAppearingCommand { get; }
        public ICommand CreateOkClicked
        {
            get
            {
                return new Command<CollectionDTO>((x)=> CreateCollection(x));
            }
        }

        public ICommand ExistingCollectionClicked
        {
            get
            {
                return new Command<CollectionDTO>((x) => GoToExistingCollection(x));
            }
        }

        #endregion
        #endregion

        public PatientModelViewModel()
        {
            loginModel = LoginSingleton.GetLoginModel();
            PatientCollection = new ObservableCollection<CollectionDTO>();

            patientModelClass = PatientModelSingleton.GetPatientModel();


            IsAnimationPlaying = false;
            Title = "Vælg Modermærke";
            RotatePlaceholder = "animated_rotate.gif";


            PatientPicture = "MaleFrontCrop.png";


            PlusIcon = "Plus_Icon.png";
            IsPatientFrontFacing = true;
            CreateCollectionInProgress = false;

            
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            RotateClicked = new Command(FlipPatient);
            PlusClicked = new Command(onPlusClicked);
            BackCommand = new Command(OnBackButtonClicked);
            OnPageAppearingCommand = new Command(LoadPatient);
        }

        private void LoadPatient()
        {
            if (!loginModel.IsPatientLoadedFromAPI)
            {
                patientModelClass.CurrentPatient = loginModel.PatientData.PatientInfo;
                patientModelClass.CurrentPatientData = loginModel.PatientData;
                loginModel.IsPatientLoadedFromAPI = true;
            }

            IsPatientFrontFacing = true;

            if (patientModelClass.CurrentPatient.Gender.ToLower() == "b")
            {
                IsPatientMale = true;
                PatientPicture = "MaleFrontCrop.png";
            }
            else if(patientModelClass.CurrentPatient.Gender.ToLower() == "g")
            {
                IsPatientMale = false;
                PatientPicture = "FemaleFrontCrop.png";
            }
            else
            {
                PatientPicture = "MaleFrontCrop.png";
                // Der bliver ikke taget højde for transkønnede i dette program
                //throw new NotImplementedException("Køn ukendt.");
            }


            if (PatientCollection.Count == 0)
            {
                //ObservableCollection<CollectionDTO> tempCollection =
                //    new ObservableCollection<CollectionDTO>(loginModel.PatientData.CollectionList);

                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    PatientCollection = tempCollection;
                //});

                foreach (CollectionDTO ExistingCollectionDTO in loginModel.PatientData.CollectionList)
                {
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                        PatientCollection.Add(ExistingCollectionDTO);
                    //});

                }
            }
            else
            {
                //Device.BeginInvokeOnMainThread(() =>
                //{
                    ObservableCollection<CollectionDTO> tempCollection = new ObservableCollection<CollectionDTO>(patientModelClass.CurrentPatientData.CollectionList);
                    PatientCollection = tempCollection;
                //});

            }


            //Thread UpdateCollectionsThread = new Thread(UpdateCollectionsWhenLoaded);
            //UpdateCollectionsThread.Start();

        }


        private void UpdateCollectionsWhenLoaded()
        {
            Thread.Sleep(5000);


        }




        private void FlipPatient(object obj)
        {

            Task AnimationTask = new Task(AnimateRotation);
            AnimationTask.Start();

            if (IsPatientMale)
            {
                if (IsPatientFrontFacing)
                {
                    PatientPicture = "MaleBackCrop.png";
                    
                    IsPatientFrontFacing = false;
                }
                else
                {
                    PatientPicture = "MaleFrontCrop.png";
                    
                    IsPatientFrontFacing = true;
                }
            }
            else
            {
                if (IsPatientFrontFacing)
                {
                    PatientPicture = "FemaleBackCrop.png";
                    
                    IsPatientFrontFacing = false;
                }
                else
                {
                    PatientPicture = "FemaleFrontCrop.png";
                   
                    IsPatientFrontFacing = true;
                }
            }
           

        }

        private void AnimateRotation()
        {
            RotatePlaceholder = null;
            IsAnimationPlaying = true;
            Thread.Sleep(1180);
            IsAnimationPlaying = false;
            RotatePlaceholder = "animated_rotate.gif";
        }

        private async void GoToExistingCollection(CollectionDTO Collection)
        {
            if (!BaseIsBusy)
            {
                BaseIsBusy = true;
                await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

                patientModelClass.LoadExistingCollection(Collection);

                await Shell.Current.GoToAsync($"{nameof(CreateCollectionView)}");

                BaseIsBusy = false;
            }

        }


        private void onPlusClicked()
        {
            if (CreateCollectionInProgress == false)
            {
                NewPinAdded = "map_pin.png";
                PlusIcon = "cancel.png";
                Checkmark = "checkmark.png";
                CreateCollectionInProgress = true;
            }
            else
            {
                NewPinAdded = null;
                Checkmark = null;
                PlusIcon = "Plus_icon.png";
                CreateCollectionInProgress = false;
            }
        }

        private async void CreateCollection(CollectionDTO Collection)
        {
            NewPinAdded = null;
            Checkmark = null;
            PlusIcon = "Plus_icon.png";
            CreateCollectionInProgress = false;
            string collectionName = Collection.CollectionName;
            
            int SameName = 0;

            foreach (CollectionDTO collection in patientModelClass.CurrentPatientData.CollectionList)
            {
                if (collection.CollectionName.Contains(collectionName))
                {
                    SameName++;
                }

            }

            if (SameName > 0)
            {

                 collectionName += Convert.ToString(" " + SameName);
            }

            Collection.CollectionName = collectionName;

            Collection.Location.IsFrontFacing = IsPatientFrontFacing;

            Collection.CollectionID = patientModelClass.LoadNewCollection(Collection);

            patientModelClass.CurrentPatientData.CollectionList.Add(Collection);


            ObservableCollection<CollectionDTO> TempCollection = PatientCollection;
            TempCollection.Add(Collection);
            PatientCollection = TempCollection;


            await Shell.Current.GoToAsync($"{nameof(CreateCollectionView)}");
        }

        private async void OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}