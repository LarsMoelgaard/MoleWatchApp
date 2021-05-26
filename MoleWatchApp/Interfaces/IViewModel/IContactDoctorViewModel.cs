using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface IContactDoctorViewModel
    {
        string MobileNumber { get; set; }
        string OpeningHours { get; set; }
        string DoctorName { get; set; }
        string DoctorAdress { get; set; }
        ICommand OpenWebCommand { get; }
        ICommand CallNumber { get; }
    }
}
