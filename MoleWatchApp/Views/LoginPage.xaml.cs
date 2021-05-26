using MoleWatchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleWatchApp.Interfaces.IViewModel;
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

        private ILoginViewModel LoginVM;

        /// <summary>
        /// Default constructor til siden
        /// </summary>
        public LoginPage()
        {
            LoginVM = new LoginViewModel();
            this.BindingContext = LoginVM;

            InitializeComponent();
            

            MessagingCenter.Subscribe<LoginViewModel,string>(this,"SmartLoginMessage", async (sender, arg) =>
            {
                switch (arg)
                {
                    case "SuccesfulBiometric":
                        await DisplayAlert("Succes","Loginnet er godkendt", "OK");
                        break;
                    case "BiometricFailed":
                        await DisplayAlert("Fejl", "Loginnet blev ikke godkendt", "OK");
                        break;
                    case "NoBiometricDataFound":
                        await DisplayAlert("Fejl", "Biometrisk godkendelse er ikke tilgængeligt eller ikke konfigureret på dette device.", "OK");
                        break;
                }
            });

        }


        /// <summary>
        /// Metoden kaldes når brugeren skriver noget ind i brugernavn-feltet, og opdaterer teksten så den står korrekt med bindestreg. Dette gøres for at sikre at CPR-nummeret er i det rigtige format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (e.NewTextValue.Length >= 12)
            {
                ((Entry)sender).Text = e.OldTextValue;
            }


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