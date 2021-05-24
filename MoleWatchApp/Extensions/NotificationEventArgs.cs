using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.Extensions
{
    /// <summary>
    /// Event til at sende notifikationer 
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
