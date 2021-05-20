using MoleWatchApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Plugin.Media.CrossMedia.Current.Initialize();

            MainPage = new AppShell();
        }


        protected override void OnStart()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
