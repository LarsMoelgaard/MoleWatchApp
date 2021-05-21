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
        private string doctorName;
        private string doctorAdress;
        private string openingHours;
        private string mobileNumber;

        public ContactDoctorViewModel()
        {
            ContactDoctorModel doctorModel = new ContactDoctorModel();
            DoctorContactInfoDTO doctorInfo = doctorModel.GetDoctorInfo();

            string website = doctorInfo.Website.Contains("www.") ? doctorInfo.Website : "www." + doctorInfo.Website;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("" + website));

            DoctorName = doctorInfo.MedicalPracticeName;
            DoctorAdress = doctorInfo.Adress;

            string OpenInfo = "";
            foreach (string openingHour in doctorInfo.OpeningHours)
            {
                OpenInfo += (openingHour + "\n");
            }

            OpeningHours = OpenInfo;
        }

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
    }
}
