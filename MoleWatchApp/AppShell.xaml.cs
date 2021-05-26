using MoleWatchApp.ViewModels;
using MoleWatchApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoleWatchApp
{
    /// <summary>
    /// Default klasse i Xamarin.Forms
    /// </summary>
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateCollectionView), typeof(CreateCollectionView));
            Routing.RegisterRoute(nameof(PictureListView), typeof(PictureListView));
            Routing.RegisterRoute(nameof(CollectionListView), typeof(CollectionListView));
            Routing.RegisterRoute(nameof(NotificationView), typeof(NotificationView));
            Routing.RegisterRoute(nameof(FullPictureView), typeof(FullPictureView));
            Routing.RegisterRoute(nameof(ContactDoctorView),typeof(ContactDoctorView));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
