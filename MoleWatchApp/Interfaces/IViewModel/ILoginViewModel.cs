using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface ILoginViewModel
    {
        Command LoginCommand { get; }

        Command SmartLoginCommand { get; }

        string UsernameInput { get; set; }
        string Password { get; set; }
        bool BaseIsBusy { get; set; }
        string UsernameLabel { get; set; }
        string PasswordLabel { get; set; }
        Color UsernameLabelColor { get; set; }
        Color PasswordLabelColor { get; set; }
    }
}
