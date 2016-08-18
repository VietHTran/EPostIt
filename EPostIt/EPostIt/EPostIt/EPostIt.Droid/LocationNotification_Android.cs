using System;

using Android.App;
using Android.Content;
using EPostIt.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationNotification_Android))]

namespace EPostIt.Droid
{
    class LocationNotification_Android : ILocationNotification
    {
        public void Remind(string title, string message, int id)
        {
            DateTime dateTime = DateTime.Now.AddSeconds(1);
            int pendingIntentID = id;
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver_Android));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("ID", pendingIntentID);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, pendingIntentID, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
            DateTime dtBasis = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            alarmManager.Set(AlarmType.RtcWakeup, (long)dateTime.ToUniversalTime().Subtract(dtBasis).TotalMilliseconds, pendingIntent);
        }
    }
}