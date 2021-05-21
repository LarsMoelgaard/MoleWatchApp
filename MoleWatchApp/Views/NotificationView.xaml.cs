using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleWatchApp.Extensions;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    /// <summary>
    /// Opretter view til at vise notifikationsindstillinger 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationView : ContentPage
    {
        private INotificationManager notificationManager;
        private string MoleName;
        private int CollectionID;
        private NotificationViewModel vm;

        public NotificationView()
        {
            InitializeComponent();
            vm = new NotificationViewModel();
            BindingContext = vm;
            MoleName = vm.CurrentCollectionName;
            CollectionID = vm.currentCollection.CollectionID;

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }


        /// <summary>
        ///  Binding til NotifikationPicker hvor brugeren kan vælge varrighed imellem notifikationerne 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotificationPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            vm.PickedIndex = picker.SelectedIndex;
        }


        /// <summary>
        /// Binding til DatePicker hvor brugeren kan vælge dato for næste notifikation 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            var datepicker = (DatePicker) sender;
            vm.PickedDate = datepicker.Date;
            vm.NewDatePicked = true;
        }


        /// <summary>
        /// Gemmer notifikationen med den valgte dato og varighed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveNotificationButton_OnClicked(object sender, EventArgs e)
        {

            vm.SaveClickedCommand.Execute(null); //TODO forbindelse til API så notifikation kan ændres - Dette er ikke implementeret - måske det heller ikke skal? 

            //Bemærk: for testens skyld laves dette i sekunder og ikke i dage, uger og måneder

            string title = $"Moletracker";
            string message = $"Dit modermærke {MoleName} skal opdateres!";
            int IntervalInWeeks = CalculateIntervalInWeeks(vm.PickedIndex);
            int CollectionID = vm.currentCollection.CollectionID;

            notificationManager.SendNotification(title, message,CollectionID ,IntervalInWeeks, vm.PickedDate);
            await Shell.Current.GoToAsync("..");

        }


        /// <summary>
        /// Metoden beregner antallet af uger ud fra det valgte index på NotifikationPicker 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int CalculateIntervalInWeeks(int index)
        {
            switch (index)
            {
                case 0:
                    return 0;

                case 1:
                    return 1;

                case 2:
                    return 2;

                case 3:
                    return 4;

                case 4:
                    return 12;

                case 5:
                    return 24;

                case 6:
                    return 52;
            }

            return 0;
        }


        /// <summary>
        /// Metoden viser notifikationen på skærmen 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"MoleWatch \nTitle: {title}\nMessage: {message}"
                };
                NotificationStackLayout.Children.Add(msg);
            });
        }



        /// <summary>
        /// Metoden fjerner notifikationen fra samlingen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteNotificationButton_OnClicked(object sender, EventArgs e)
        {
            notificationManager.DeleteNotification(CollectionID);
            await Shell.Current.GoToAsync("..");
        }
    }
}