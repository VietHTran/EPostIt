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
using Android.Util;
using Xamarin.Forms;
using EPostIt;
using EPostIt.Droid;


[assembly: Dependency(typeof(NoteTimer_Android))]
namespace EPostIt.Droid
{
    class NoteTimer_Android : INoteTimer
    {
        static int pendingIntentID = 0;
        AlarmManager alarmManager;
        PendingIntent pendingIntent;
        public void Remind(DateTime dateTime, string title, string message)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver_Android));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            pendingIntent = PendingIntent.GetBroadcast(Forms.Context, pendingIntentID, alarmIntent, PendingIntentFlags.UpdateCurrent);
            pendingIntentID++;
            alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
            DateTime dtBasis = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            alarmManager.Set(AlarmType.RtcWakeup, (long)dateTime.ToUniversalTime().Subtract(dtBasis).TotalMilliseconds, pendingIntent);
        }
        public void Cancel()
        {
            alarmManager.Cancel(pendingIntent);
        }
    }
}