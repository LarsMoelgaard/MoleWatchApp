using System;
using System.Collections.Generic;
using System.Text;

using DataClasses.DTO;

namespace MoleWatchApp.Models
{
    public class LoginModel
    {
        private APICommunication APIConnection;

        public LoginModel()
        {
            APIConnection = new APICommunication();

        }

        public bool VerifyPassword(string Username, string Password)
        {
            // TODO indskriv kommunikationen med API-klassen her.


            bool LoginVerified = APIConnection.VerifyPasswordWithAPI(Username, Password);

            return LoginVerified;

        }
    }


}
