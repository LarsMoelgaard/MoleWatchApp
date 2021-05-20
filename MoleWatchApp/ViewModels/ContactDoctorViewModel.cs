using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.ViewModels
{
    public class ContactDoctorViewModel : BaseViewModel
    {
        private string doctorName;
        private string doctorAdress;

        public string DoctorName
        {
            get
            {
                return doctorName;
            }
            set
            {
                doctorName = value;
                this.OnPropertyChanged();
            }
        }

        public string DoctorAdress
        {
            get
            {
                return doctorAdress;
            }
            set
            {
                doctorAdress = value;
                this.OnPropertyChanged();
            }
        }
    }
}
