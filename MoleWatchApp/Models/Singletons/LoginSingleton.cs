using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class LoginSingleton
    {
        private static ILogin _login;

        private static ILogin loginModel
        {
            get
            {
                if (_login == null)
                {
                    _login = new LoginModel(); 
                }

                return _login;
            }
        }

        public static ILogin GetLoginModel()
        {
            return loginModel;
        }
    }
}
