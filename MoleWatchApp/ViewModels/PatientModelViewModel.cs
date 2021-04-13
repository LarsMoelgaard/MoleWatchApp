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
        public Command PatientClicked { get; }
        public Command PlusClicked { get; }

        private bool isAnimationPlaying;
        private bool IsPatientMale;
        private bool IsPatientFrontFacing;
        private bool AddCollectionInProgress;


        public PatientModelViewModel()
        {

            IsAnimationPlaying = false;
            Title = "Vælg Modermærke";
            RotatePlaceholder = "animated_rotate.gif";


            PatientPicture = "MaleFrontCrop.png"; //TODO skal ændres senere
            IsPatientFrontFacing = true;
            IsPatientMale = true;    //TODO skal ændres senere
            AddCollectionInProgress = false;

            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart")); //TODO Skal evt. bruges til åbning af doctor info
            RotateClicked = new Command(FlipPatient);
            PlusClicked = new Command(AddClicked);
            PatientClicked = new Command(PatientModelClicked);
            
            
        }

        private void AddClicked()
        {
            if (AddCollectionInProgress)
            {
                AddCollectionInProgress = false;
            }
            else
            {
                AddCollectionInProgress = true;
                Title = "Placér modermærke på kroppen";

            }
        }

        private void PatientModelClicked()
        {
            Title = "Lol det virker";
            // TODO find en måde at registrere koordinatet for touchet.
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
    }
}