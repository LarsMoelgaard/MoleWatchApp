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

            bool LoginVerified = APIConnection.VerifyPasswordWithAPI(Username, Password);

            return LoginVerified;

        }

        public bool VerifySmartLoginPassword()
        {

            bool LoginVerified = APIConnection.VerifySmartLogin();
            return LoginVerified;

        }
    }


}
