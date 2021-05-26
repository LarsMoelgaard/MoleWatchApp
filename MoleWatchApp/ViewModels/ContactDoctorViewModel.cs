using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Interfaces.IModel;
using MoleWatchApp.Interfaces.IViewModel;
using MoleWatchApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    /// <summary>
    ///  Viewmodel for contact doctor view 
    /// </summary>
    public class ContactDoctorViewModel : BaseViewModel, IContactDoctorViewModel
    {
        /// <summary>
        /// Default constructor til Viewet  
        /// </summary>
        public ContactDoctorViewModel() 
        {
            IContactDoctorModel doctorModel = new ContactDoctorModel();
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

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string doctorName;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string doctorAdress;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string openingHours;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string mobileNumber;

        /// <summary>
        /// Property der indholder telefonnummeret på lægens praksis
        /// </summary>
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
        
        /// <summary>
        /// Property der indeholder læge-praksisens åbningstider
        /// </summary>
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



        /// <summary>
        /// Kommando der eksekveres når "Besøg hjemmeside"-knappen bliver trykket på
        /// </summary>
        public ICommand OpenWebCommand { get; }



        /// <summary>
        /// Kommando der eksekveres når "Ring op"-knappen bliver trykket på
        /// </summary>
        public ICommand CallNumber { get; }

        /// <summary>
        /// Property der indeholder lægepraksisens navn
        /// </summary>
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


        /// <summary>
        /// Property der indeholder lægepraksisens adresse
        /// </summary>
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


        /// <summary>
        /// Metoden åbner mobilnummeret så det kan ringes op. 
        /// </summary>
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
