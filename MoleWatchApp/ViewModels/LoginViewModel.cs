using MoleWatchApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Models;
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


        public Command LoginCommand { get; }

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

            loginModel = new LoginModel();

        }

        private async void OnLoginClicked(object obj)
        {
            //TODO verify logininformation.

            if (loginModel.VerifyPassword(UsernameInput, Password))
            {
                //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                
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
    }
}
