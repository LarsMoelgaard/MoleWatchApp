using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoleWatchApp.ViewModels;
using SlideOverKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MoleWatchApp.Views
{
    /// <summary>
    /// Opretter slide up menu til at styre indstilling for samlingen 
    /// </summary>
    public partial class SlideUpMenuView : SlideMenuView
    {
        /// <summary>
        /// Reference til Viewet hvori slideup-menuen kommer fra
        /// </summary>
        private CreateCollectionView collection;



        ////private bool hidePicker;

        ////public bool HidePicker
        ////{
        ////    get
        ////    {
        ////        return hidePicker;
        ////    }
        ////    set
        ////    {
        ////        hidePicker = value;
        ////        this.OnPropertyChanged();
        ////    }
        ////}

        public SlideUpMenuView (CreateCollectionView CollectionView)
        {
            InitializeComponent();
            // You must set HeightRequest in this case
            this.HeightRequest = 250;
            // You must set IsFullScreen in this case, 
            // otherwise you need to set WidthRequest, 
            // just like the QuickInnerMenu sample
            this.IsFullScreen = true;
            this.MenuOrientations = MenuOrientation.BottomToTop;

            // You must set BackgroundColor, 
            // and you cannot put another layout with background color cover the whole View
            // otherwise, it cannot be dragged on Android
            this.BackgroundColor = Color.Black;
            this.BackgroundViewColor = Color.Transparent;

            // In some small screen size devices, the menu cannot be full size layout.
            // In this case we need to set different size for Android.
            if (Device.RuntimePlatform == Device.Android)
                this.HeightRequest += 50;

            this.collection = CollectionView;
        }


        /// <summary>
        /// Ændre navn på samling 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RenameButton_OnClicked(object sender, EventArgs e)
        {
            collection.RenameButtonClicked();
        }


        /// <summary>
        /// Åbner notifikationsside, så der kan ændres i hyppighed af notifikationen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NotificationButton_OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NotificationView)}");
        }

        /// <summary>
        /// Sletter den valgte samling 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            collection.DeleteButtonClicked();
        }

    }
}

