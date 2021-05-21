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
        public CollectionDTO currentCollection;
        public string CurrentCollectionName;

        public NotificationViewModel()
        {
            DateToday = DateTime.Now;

            NotificationModel = new NotificationModel();
            currentCollection = NotificationModel.GetCurrentCollection();
            CurrentCollectionName = currentCollection.CollectionName;

        }

        #region Properties, commands mm. 

        private DateTime dateToday;
        private DateTime pickedDate = DateTime.Now;
        private int pickedIndex;
        private NotificationModel NotificationModel;
        private bool newDatePicked;

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

        public ICommand SaveClickedCommand
        {
            get
            {
                return new Command<int>((x)=>SaveNotification(x));
            }
        }

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
