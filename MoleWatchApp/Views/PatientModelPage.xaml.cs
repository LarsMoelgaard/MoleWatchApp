using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MoleWatchApp.Extensions;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    public partial class PatientModelPage : ContentPage
    {
        private string result = "";
        
        public PatientModelPage() 
        {
            InitializeComponent();
        }

        private async void Checkmark_button_Clicked(object sender, EventArgs e)
        {
            result = await DisplayPromptAsync("Opret ny samling for modermærke", "Indtast navn på det valgte modermærke");

            if (result == null)
            {

            }
            else
            {

                double YVal = PatientModelImage.Y + PinImage.Y + TestKnap.Height / 2; // De her virker!


                double XVal =  PatientModelImage.X + PinImage.X; // De her virker Ca!!



                double[] PinPlacement = PinchPanContainer.getPinPlacement(XVal,YVal);

                TestKnap.TranslationX = PinPlacement[0];
                TestKnap.TranslationY = PinPlacement[1];

                PViewModel.CreateOkClicked.Execute(result);
            }
        }

        private void PatientModelPage_OnAppearing(object sender, EventArgs e)
        {
            PinchPanContainer.UpdateScreenSize();
        }
    }
}