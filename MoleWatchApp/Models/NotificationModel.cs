using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using MoleWatchApp.Extensions.DTO;

namespace MoleWatchApp.Models
{
    class NotificationModel
    {
        public IAPIService api { get; }

        public NotificationModel()
        {
            api = APISingleton.GetAPI();
        }

        public void UpdateNotification(NotificationData data)
        {
           
        }
    }
}
