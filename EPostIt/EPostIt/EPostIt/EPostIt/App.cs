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
        [Column("Name")]
        public string name { get; set; }
        [Column("Latitude")]
        public double latitude { get; set; }
        [Column("Longitude")]
        public double longitude { get; set; }
        [Column("AssignedTime")]
        public double assignedTime { get; set; }
        [Column("AssignedEvents")]
        public double assignedEvents { get; set; }
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
            AddQuickNote(n);
            mainDatabase.Delete<QuickNoteDB>(n.Id);
            n.Id = nextID;
            nextID++;
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
        }
        protected override void OnStart()
        {
            if (LandmarkCollection.landmarks==null)
            {
                LandmarkCollection.CreateLandmark("None", 0, 0);
            }
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
