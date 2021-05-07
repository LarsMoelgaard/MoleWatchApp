using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.ViewModels
{
    class NotificationViewModel : BaseViewModel
    {
        private List<string> numberList;

        public NotificationViewModel()
        {
            NumberList = new List<string>();
            NumberList.Add("1");
            NumberList.Add("2");
            NumberList.Add("3");
            NumberList.Add("4");
        }

        public List<string> NumberList
        {
            get
            {
                return numberList;
            }
            set
            {
                numberList = value;
                this.OnPropertyChanged();
            }
        }


    }
}
