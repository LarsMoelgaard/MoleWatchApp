﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using DataClasses.DTO;
using MoleWatchApp.Extensions;
using MoleWatchApp.Interfaces;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;


namespace MoleWatchApp.Views
{
    /// <summary>
    /// Opretter view for patientmodel 
    /// </summary>
    public partial class PatientModelView : ContentPage
    {
        /// <summary>
        /// default constructor til viewet som sætter bindingcontexten
        /// </summary>
        public PatientModelView() 
        {
            InitializeComponent();
            PViewModel = new PatientModelViewModel();
            BindingContext = PViewModel;

        }
        /// <summary>
        /// Listen af alle knapperne/modermærkerne på patientmodellen
        /// </summary>
        private List<ImageButton> PatientButtonList = new List<ImageButton>();

        /// <summary>
        /// Reference til Viewmodellen så den kan eksekverer kommandoer
        /// </summary>
        private IPatientViewModel PViewModel;
        
        /// <summary>
        /// Bool der sikrer at PatientModelViewet ikke eksekverer UpdateTable() før siden er synlig på skærmen.
        /// </summary>
        private bool IsVisible = false;





        /// <summary>
        /// Metoden kaldes når brugeren trykker Checkmark-button for at tilføje en ny samling 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Checkmark_button_Clicked(object sender, EventArgs e)
        {

            string result = await DisplayPromptAsync("Opret ny samling for modermærke", "Indtast navn på det valgte modermærke");

            if (result == null)
            {

            }
            else
            {
                CollectionDTO newCollectionDto = new CollectionDTO();

                LocationOnBodyDTO NewCollectionLocation = new LocationOnBodyDTO();
                
                double YVal = PatientModelImage.Y + PinImage.Y + 2.5; // De her virker!
                double XVal =  PatientModelImage.X + PinImage.X; // De her virker Ca!!

                int[] PinPlacement = PinchPanContainer.getPinPlacement(XVal,YVal);

                //gøres for at gøre samlingernes koordinater relative til skærmstørrelse og telefonens aspect ratio
                int relativeXCoordinate = Convert.ToInt32( PinPlacement[0] / PatientModelImage.Width * 10000); 
                int relativeYCoordinate = Convert.ToInt32(PinPlacement[1] / PatientModelImage.Height * 10000);


                NewCollectionLocation.xCoordinate = relativeXCoordinate;
                NewCollectionLocation.yCoordinate = relativeYCoordinate;

                newCollectionDto.Location = NewCollectionLocation;

                string bodypart = GetBodypart(relativeXCoordinate, relativeYCoordinate, PatientModelImage.Source);
                if (result == "")
                {
                    newCollectionDto.CollectionName = bodypart;
                }
                else
                {
                    newCollectionDto.CollectionName = result;
                }

                string collectionName = newCollectionDto.CollectionName;

                int SameName = 0;

                foreach (CollectionDTO collection in HiddenListView.ItemsSource)
                {
                    if (collection.CollectionName.Contains(collectionName))
                    {
                        SameName++;
                    }
                }

                if (SameName > 0)
                {
                    collectionName += Convert.ToString(" " + SameName);
                }

                if (result != collectionName)
                {
                    await DisplayAlert("Ugyldigt navn på samling",
                        "Navnet på samlingen er enten tomt eller ugyldigt. Samlingen har fået autogenereret et navn, som eventuelt kan ændres senere", "OK");
                }

                newCollectionDto.CollectionName = collectionName;


                newCollectionDto.Location.BodyPart = bodypart;
                if (relativeXCoordinate > 5000)
                {
                    newCollectionDto.Location.BodyPartSide = "Venstre";
                }
                else
                {
                    newCollectionDto.Location.BodyPartSide = "Højre";
                }

                PViewModel.CreateOkClicked.Execute(newCollectionDto);
            }
        }

        /// <summary>
        /// Metoden kaldes når patient model view oprettes - opdatere alle modermærker på patienten 
        /// </summary>
        private void PatientModelPage_OnAppearing(object sender, EventArgs e)
        {
            if (IsVisible)
            {
                PViewModel.OnPageAppearingCommand.Execute(null);

                UpdateButtonsOnPatientImage();
            }
            else
            {
                IsVisible = true;
                UpdateButtonsOnPatientImage();
            }
            
        }


        /// <summary>
        ///  Binding til skjult listview så modermærkerne på kroppen så de bliver opdateret når der tilføjes nye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            UpdateButtonsOnPatientImage();
        }


        /// <summary>
        /// Metoden vender billedet af patienten om og opdatere modermærkerne på kroppen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateImageButton_OnClicked(object sender, EventArgs e)
        {
            PViewModel.RotateClicked.Execute(null);

            UpdateButtonsOnPatientImage();
        }


        /// <summary>
        /// Opdatere alle modermærker på kroppen 
        /// </summary>
        private async void UpdateButtonsOnPatientImage()
        {
            if (HiddenListView.ItemsSource != null)
            {

                await Task.Delay(60); //Gøres for at sikre at patientbilledet er opdateret før knapperne laves


                foreach (ImageButton existingImageButton in PatientButtonList)
                {
                    PinchAndPanGrid.Children.Remove(existingImageButton);
                }


                    foreach (CollectionDTO item in HiddenListView.ItemsSource)
                    {
                        if (item.Location.IsFrontFacing &&
                            PatientModelImage.Source.ToString().ToLower().Contains("front")
                            || !item.Location.IsFrontFacing &&
                            PatientModelImage.Source.ToString().ToLower().Contains("back")
                        )
                        {
                            ImageButton NewCollectionButton = new ImageButton();

                            PinchAndPanGrid.Children.Add(NewCollectionButton);
                            NewCollectionButton.VerticalOptions = LayoutOptions.Start;
                            NewCollectionButton.HorizontalOptions = LayoutOptions.Start;

                            NewCollectionButton.WidthRequest = 5;
                            NewCollectionButton.HeightRequest = 5;

                            if (item.IsMarked)
                            {
                                NewCollectionButton.Source = "MarkedCollection.png";
                            }
                            else
                            {
                                NewCollectionButton.Source = "NotMarkedCollection.png";
                            }


                            //gøres for at gøre samlingernes koordinater relative til skærmstørrelse og telefonens aspect ratio
                            double RelativeXCoordinate = Convert.ToDouble(item.Location.xCoordinate) / 10000 *
                                                         PatientModelImage.Width;
                            double RelativeYCoordinate = Convert.ToDouble(item.Location.yCoordinate) / 10000 *
                                                         PatientModelImage.Height;

                            NewCollectionButton.TranslationX = Convert.ToInt32(RelativeXCoordinate);
                            NewCollectionButton.TranslationY = Convert.ToInt32(RelativeYCoordinate);

                            NewCollectionButton.CommandParameter = item;



                            NewCollectionButton.SetBinding(ImageButton.CommandProperty,
                                new Binding("ExistingCollectionClicked"));




                        PatientButtonList.Add(NewCollectionButton);

                    }



                }
            }
        }

        /// <summary>
        /// Metoden åbner en hjælpe-menu med forklaring af hvordan programmet anvendes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ShowHelpMenu(object sender, EventArgs e)
        {

            await DisplayAlert("Hjælp:",
                "-   Brug 1 finger til at navigere rundt på modellen \r\n"
                + "-   Brug 2 fingre til at zoome ind/ud på modellen \r\n"
                + "-   Tryk på en prik for at se et specifikt modermærke \r\n\r\n"
                + "-   Tryk på det blå plus for at kunne tilføje et nyt modersmærke. " +
                "Når nålen er placeret korrekt, tryk på det grønne flueben for at oprette samlingen\r\n"+
                  "-   Tryk på de 2 blå pile for at vende modellen om"

                , "OK");
        }


        /// <summary>
        /// Metoden henter navnet på den kropsdel hvor modermærket er placeret ud fra modermærkets koordinator 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetBodypart(int x, int y, ImageSource source)
        {
            PixelImageHandler imageHandler = new PixelImageHandler(source);
            var color = imageHandler.getPixelValue(x, y);

            return color;
        }
    }
}