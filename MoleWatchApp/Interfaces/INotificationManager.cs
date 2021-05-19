using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message,int id, int interval, DateTime notifyTime = default(DateTime));
        void DeleteNotification(int id);

    }
}
