using System;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using EPostIt.Droid;


[assembly: Dependency(typeof(NoteTimer_Android))]
namespace EPostIt.Droid
{
    class NoteTimer_Android : INoteTimer
    {
        public void Remind(DateTime dateTime, string title, string message, int id)
        {
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
        public void Cancel(int id)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver_Android));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(pendingIntent);
        }
    }
}