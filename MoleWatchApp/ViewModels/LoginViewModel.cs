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


        private Color usernameLabelColor;
        private string usernameInput;
        private string password;



        public Command LoginCommand { get; }

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

        public string UsernameLabel { get; set; }


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

        public LoginViewModel()
        {
            UsernameLabel = "Indtast CPR-nummer";
            LoginCommand = new Command(OnLoginClicked);
            loginModel = new LoginModel();
        }

        private async void OnLoginClicked(object obj)
        {
            //TODO verify logininformation.

            if (loginModel.VerifyPassword(UsernameInput, Password))
            {
                UsernameLabel = "Lol det virker";
                
                UsernameInput = "Test";
                //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Forkert Login",
                    "Loginoplysningerne var forkerte. Indtast dem igen", "OK");
                UsernameLabelColor = Color.Crimson;
                UsernameInput = "";
                Password = "";
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }
    }
}
