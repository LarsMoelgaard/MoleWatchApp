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
                List<double> PinPlacement = PinchPanContainer.getPinPlacement();
            }
            else
            {
                PViewModel.CreateOkClicked.Execute(result);
            }
        }

        private void PatientModelPage_OnAppearing(object sender, EventArgs e)
        {
            PinchPanContainer.UpdateScreenSize();
        }
    }
}