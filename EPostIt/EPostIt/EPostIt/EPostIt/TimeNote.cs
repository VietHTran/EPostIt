using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace EPostIt
{
    public class TimeNote : Note
    {
        public DateTime DateTimeSet { get; set; }
        public INoteTimer Alarm { get; set; }
        //Only load once. To reload-> Change time
        private bool isTriggered;
        public bool IsTriggered
        {
            get
            {
                return isTriggered;
            }
            set {
                isTriggered = value;
                int val = value ? 1 : 0;
                App.mainDatabase.Query<TimeNoteDB>($"UPDATE [TN] SET [IsTriggered]={val} WHERE [_idt]={Id}");
            }
        } //Used to check if there is already an alarm set for it
        public static int NextID { get; set; }
        public TimeNote(string s, DateTime setter) : base (s,true)
        {
            this.DateTimeSet = setter;
            Id = NextID;
            NextID++;
            AddToDB();
            if (AppController.TimeNotification)
            {
                this.IsTriggered = true;
                SetAlarm();
            }
            else
                this.IsTriggered = false;
        }
        public TimeNote (string s, DateTime setter, DateTime dateC, int id, bool isTrig) : base(s,dateC,id)
        {
            DateTimeSet = setter;
            IsTriggered = isTrig;
            Alarm = DependencyService.Get<INoteTimer>();
        }
        private bool isUpdate = false;
        public bool IsTime()
        {
            if (DateTimeSet.CompareTo(DateTime.Now)<=0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public void SetAlarm()
        {
            Alarm = DependencyService.Get<INoteTimer>();
            Alarm.Remind(DateTimeSet, "E-Post-it Reminder", NoteContent,Id);
        }
        public static bool compareDateTime(DateTime d)
        {
            if (d.CompareTo(DateTime.Now)>0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public void AddToDB()
        {
            TimeNoteDB holder = new TimeNoteDB();
            holder.content = NoteContent;
            holder.DateCreated = dateCreated;
            holder.DateTriggered = DateTimeSet;
            holder.IsTriggered = IsTriggered;
            App.mainDatabase.Insert(holder);
            if (isUpdate)
            {
                App.mainDatabase.Delete<TimeNoteDB>(Id);
                Debug.WriteLine($"Delete {NoteContent}, ID: {Id}");
            }
            if (Id != App.mainDatabase.Table<TimeNoteDB>().Last().Idt)
            {
                Id = App.mainDatabase.Table<TimeNoteDB>().Last().Idt;
                NextID = Id + 1;
            }
        }
        public void UpdateDB()
        {
            isUpdate = true;
            AddToDB();
            isUpdate = false;
            if (isTriggered)
                SetAlarm();
        }
    }
}
