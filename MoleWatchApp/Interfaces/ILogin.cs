using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;

namespace MoleWatchApp.Interfaces
{
    public interface ILogin
    {
        PatientDataDTO PatientData { get; }
        bool IsPatientLoadedFromAPI { get; set; }

        bool VerifyPassword(string Username, string Password);

        bool VerifySmartLoginPassword(); 
    }
}
