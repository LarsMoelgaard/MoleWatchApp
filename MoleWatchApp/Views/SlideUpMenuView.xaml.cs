using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoleWatchApp.ViewModels;
using SlideOverKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MoleWatchApp.Views
{
    public partial class SlideUpMenuView : SlideMenuView
    {
        private CreateCollectionView collection;
        private bool hidePicker;

        public bool HidePicker
        {
            get
            {
                return hidePicker;
            }
            set
            {
                hidePicker = value;
                this.OnPropertyChanged();
            }
        }

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

        private async void RenameButton_OnClicked(object sender, EventArgs e)
        {
            collection.RenameButtonClicked();
        }

        private async void NotificationButton_OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NotificationView)}");
        }

        private void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            
            collection.DeleteButtonClicked();
        }

    }
}

