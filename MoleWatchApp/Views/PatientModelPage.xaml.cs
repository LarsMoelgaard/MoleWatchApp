using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DataClasses.DTO;
using DataClasses.DTO.MISCDTOS;
using MoleWatchApp.Extensions;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    public partial class PatientModelPage : ContentPage
    {
        public PatientDataDTO patientData { get; set; }
        private string result = "";
        
        public PatientModelPage() 
        {
            InitializeComponent();
            //Shell.SetBackButtonBehavior(this, new BackButtonBehavior())
            //{
            //    Command = Binding = ""; //TODO find ud af hvordan man laver backbuttonbehaviour
            //}
        }

        private async void Checkmark_button_Clicked(object sender, EventArgs e)
        {
            result = await DisplayPromptAsync("Opret ny samling for modermærke", "Indtast navn på det valgte modermærke");

            if (result == null)
            {

            }
            else
            {
                CollectionDTO newCollectionDto = new CollectionDTO();
                newCollectionDto.CollectionName = result;

                LocationOnBodyDTO NewCollectionLocation = new LocationOnBodyDTO();
                
                double YVal = PatientModelImage.Y + PinImage.Y + 2.5; // De her virker!
                double XVal =  PatientModelImage.X + PinImage.X; // De her virker Ca!!



                int[] PinPlacement = PinchPanContainer.getPinPlacement(XVal,YVal);

                NewCollectionLocation.xCoordinate = PinPlacement[0];
                NewCollectionLocation.xCoordinate = PinPlacement[1];

                ImageButton NewCollectionButton = new ImageButton();


                PinchAndPanGrid.Children.Add(NewCollectionButton);

                NewCollectionButton.VerticalOptions = LayoutOptions.Start;
                NewCollectionButton.HorizontalOptions = LayoutOptions.Start;

                NewCollectionButton.WidthRequest = 5;
                NewCollectionButton.HeightRequest = 5;
                NewCollectionButton.Source = "NotMarkedCollection.png";

                NewCollectionButton.TranslationX = PinPlacement[0];
                NewCollectionButton.TranslationY = PinPlacement[1];


                newCollectionDto.Location = NewCollectionLocation;

                PViewModel.CreateOkClicked.Execute(newCollectionDto);
            }
        }

    }
}