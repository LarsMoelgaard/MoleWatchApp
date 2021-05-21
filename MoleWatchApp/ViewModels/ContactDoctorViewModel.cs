using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class ContactDoctorViewModel : BaseViewModel
    {
        public ContactDoctorViewModel()
        {
            ContactDoctorModel doctorModel = new ContactDoctorModel();
            DoctorContactInfoDTO doctorInfo = doctorModel.GetDoctorInfo();

            string website = !doctorInfo.Website.Contains("www.") ? doctorInfo.Website : doctorInfo.Website.Remove(0,4);
           

            //Opens website for doctor 
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(new Uri("https://" + website)));
            CallNumber = new Command(Call);

            DoctorName = doctorInfo.MedicalPracticeName;
            DoctorAdress = doctorInfo.Adress;
            MobileNumber = doctorInfo.PhoneNumber;

            string OpenInfo = "";
            foreach (string openingHour in doctorInfo.OpeningHours)
            {
                OpenInfo += (openingHour + "\n");
            }

            OpeningHours = OpenInfo;
        }

        #region Properties

        private string doctorName;
        private string doctorAdress;
        private string openingHours;
        private string mobileNumber;


        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }
            set
            {
                mobileNumber = value;
                this.OnPropertyChanged();
            }
        }

        public string OpeningHours
        {
            get
            {
                return openingHours;
            }
            set
            {
                openingHours = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand OpenWebCommand { get; }
        public ICommand CallNumber { get; }

        public string DoctorName
        {
            get
            {
                return doctorName;
            }
            set
            {
                doctorName = value;
                this.OnPropertyChanged();
            }
        }

        public string DoctorAdress
        {
            get
            {
                return doctorAdress;
            }
            set
            {
                doctorAdress = value;
                this.OnPropertyChanged();
            }
        }

        #endregion Region

        private void Call()
        {
            try
                {
                    PhoneDialer.Open(MobileNumber);
                }
                catch (ArgumentNullException anEx)
                {
                    // Number was null or white space
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Phone Dialer is not supported on this device.
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }
            
        }
    }
}
