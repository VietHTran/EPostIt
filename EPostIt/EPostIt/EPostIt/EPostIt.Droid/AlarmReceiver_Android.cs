using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EPostIt;

namespace EPostIt.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver_Android : BroadcastReceiver
    {
        static int notificationId;
        static int pendingIntentId;
        public override void OnReceive(Context context, Intent intent)
        {
            notificationId = App.NextPendingID;
            pendingIntentId = App.NextPendingID;
            App.NextPendingID++;
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            Intent secondIntent = new Intent(context, typeof(MainActivity));
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(secondIntent);
            PendingIntent pendingIntent = stackBuilder.GetPendingIntent(pendingIntentId, PendingIntentFlags.OneShot);
            
            NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager; ;
            var style = new Notification.BigTextStyle();
            style.BigText(message);
            var builder = new Notification.Builder(context)
                            .SetContentIntent(pendingIntent)
                            .SetSmallIcon(Resource.Drawable.icon)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetStyle(style)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Alarm))
                            .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                            .SetAutoCancel(true);
            var notification = builder.Build();
            notificationManager.Notify(notificationId, notification);
            notificationId++;
            pendingIntentId++;
        }
    }
}