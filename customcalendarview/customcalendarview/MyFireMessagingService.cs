using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Messaging;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace customcalendarview
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    class MyFireMessagingService: FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            SendNotificatios(message.GetNotification().Body, message.GetNotification().Title);
        }
        public void SendNotificatios(string body, string Header)
        {
            Notification.Builder builder = new Notification.Builder(this);
            builder.SetSmallIcon(Resource.Drawable.icon);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
            builder.SetContentIntent(pendingIntent);
            builder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon));
            builder.SetContentTitle(Header);
            builder.SetContentText(body);
            builder.SetDefaults(NotificationDefaults.Sound);
            builder.SetAutoCancel(true);
            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(1, builder.Build());
        }


    }
}