using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DataClasses.DTO;
using DLToolkit.Forms.Controls;
using MoleWatchApp.ViewModels;
using SlideOverKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using DLToolkit.Forms.Controls.Helpers.ImageCropView;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;

namespace MoleWatchApp.Views
{
    public class CreateCollectionView : MenuContainerPage
    {

        private CreateCollectionViewModel CCVM;

        /// <summary>
        /// Oprettelse af createcollection siden med alle UI-elementer
        /// </summary>
        public CreateCollectionView()
        {
            CCVM = new CreateCollectionViewModel();
            this.BindingContext = CCVM;
            this.SetBinding(TitleProperty, "CollectionTitle");


            GridLengthTypeConverter gridLengthConverter = new GridLengthTypeConverter();
            

            Grid CreateCollectionSuperGrid = new Grid();

            CreateCollectionSuperGrid.BackgroundColor = Color.Gray;

            #region SuperRowAndColDef
            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = new GridLength(3, GridUnitType.Star);
            RowDefinition rowDef2 = new RowDefinition();
            rowDef2.Height = new GridLength(3, GridUnitType.Star);


            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength(1, GridUnitType.Star);


            CreateCollectionSuperGrid.RowDefinitions.Add(rowDef1);
            CreateCollectionSuperGrid.RowDefinitions.Add(rowDef2);



            CreateCollectionSuperGrid.ColumnDefinitions.Add(colDef1);
            CreateCollectionSuperGrid.ColumnDefinitions.Add(colDef2);



            #endregion

            ImageCropView LastCollectionPhoto = new ImageCropView();
            LastCollectionPhoto.SetBinding(ImageCropView.SourceProperty, "LastCollectionPhoto");
            LastCollectionPhoto.HeightRequest = 500;
            LastCollectionPhoto.WidthRequest = 400;
            //LastCollectionPhoto.PreviewTransformations = new List<ITransformation>() {new CircleTransformation()}; //TODO gør således man kan ændre croppet
            Grid.SetRow(LastCollectionPhoto, 0);
            Grid.SetColumn(LastCollectionPhoto, 0);
            Grid.SetColumnSpan(LastCollectionPhoto,2);
            CreateCollectionSuperGrid.Children.Add(LastCollectionPhoto);
            
           

            ImageButton CameraButton = new ImageButton();
            CameraButton.Source = "CameraIcon.png";
            CameraButton.BackgroundColor = Color.Transparent;
            CameraButton.HorizontalOptions = LayoutOptions.CenterAndExpand;
            CameraButton.VerticalOptions = LayoutOptions.CenterAndExpand;
            CameraButton.SetBinding(ImageButton.CommandProperty, "CameraButtonClicked");
            CameraButton.SetBinding(ImageButton.IsEnabledProperty, "NoImagesInCollection");
            CameraButton.SetBinding(ImageButton.IsVisibleProperty, "NoImagesInCollection");
            Grid.SetRow(CameraButton,0);
            Grid.SetColumn(CameraButton,0);
            CreateCollectionSuperGrid.Children.Add(CameraButton);


            ImageButton GalleryButton = new ImageButton();
            GalleryButton.Source = "GalleryIcon.png";
            GalleryButton.BackgroundColor = Color.Transparent;
            GalleryButton.HorizontalOptions = LayoutOptions.CenterAndExpand;
            GalleryButton.VerticalOptions = LayoutOptions.CenterAndExpand;
            GalleryButton.SetBinding(ImageButton.CommandProperty, "GalleryButtonClicked");
            GalleryButton.SetBinding(ImageButton.IsEnabledProperty, "NoImagesInCollection");
            GalleryButton.SetBinding(ImageButton.IsVisibleProperty, "NoImagesInCollection");
            Grid.SetRow(GalleryButton, 0);
            Grid.SetColumn(GalleryButton, 1);
            CreateCollectionSuperGrid.Children.Add(GalleryButton);

            Grid OptionsGrid = new Grid();
            OptionsGrid.BackgroundColor = Color.White;
            Grid.SetColumn(OptionsGrid,0);
            Grid.SetRow(OptionsGrid,1);
            Grid.SetColumnSpan(OptionsGrid,2);
            OptionsGrid.RowSpacing = 10;

            #region OptionRowAndColDef
            RowDefinition OptRowDef1 = new RowDefinition();
            OptRowDef1.Height = new GridLength(0.5, GridUnitType.Star);
            RowDefinition OptRowDef2 = new RowDefinition();
            RowDefinition OptRowDef3 = new RowDefinition();
            RowDefinition OptRowDef4 = new RowDefinition();
            OptRowDef2.Height = new GridLength(1, GridUnitType.Star);
            OptRowDef3.Height = new GridLength(1, GridUnitType.Star);
            OptRowDef4.Height = new GridLength(2, GridUnitType.Star);

            RowDefinition OptRowDef5 = new RowDefinition();
            OptRowDef5.Height = new GridLength(0.5, GridUnitType.Star);



            ColumnDefinition OptColDef1 = new ColumnDefinition();
            OptColDef1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition OptColDef2 = new ColumnDefinition();
            OptColDef2.Width = new GridLength(2, GridUnitType.Star);
            ColumnDefinition OptColDef3 = new ColumnDefinition();
            OptColDef3.Width = new GridLength(3, GridUnitType.Star);
            ColumnDefinition OptColDef4 = new ColumnDefinition();
            OptColDef4.Width = new GridLength(2, GridUnitType.Star);
            ColumnDefinition OptColDef5 = new ColumnDefinition();
            OptColDef5.Width = new GridLength(1, GridUnitType.Star);

            OptionsGrid.RowDefinitions.Add(OptRowDef1);
            OptionsGrid.RowDefinitions.Add(OptRowDef2);
            OptionsGrid.RowDefinitions.Add(OptRowDef3);
            OptionsGrid.RowDefinitions.Add(OptRowDef4);
            OptionsGrid.RowDefinitions.Add(OptRowDef5);

            OptionsGrid.ColumnDefinitions.Add(OptColDef1);
            OptionsGrid.ColumnDefinitions.Add(OptColDef2);
            OptionsGrid.ColumnDefinitions.Add(OptColDef3);
            OptionsGrid.ColumnDefinitions.Add(OptColDef4);
            OptionsGrid.ColumnDefinitions.Add(OptColDef5);


            #endregion

            //StackLayout createCollectionLayout = new StackLayout();
            //createCollectionLayout.Spacing = 5;
            //createCollectionLayout.Margin = 10;
            //Grid.SetColumn(createCollectionLayout, 0);
            //Grid.SetRow(createCollectionLayout, 0);
            //Grid.SetColumnSpan(createCollectionLayout, 5);



            Label DateLabel = new Label();
            DateLabel.SetBinding(Label.TextProperty, "DateText");
            DateLabel.FontSize = 20;
            DateLabel.HorizontalTextAlignment = TextAlignment.Center;
            Grid.SetColumn(DateLabel, 0);
            Grid.SetRow(DateLabel, 0);
            Grid.SetColumnSpan(DateLabel, 5);
            
            OptionsGrid.Children.Add(DateLabel);
            //createCollectionLayout.Children.Add(DateLabel);


            Button MarkCollectionButton = new Button();
            MarkCollectionButton.Text = "Markér samling";
            MarkCollectionButton.FontSize = 20;
            MarkCollectionButton.HeightRequest = 50;
            MarkCollectionButton.CornerRadius = 20;
            MarkCollectionButton.VerticalOptions = LayoutOptions.Center;
            MarkCollectionButton.SetBinding(Button.ImageSourceProperty, "MarkCollectionImage");
            MarkCollectionButton.SetBinding(Button.CommandProperty,"MarkCommand");
            //createCollectionLayout.Children.Add(MarkCollectionButton);
            Grid.SetColumn(MarkCollectionButton, 1);
            Grid.SetRow(MarkCollectionButton, 2);
            Grid.SetColumnSpan(MarkCollectionButton, 3);

            OptionsGrid.Children.Add(MarkCollectionButton);

            Button ShowPicturesCollection = new Button();
            ShowPicturesCollection.HeightRequest = 50;
            ShowPicturesCollection.CornerRadius = 20;
            ShowPicturesCollection.Text = "Vis billeder for samlingen";
            ShowPicturesCollection.FontSize = 20;
            ShowPicturesCollection.SetBinding(Button.CommandProperty, "ShowPictureCollectionCommand");
            Grid.SetColumn(ShowPicturesCollection, 1);
            Grid.SetRow(ShowPicturesCollection, 1);
            Grid.SetColumnSpan(ShowPicturesCollection, 3);

            OptionsGrid.Children.Add(ShowPicturesCollection);
            //createCollectionLayout.Children.Add(ShowPicturesCollection);



            ImageButton SettingsButton = new ImageButton();
            SettingsButton.HorizontalOptions = LayoutOptions.Center;
            SettingsButton.VerticalOptions = LayoutOptions.Center;
            SettingsButton.HeightRequest = 95;
            SettingsButton.WidthRequest = 95;
            SettingsButton.BackgroundColor = Color.Transparent;
            SettingsButton.Source = "settings.png";
            SettingsButton.Command = new Command(()=> ShowSettingsMenu());
            Grid.SetRow(SettingsButton,3);
            Grid.SetColumn(SettingsButton,1);

            
            ImageButton AddButton = new ImageButton();
            AddButton.HorizontalOptions = LayoutOptions.Center;
            AddButton.VerticalOptions = LayoutOptions.Center;
            AddButton.HeightRequest = 100;
            AddButton.WidthRequest = 100;
            AddButton.BackgroundColor = Color.Transparent;
            AddButton.Source = "Plus_icon.png";
            AddButton.Command = new Command(() => showAddPhotoMessage());
            Grid.SetRow(AddButton, 3);
            Grid.SetColumn(AddButton, 3);

            //OptionsGrid.Children.Add(createCollectionLayout);
            OptionsGrid.Children.Add(SettingsButton);
            OptionsGrid.Children.Add(AddButton);


            CreateCollectionSuperGrid.Children.Add(OptionsGrid);

            Content = CreateCollectionSuperGrid;

            this.SlideMenu = new SlideUpMenuView(this);

        }

        private async void showAddPhotoMessage()
        {
            string action = await DisplayActionSheet("Vælg metode til upload af billede:", "Fortryd", null, "Galleri", "Kamera");
           
            var ViewModel = (CreateCollectionViewModel)this.BindingContext;

            switch (action)
            {
                case "Kamera":

                    ViewModel.CameraButtonClicked.Execute(null);

                    break;
                case "Galleri":
                    
                    ViewModel.GalleryButtonClicked.Execute(null);

                    break;
            }
        }

        private void ShowSettingsMenu()
        {
            this.ShowMenu();
        }

        public async void DeleteButtonClicked()
        {
            bool answer = await DisplayAlert("Slet modermærke", "Ønsker du at slette modermærket?", "Ja", "Nej");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                CCVM.DeleteCollectionCommand.Execute(null);
            }
        }

        public async void RenameButtonClicked()
        {
            string result = await DisplayPromptAsync("Ændre navn for modermærke", "Angiv navn på modermærke:");
            if (!string.IsNullOrEmpty(result))
            {
                CCVM.ChangeNameCommand.Execute(result);
            }
        }

        public async void NotificationClicked()
        {

        }

    }
}
