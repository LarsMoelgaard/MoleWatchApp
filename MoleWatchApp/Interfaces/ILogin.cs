using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.Interfaces
{
    public interface ILogin
    {
        bool VerifyPassword(string Username, string Password);

        bool VerifySmartLoginPassword(); 
    }
}
