using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.ViewModels
{
    class NotificationViewModel : BaseViewModel
    {
        private DateTime dateToday;
        private DateTime pickedDate;
        private int pickedIndex;

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

      
        public NotificationViewModel()
        {
            DateToday = DateTime.Now;
        }
    }
}
