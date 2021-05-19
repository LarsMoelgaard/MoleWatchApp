using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Renderscripts;
using AndroidX.Core.App;
using MoleWatchApp.Droid;
using MoleWatchApp.Extensions;
using MoleWatchApp.Interfaces;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;


[assembly: Dependency(typeof(AndroidNotificationManager))]
namespace MoleWatchApp.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        private Intent intent;
        private AlarmManager alarmManager;
        private PendingIntent pendingIntent;

        bool channelInitialized = false;
        int messageId = 0;
        int pendingIntentId = 0;


        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AndroidNotificationManager Instance { get; private set; }

        public AndroidNotificationManager() => Initialize();
        

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
                alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            }
        }

        public void SendNotification(string title, string message, int id, int interval ,DateTime notifyTime = default(DateTime))
        {
            intent = new Intent(AndroidApp.Context, typeof(AlarmHandler)).SetAction("LocalNotifierIntent" + id);
            pendingIntentId = id;

            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            intent.PutExtra(TitleKey, title); 
            intent.PutExtra(MessageKey, message);

            long triggerTime = GetNotifyTime(notifyTime);
            pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.CancelCurrent);

            if (interval == 0)
            { 
                alarmManager.Set(AlarmType.RtcWakeup, 1000 + triggerTime * 1000,  pendingIntent);
            }
            else
            {
                alarmManager.SetRepeating(AlarmType.RtcWakeup, 1000 + triggerTime * 1000, interval * 1000, pendingIntent);
            }
        }

        public void DeleteNotification(int id)
        {
            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, id, intent, PendingIntentFlags.UpdateCurrent);
            alarmManager.Cancel(pendingIntent);
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void Show(string title, string message)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.GalleryIcon))
                .SetSmallIcon(Resource.Drawable.GalleryIcon)
                .SetPriority(2)
                .SetVibrate(new long[0])
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId++, notification);
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        long GetNotifyTime(DateTime notifyTime)
        {
            int daysToNotifikation = Convert.ToInt16((notifyTime.Date - DateTime.Today).TotalDays);
            int weeksToNotifikation = daysToNotifikation;
            return weeksToNotifikation;
        }
    }
}