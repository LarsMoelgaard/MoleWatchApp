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
        private bool IsVisible = false;
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
                CollectionDTO newCollectionDto = new CollectionDTO();
                newCollectionDto.CollectionName = result;

                LocationOnBodyDTO NewCollectionLocation = new LocationOnBodyDTO();
                
                double YVal = PatientModelImage.Y + PinImage.Y + 2.5; // De her virker!
                double XVal =  PatientModelImage.X + PinImage.X; // De her virker Ca!!



                int[] PinPlacement = PinchPanContainer.getPinPlacement(XVal,YVal);

                NewCollectionLocation.xCoordinate = PinPlacement[0];
                NewCollectionLocation.xCoordinate = PinPlacement[1];

                //ImageButton NewCollectionButton = new ImageButton();


                //PinchAndPanGrid.Children.Add(NewCollectionButton);

                //NewCollectionButton.VerticalOptions = LayoutOptions.Start;
                //NewCollectionButton.HorizontalOptions = LayoutOptions.Start;

                //NewCollectionButton.WidthRequest = 5;
                //NewCollectionButton.HeightRequest = 5;
                //NewCollectionButton.Source = "NotMarkedCollection.png";

                //NewCollectionButton.TranslationX = PinPlacement[0];
                //NewCollectionButton.TranslationY = PinPlacement[1];


                //newCollectionDto.Location = NewCollectionLocation;

                PViewModel.CreateOkClicked.Execute(newCollectionDto);
            }
        }


        private void PatientModelPage_OnAppearing(object sender, EventArgs e)
        {
            if (IsVisible)
            {
                PViewModel.OnPageAppearingCommand.Execute(null);
            }
            else
            {
                IsVisible = true;
            }
            
        }

        private void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                foreach (CollectionDTO item in HiddenListView.ItemsSource)
                {
                    ImageButton NewCollectionButton = new ImageButton();


                    PinchAndPanGrid.Children.Add(NewCollectionButton);

                    NewCollectionButton.VerticalOptions = LayoutOptions.Start;
                    NewCollectionButton.HorizontalOptions = LayoutOptions.Start;

                    NewCollectionButton.WidthRequest = 5;
                    NewCollectionButton.HeightRequest = 5;
                    NewCollectionButton.Source = "NotMarkedCollection.png";

                    NewCollectionButton.TranslationX = item.Location.xCoordinate;
                    NewCollectionButton.TranslationY = item.Location.yCoordinate;


                    //TODO differntiere i mellem knapperne og gør at de kan starte en command

                }
            }

        }
    }
}