using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class PatientModelViewModel : BaseViewModel
    {


        private string patientPicture;
        private string rotatePlaceholder;
        private string newPinAdded;
        private string plusIcon;
        private string checkmark;

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

        public Command RotateClicked { get; }
        public Command PlusClicked { get; }

        public ICommand CreateOkClicked
        {
            get
            {
                return new Command<string>((x)=> CreateCollection(x));
            }
        }


        private bool isAnimationPlaying;
        private bool IsPatientMale;
        private bool IsPatientFrontFacing;
        private bool CreateCollectionInProgress;



        public PatientModelViewModel()
        {

            IsAnimationPlaying = false;
            Title = "Vælg Modermærke";
            RotatePlaceholder = "animated_rotate.gif";


            PatientPicture = "MaleFrontCrop.png";
            PlusIcon = "Plus_Icon.png";
            IsPatientFrontFacing = true;
            IsPatientMale = true;    //TODO skal ændres senere
            CreateCollectionInProgress = false;



            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            RotateClicked = new Command(FlipPatient);
            PlusClicked = new Command(onPlusClicked);
        }

        private async void FlipPatient(object obj)
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
        //public ICommand OpenWebCommand { get; }

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

        private void CreateCollection(string CollectionName)
        {
            NewPinAdded = null;
            Checkmark = null;
            PlusIcon = "Plus_icon.png";
            CreateCollectionInProgress = false;

            if (CollectionName == "")
            {
                CollectionName = "AutoNavn";
            }
            else
            {

            }
        }
    }
}