using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class PatientModelViewModel : BaseViewModel
    {


        private string patientPicture;

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

        public Command RotateClicked { get; }

        private bool IsPatientMale;
        private bool IsPatientFrontFacing;

        public PatientModelViewModel()
        {


            Title = "Vælg Modermærke";
            
            
            PatientPicture = "MaleFrontCrop.png";
            IsPatientFrontFacing = true;
            IsPatientMale = true;    //TODO skal ændres senere

            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            RotateClicked = new Command(FlipPatient);
        }

        private async void FlipPatient(object obj)
        {
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
        

        //public ICommand OpenWebCommand { get; }
    }
}