using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;
using MoleWatchApp.ViewModels;
using SlideOverKit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoleWatchApp.Views
{
    public class CreateCollectionPage2 : MenuContainerPage
    {
        public CreateCollectionPage2()
        {
            this.BindingContext = new CreateCollectionViewModel();
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

            Image LastCollectionPhoto = new Image();
            LastCollectionPhoto.SetBinding(Image.SourceProperty,"LastPhotoSource");
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
            Grid.SetRow(CameraButton,0);
            Grid.SetColumn(CameraButton,0);
            CreateCollectionSuperGrid.Children.Add(CameraButton);


            ImageButton GalleryButton = new ImageButton();
            GalleryButton.Source = "GalleryIcon.png";
            GalleryButton.BackgroundColor = Color.Transparent;
            GalleryButton.HorizontalOptions = LayoutOptions.CenterAndExpand;
            GalleryButton.VerticalOptions = LayoutOptions.CenterAndExpand;
            GalleryButton.SetBinding(ImageButton.IsEnabledProperty, "NoImagesInCollection");
            Grid.SetRow(GalleryButton, 0);
            Grid.SetColumn(GalleryButton, 1);
            CreateCollectionSuperGrid.Children.Add(GalleryButton);

            Grid OptionsGrid = new Grid();
            OptionsGrid.BackgroundColor = Color.White;
            Grid.SetColumn(OptionsGrid,0);
            Grid.SetRow(OptionsGrid,1);
            Grid.SetColumnSpan(OptionsGrid,2);


            #region OptionRowAndColDef
            RowDefinition OptRowDef1 = new RowDefinition();
            OptRowDef1.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition OptRowDef2 = new RowDefinition();
            OptRowDef2.Height = new GridLength(3, GridUnitType.Star);
            RowDefinition OptRowDef3 = new RowDefinition();
            OptRowDef3.Height = new GridLength(2, GridUnitType.Star);



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

            OptionsGrid.ColumnDefinitions.Add(OptColDef1);
            OptionsGrid.ColumnDefinitions.Add(OptColDef2);
            OptionsGrid.ColumnDefinitions.Add(OptColDef3);
            OptionsGrid.ColumnDefinitions.Add(OptColDef4);
            OptionsGrid.ColumnDefinitions.Add(OptColDef5);


            #endregion

            StackLayout createCollectionLayout = new StackLayout();
            Grid.SetColumn(createCollectionLayout, 0);
            Grid.SetRow(createCollectionLayout, 0);
            Grid.SetColumnSpan(createCollectionLayout, 5);

            Label DateLabel = new Label();
            DateLabel.SetBinding(Label.TextProperty, "DateText");
            DateLabel.FontSize = 25;
            DateLabel.HorizontalTextAlignment = TextAlignment.Center;

            createCollectionLayout.Children.Add(DateLabel);


            Button MarkCollectionButton = new Button();
            MarkCollectionButton.Text = "Markér samling";
            MarkCollectionButton.FontSize = 20;
            MarkCollectionButton.SetBinding(Button.ImageSourceProperty, "MarkCollectionImage");
            MarkCollectionButton.SetBinding(Button.CommandProperty,"MarkCommand");

            createCollectionLayout.Children.Add(MarkCollectionButton);

            Button ShowPicturesCollection = new Button();
            ShowPicturesCollection.Text = "Vis billeder for samlingen";
            ShowPicturesCollection.FontSize = 20;
            ShowPicturesCollection.SetBinding(Button.CommandProperty, "ShowPictureCollectionCommand");

            createCollectionLayout.Children.Add(ShowPicturesCollection);



            ImageButton SettingsButton = new ImageButton();
            SettingsButton.HorizontalOptions = LayoutOptions.Center;
            SettingsButton.VerticalOptions = LayoutOptions.Center;
            SettingsButton.HeightRequest = 95;
            SettingsButton.WidthRequest = 95;
            SettingsButton.BackgroundColor = Color.Transparent;
            SettingsButton.Source = "settings.png";
            SettingsButton.Command = new Command(()=> ShowSettingsMenu());
            Grid.SetRow(SettingsButton,2);
            Grid.SetColumn(SettingsButton,1);

            
            ImageButton AddButton = new ImageButton();
            AddButton.HorizontalOptions = LayoutOptions.Center;
            AddButton.VerticalOptions = LayoutOptions.Center;
            AddButton.HeightRequest = 100;
            AddButton.WidthRequest = 100;
            AddButton.BackgroundColor = Color.Transparent;
            AddButton.Source = "Plus_icon.png";
            Grid.SetRow(AddButton, 2);
            Grid.SetColumn(AddButton, 3);

            OptionsGrid.Children.Add(createCollectionLayout);
            OptionsGrid.Children.Add(SettingsButton);
            OptionsGrid.Children.Add(AddButton);


            CreateCollectionSuperGrid.Children.Add(OptionsGrid);

            Content = CreateCollectionSuperGrid;

            this.SlideMenu = new SlideUpMenuView();

        }


        private void ShowSettingsMenu()
        {
            this.ShowMenu();
        }

    }
}
