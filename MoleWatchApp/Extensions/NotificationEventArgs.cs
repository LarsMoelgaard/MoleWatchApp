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
        /// <summary>
        /// Titel på notifikation
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Beskeden som bliver sendt med notifikationen
        /// </summary>
        public string Message { get; set; }
    }
}
