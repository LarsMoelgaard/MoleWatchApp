﻿using MoleWatchApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Models;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private LoginModel loginModel;

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
                //bool IsNumericalValue = false;
                //try
                //{
                //    Convert.ToInt32(value[value.Length - 1]);
                //    IsNumericalValue = true;
                //}
                //catch (InvalidOperationException e)
                //{
                    
                //}

                //if (IsNumericalValue || value[value.Length - 1] == '-')
                //{
                    
                    
                //}
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

        public LoginViewModel()
        {
            UsernameLabel = "Indtast CPR-nummer:";
            PasswordLabel = "Indtast password:";

            


            LoginCommand = new Command(OnLoginClicked);
            SmartLoginCommand = new Command(AuthButton_OnClicked);
            loginModel = new LoginModel();


            UsernameInput = "12345";
            Password = "12345";
        }

        private async void OnLoginClicked(object obj)
        {
            //TODO verify logininformation.

            if (loginModel.VerifyPassword(UsernameInput, Password))
            {
                await Shell.Current.GoToAsync($"//{nameof(PatientModelPage)}");
                
            }
            else
            {

                // Hvordan man laver en dialogboks med en enkelt knap.
                //Application.Current.MainPage.DisplayAlert("Forkert Login",
                //    "Loginoplysningerne var forkerte. Indtast dem igen", "OK");
                UsernameLabel = "loginoplysninger var forkerte. indtast CPR-nummer igen:";
                UsernameLabelColor = Color.Crimson;
                PasswordLabelColor = Color.Crimson;

                UsernameInput = "";
                Password = "";
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }


        private async void AuthButton_OnClicked(object sender)
        {
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (!isFingerprintAvailable)
            {
                MessagingCenter.Send(this, "SmartLoginMessage", "NoBiometricDataFound");
                return;
            }

            AuthenticationRequestConfiguration conf =
                new AuthenticationRequestConfiguration("Authentication",
                    "Authenticate access to your personal data");

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
            if (authResult.Authenticated)
            {
                //Success  
                //
                //TODO benyt sessionID til at verificere brugeren af dette device.
                MessagingCenter.Send(this, "SmartLoginMessage", "SuccesfulBiometric");
            }
            else
            {
                MessagingCenter.Send(this,"SmartLoginMessage","BiometricFailed");
            }
        }


    }
}
