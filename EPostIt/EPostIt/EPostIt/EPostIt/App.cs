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
            LoadDatabase();
            MainPage = new NavigationPage(new MainPage());
        }
        public static void AddQuickNote(Note n)
        {
            QuickNoteDB holder = new QuickNoteDB();
            holder.content = n.NoteContent;
            holder.DateCreated = n.dateCreated;
            mainDatabase.Insert(holder);
        }
        public static void EditQuickNote(Note n)
        {
            Debug.WriteLine("1");
            AddQuickNote(n);
            Debug.WriteLine("2");
            mainDatabase.Delete<QuickNoteDB>(n.Id);
            Debug.WriteLine("3");
            n.Id = nextID;
            Debug.WriteLine("4");
            nextID++;
            Debug.WriteLine("5");
        }
        void LoadDatabase()
        {
            if (mainDatabase.Table<QuickNoteDB>().Count() == 0)
                return;
            else
            {
                var table = mainDatabase.Table<QuickNoteDB>();
                nextID = mainDatabase.Table<QuickNoteDB>().Last().Id+1;
                foreach (var i in table)
                {
                    NoteManager.quickNotes.Add(new Note(i.content,i.DateCreated,i.Id));
                }
            }
            //mainDatabase.Query<LandmarkDB>("DELETE FROM [Landmark]");
            if (mainDatabase.Table<LandmarkDB>().Count() == 0)
                return;
            else
            {
                var table = mainDatabase.Table<LandmarkDB>();
                foreach (var i in table)
                {
                    LandmarkCollection.CreateLandmark(i.name, i.latitude, i.longitude, i.assignedTime, i.assignedEvents);
                }
            }
        }
        protected override void OnStart()
        {
            if (LandmarkCollection.landmarks==null)
            {
                LandmarkCollection.CreateLandmark("None", 0, 0);
            }
            //Later will need to extract from DB
            AppController.LocationNotification = true;
            AppController.TimeNotification = true;
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
