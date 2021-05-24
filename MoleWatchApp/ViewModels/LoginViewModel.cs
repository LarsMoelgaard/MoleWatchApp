using MoleWatchApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataClasses.DTO.MISCDTOS;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    /// <summary>
    /// ViewModel for LoginView 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Properties mm
        private ILogin loginModel;
        private Color passwordLabelColor;
        private Color usernameLabelColor;
        private string usernameInput;
        private string password;
        private string usernameLabel;
        private string passwordLabel;
        private bool biometricValue;

        public bool BiometricValue
        {
            get
            {
                return biometricValue;
            }
            set
            {
                biometricValue = value;
                this.OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }
        public Command SmartLoginCommand { get; }

        public string UsernameInput
        {
            get
            {
                return usernameInput;
            }
            set
            {
                usernameInput = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
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

        public string UsernameLabel
        {
            get
            {
                return usernameLabel;
            }
            set
            {
                usernameLabel = value;
                this.OnPropertyChanged();
            }
        }

        public string PasswordLabel
        {
            get
            {
                return passwordLabel;
            }
            set
            {
                passwordLabel = value;
                this.OnPropertyChanged();
            }
        }


        public Color UsernameLabelColor
        {
            get
            {
                return usernameLabelColor;
            }
            set
            {
                usernameLabelColor = value;
                this.OnPropertyChanged();
            }
        }


        public Color PasswordLabelColor
        {
            get
            {
                return passwordLabelColor;
            }
            set
            {
                passwordLabelColor = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        public LoginViewModel()
        {
            UsernameLabel = "Indtast CPR-nummer:";
            PasswordLabel = "Indtast password:";

            LoginCommand = new Command(OnLoginClicked);
            SmartLoginCommand = new Command(AuthButton_OnClicked);
            loginModel = LoginSingleton.GetLoginModel();

            //Udfylder automatisk brugernavn og kode for at logge hurtigt ind - skal slettes når koden skal ud til bruger 
            UsernameInput = "12345";
            Password = "12345";
        }


        /// <summary>
        /// Metoden kaldes når brugeren trykker login. 
        /// </summary>
        /// <param name="obj"></param>
        private async void OnLoginClicked(object obj)
        {
            
            BaseIsBusy = true;
            await Task.Delay(1); //Indsat delay så Activity indicator virker - Ved ikke helt hvorfor.

            bool LoginResult = loginModel.VerifyPassword(UsernameInput, Password);

            if (LoginResult)
            {

                BaseIsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(PatientModelView)}");

            }
            else
            {
                UsernameLabel = "loginoplysninger var forkerte. indtast CPR-nummer igen:";
                UsernameLabelColor = Color.Crimson;
                PasswordLabelColor = Color.Crimson;

                UsernameInput = "";
                Password = "";

                BaseIsBusy = false;
            }
        }



        /// <summary>
        /// Metoden kaldes når brugeren trykker på fingeraftrykket for at logge ind med smartlogin 
        /// </summary>
        /// <param name="sender"></param>
        private async void AuthButton_OnClicked(object sender)
        {
            BaseIsBusy = true;

            await Task.Delay(1); //Indsat delay så Activity indicator virker


            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (!isFingerprintAvailable)
            {
                MessagingCenter.Send(this, "SmartLoginMessage", "NoBiometricDataFound");
                BaseIsBusy = false;
                return;
            }

            AuthenticationRequestConfiguration conf =
                new AuthenticationRequestConfiguration("Authentication",
                    "Authenticate access to your personal data");

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);

            bool SessionIdValidated = loginModel.VerifySmartLoginPassword();


            if (authResult.Authenticated && SessionIdValidated)
            {
             
               MessagingCenter.Send(this, "SmartLoginMessage", "SuccesfulBiometric");

                BaseIsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(PatientModelView)}");
            }
            else
            {
                BaseIsBusy = false;
                MessagingCenter.Send(this,"SmartLoginMessage","BiometricFailed");
            }
        }


    }
}
