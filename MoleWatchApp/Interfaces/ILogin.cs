using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO.MISCDTOS;

namespace MoleWatchApp.Interfaces
{
    public interface ILogin
    {
        PatientDataDTO PatientData { get; }

        bool VerifyPassword(string Username, string Password);

        bool VerifySmartLoginPassword(); 
    }
}
