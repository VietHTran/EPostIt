using Android.App;
using Android.Content;
namespace EPostIt.Droid
{
    [BroadcastReceiver]
    class LocationReceiver_Android : BroadcastReceiver
    {
        int notificationId;
        int pendingIntentId;
        public override void OnReceive(Context context, Intent intent)
        {
            notificationId = intent.GetIntExtra("ID", 0);
            pendingIntentId = notificationId;
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
        }
    }
}