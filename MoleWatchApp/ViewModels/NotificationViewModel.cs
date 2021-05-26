using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataClasses.DTO;
using MoleWatchApp.Extensions;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    class NotificationViewModel : BaseViewModel
    {
        /// <summary>
        /// Refernce til den pågældende collection som er på siden.
        /// </summary>
        public CollectionDTO currentCollection;
        /// <summary>
        /// Navnet på den pågældende collection på siden.
        /// </summary>
        public string CurrentCollectionName;


        /// <summary>
        /// Default constructor til viewmodellen
        /// </summary>
        public NotificationViewModel()
        {
            DateToday = DateTime.Now;

            NotificationModel = new NotificationModel();
            currentCollection = NotificationModel.GetCurrentCollection();
            CurrentCollectionName = currentCollection.CollectionName;

        }

        #region Properties, commands mm. 
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private DateTime dateToday;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private DateTime pickedDate = DateTime.Now;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private int pickedIndex;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private NotificationModel NotificationModel;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private bool newDatePicked;

        /// <summary>
        /// Tjekker om hvorvidt der er valgt en ny dato
        /// </summary>
        public bool NewDatePicked
        {
            get
            {
                return newDatePicked;
            }
            set
            {
                newDatePicked = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Kommando der eksekveres når der trykkes på gem-knappen.
        /// </summary>
        public ICommand SaveClickedCommand
        {
            get
            {
                return new Command<int>((x)=>SaveNotification(x));
            }
        }

        /// <summary>
        /// Index på hjulet som notifikationen sættes til.
        /// </summary>
        public int PickedIndex
        {
            get
            {
                return pickedIndex;
            }
            set
            {
                pickedIndex = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Datoen som bliver valgt i Viewet
        /// </summary>
        public DateTime PickedDate
        {
            get
            {
                return pickedDate;
            }
            set
            {
                pickedDate = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sættes så kalenderen ved at notfikationen skal startes fra dags dato.
        /// </summary>
        public DateTime DateToday
        {
            get
            {
                return dateToday;
            }
            set
            {
                dateToday = value;
                this.OnPropertyChanged();
            }
        }


        #endregion


        /// <summary>
        /// Metoden gemmer ændringen i notifikationen og sender dette videre til Notification Model så det kan gemmes på API'en 
        /// </summary>
        /// <param name="interval"></param>
        public void SaveNotification(int interval)
        {
            NotificationData notificationData = new NotificationData();
            notificationData.nextNotification = PickedDate;
            notificationData.ReminderIntervalInWeeks = interval;

            NotificationModel.UpdateNotification(notificationData);
        }
    }
}
