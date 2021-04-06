using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses.DTO
{
    public class ChangeNotificationsDTO
    {
        public int CollectionID { get; set; }
        public TimeSpan NotificationTimeInterval { get; set; }
    }
}
