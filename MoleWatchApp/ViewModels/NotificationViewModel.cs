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

            SaveClickedCommand = new Command(SaveNotification);
            NotificationModel = new NotificationModel();
            CurrentCollectionName = NotificationModel.GetName();

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

        public Command SaveClickedCommand { get; }

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



        public void SaveNotification()
        {
            NotificationData notificationData = new NotificationData();
            notificationData.nextNotification = PickedDate;
            notificationData.ReminderIntervalInWeeks = CalculateIntervalInWeeks(PickedIndex);

            NotificationModel.UpdateNotification(notificationData);
        }

        
        //TODO skal slettes enten her eller i view'et 
        private int CalculateIntervalInWeeks(int index)
        {
            switch (index)
            {
                case 1:
                    return 0;

                case 2:
                    return 1;

                case 3:
                    return 2;

                case 4:
                    return 4;

                case 5:
                    return 12;

                case 6:
                    return 24;

                case 7:
                    return 52;
            }

            return 0;
        }
    }
}
