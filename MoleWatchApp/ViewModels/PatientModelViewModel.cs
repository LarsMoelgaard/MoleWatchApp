using System;
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
    /// <summary>
    /// View model for PatientModelView 
    /// </summary>
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

            //Indsæt defaultbilleder mm for view 
            IsAnimationPlaying = false;
            Title = "Vælg Modermærke";
            RotatePlaceholder = "animated_rotate.gif";
            PatientPicture = "MaleFrontCrop.png";
            PlusIcon = "Plus_Icon.png";
            IsPatientFrontFacing = true;
            CreateCollectionInProgress = false;

            RotateClicked = new Command(FlipPatient);
            PlusClicked = new Command(onPlusClicked);
            OnPageAppearingCommand = new Command(LoadPatient);
        }


        /// <summary>
        /// Metoden kaldes når View'et vises. Den henter patientdataen og får sat den korrekte patientmodel m. tilhørende modermærker 
        /// </summary>
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
                foreach (CollectionDTO ExistingCollectionDTO in loginModel.PatientData.CollectionList)
                {

                        PatientCollection.Add(ExistingCollectionDTO);
                }
            }
            else
            {
                ObservableCollection<CollectionDTO> tempCollection = new ObservableCollection<CollectionDTO>(patientModelClass.CurrentPatientData.CollectionList);
                PatientCollection = tempCollection;
                    
            }
        }


        /// <summary>
        /// Metoden kaldes når "Flip patient" knappen trykkes på view'et 
        /// </summary>
        /// <param name="obj"></param>
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


        /// <summary>
        /// Metoden starter animationen på "Flip patient" knappen 
        /// </summary>
        private void AnimateRotation()
        {
            RotatePlaceholder = null;
            IsAnimationPlaying = true;
            Thread.Sleep(1180);
            IsAnimationPlaying = false;
            RotatePlaceholder = "animated_rotate.gif";
        }


        /// <summary>
        /// Metoden kaldes når brugeren klikker på en eksistrende kollektion. Den valgte kollektion vises 
        /// </summary>
        /// <param name="Collection"></param>
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

        /// <summary>
        /// Metoden kaldes når der trykkes på "Add new collection" og der vises en pin så den nye samling kan placeres på kroppen. Hvis brugeren allerede er i gang med at placere en samling vil trykket gøre således at handlingen stoppes 
        /// </summary>
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


        /// <summary>
        /// Metoden opretter et nyt modermærkem tilføjer det til samlingen af modermærker og åbner Create Collection View'et. 
        /// </summary>
        /// <param name="Collection"></param>
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
    }
}