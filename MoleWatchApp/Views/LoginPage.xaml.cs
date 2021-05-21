using MoleWatchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    /// <summary>
    /// Opretter view til loginsiden 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

            MessagingCenter.Subscribe<LoginViewModel,string>(this,"SmartLoginMessage", async (sender, arg) =>
            {
                switch (arg)
                {
                    case "SuccesfulBiometric":
                        await DisplayAlert("Succes","Authentication succeeded", "OK");
                        break;
                    case "BiometricFailed":
                        await DisplayAlert("Error", "Authentication failed", "OK");
                        break;
                    case "NoBiometricDataFound":
                        await DisplayAlert("Error", "Biometric authentication is not available or is not configured.", "OK");
                        break;
                }
            });

        }


        /// <summary>
        /// Metoden kaldes når brugeren skriver noget ind i brugernavn-feltet, og opdateres teksten så den står korrekt med bindestreg. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            string NewInput = e.NewTextValue;

            if (!char.IsNumber(e.NewTextValue[e.NewTextValue.Length - 1]) && e.NewTextValue[e.NewTextValue.Length - 1] != '-')
            {
                ((Entry)sender).Text = e.OldTextValue;
            }

            if (e.NewTextValue.Length > 5 && e.NewTextValue.Length > e.OldTextValue.Length && e.NewTextValue.Length == 6)
            {
                ((Entry)sender).Text = e.NewTextValue + "-";
            }

            if (e.NewTextValue.Length == 7 && !e.NewTextValue.Contains('-') && e.NewTextValue.Length > e.OldTextValue.Length)
            {
                string TextValueWithDash = "";
                for (int i = 0; i < 6; i++)
                {
                    TextValueWithDash += Convert.ToString(e.NewTextValue[i]);
                }

                TextValueWithDash += "-" + Convert.ToString(e.NewTextValue[6]);

                ((Entry)sender).Text = TextValueWithDash;
            }
        }
    }
}