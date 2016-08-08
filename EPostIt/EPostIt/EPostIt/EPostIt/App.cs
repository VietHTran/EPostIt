using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SQLite;
using Xamarin.Forms;

namespace EPostIt
{
    [Table("QN")]
    public class QuickNoteDB
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("Content")]
        public string content { get; set; }
        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }
    }
    [Table("TN")]
    public class TimeNoteDB
    {
        [PrimaryKey, AutoIncrement, Column("_idt")]
        public int Idt { get; set; }
        [Column("ContentT")]
        public string content { get; set; }
        [Column("DateCreatedT")]
        public DateTime DateCreated { get; set; }
        [Column("DateTriggered")]
        public DateTime DateTriggered { get; set; }
        [Column("IsTriggered")]
        public bool IsTriggered { get; set; }
    }
    [Table("Landmark")]
    public class LandmarkDB
    {
        [PrimaryKey,Column("Name")]
        public string name { get; set; }
        [Column("Latitude")]
        public double latitude { get; set; }
        [Column("Longitude")]
        public double longitude { get; set; }
        [Column("AssignedTime")]
        public DateTime assignedTime { get; set; }
        [Column("AssignedEvents")]
        public int assignedEvents { get; set; }
    }
    [Table("Trivia")]
    public class ExtraInformationDB
    {
        public bool TimeNotification { get; set; }
        public bool LocationNotification { get; set; }
        //public int pendingID { get; set; } use for Android Alarm
    }
    public class App : Application
    {
        public static SQLiteConnection mainDatabase;
        public static int nextID;
        public App()
        {
            NoteManager.Init();
            mainDatabase = DependencyService.Get<ISQLite>().GetConnection();
            mainDatabase.CreateTable<QuickNoteDB>();
            mainDatabase.CreateTable<LandmarkDB>();
            mainDatabase.CreateTable<TimeNoteDB>();
            mainDatabase.CreateTable<ExtraInformationDB>();
            LandmarkCollection.CreateLandmark("None", 0, 0);
            LoadDatabase();
            Debug.WriteLine($"update latest3");
            MainPage = new NavigationPage(new MainPage());
        }
        public static void AddQuickNote(Note n)
        {
            QuickNoteDB holder = new QuickNoteDB();
            holder.content = n.NoteContent;
            holder.DateCreated = n.dateCreated;
            mainDatabase.Insert(holder);
            if (n.Id!=mainDatabase.Table<QuickNoteDB>().Last().Id)
            {
                n.Id = mainDatabase.Table<QuickNoteDB>().Last().Id;
                nextID = n.Id + 1;
            }
        }
        public static void EditQuickNote(Note n)
        {
            AddQuickNote(n);
            mainDatabase.Delete<QuickNoteDB>(n.Id);
            n.Id = nextID;
            nextID++;
        }
        void LoadDatabase()
        {
            if (mainDatabase.Table<ExtraInformationDB>().Count() !=0)
            {
                var holder = mainDatabase.Table<ExtraInformationDB>().First();
                AppController.TimeNotification = holder.TimeNotification;
                AppController.LocationNotification = holder.LocationNotification;
            } else
            {
                ExtraInformationDB holder = new ExtraInformationDB();
                holder.TimeNotification = true;
                holder.LocationNotification = true;
                mainDatabase.Insert(holder);
                AppController.TimeNotification = true;
                AppController.LocationNotification = true;
            }
            if (mainDatabase.Table<QuickNoteDB>().Count() != 0)
            {
                var table = mainDatabase.Table<QuickNoteDB>();
                nextID = mainDatabase.Table<QuickNoteDB>().Last().Id+1;
                foreach (var i in table)
                    NoteManager.quickNotes.Add(new Note(i.content, i.DateCreated, i.Id));
            }
            if (mainDatabase.Table<TimeNoteDB>().Count() != 0)
            {
                var table = mainDatabase.Table<TimeNoteDB>();
                TimeNote.NextID = mainDatabase.Table<TimeNoteDB>().Last().Idt + 1;
                foreach (var i in table)
                    NoteManager.timeNotes.Add(new TimeNote(i.content, i.DateTriggered, i.DateCreated, i.Idt,i.IsTriggered));
            }
            //mainDatabase.Query<LandmarkDB>("DELETE FROM [Landmark]");
            if (mainDatabase.Table<LandmarkDB>().Count() != 0)
            {
                var table = mainDatabase.Table<LandmarkDB>();
                foreach (var i in table)
                    LandmarkCollection.CreateLandmark(i.name, i.latitude, i.longitude, i.assignedTime, i.assignedEvents);
            }
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
