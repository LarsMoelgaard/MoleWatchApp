using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;

namespace MoleWatchApp.Models
{
    public class LoginSingleton
    {
        private static ILogin _login;
        private static readonly object threadSafetyLock = new object();

        private static ILogin loginModel
        {
            get
            {
                lock (threadSafetyLock)
                {
                    if (_login == null)
                    {
                        _login = new LoginModel();
                    }

                    return _login;
                }
            }
        }

        public static ILogin GetLoginModel()
        {
            return loginModel;
        }
    }
}
