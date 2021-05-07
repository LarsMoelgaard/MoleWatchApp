using MoleWatchApp.ViewModels;
using MoleWatchApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoleWatchApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateCollectionView), typeof(CreateCollectionView));
            Routing.RegisterRoute(nameof(PictureListView), typeof(PictureListView));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
