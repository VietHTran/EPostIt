using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Xamarin.Forms;

namespace EPostIt
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            LoadPersistedValues();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Debug.WriteLine("0a");
            LoadPersistedValues();
            if (LandmarkCollection.landmarks==null)
            {
                LandmarkCollection.CreateLandmark("None", 0, 0);
                //LandmarkCollection.CreateLandmark("Home", 40.053142, -75.067209);
                //LandmarkCollection.CreateLandmark("NEHS", 40.055595, -75.071062);
                //LandmarkCollection.CreateLandmark("Random Place", 40.057770, -75.065449);
            }
            NoteManager.Init();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Properties["Landmarks"] = LandmarkCollection.landmarks;
            Properties["LandmarksName"] = LandmarkCollection.nameList;
            Properties["QuickNotes"] =  NoteManager.quickNotes;
        }

        private void LoadPersistedValues()
        {
            if (Properties.ContainsKey("Landmarks"))
                LandmarkCollection.landmarks = (List<Landmark>) Properties["Landmarks"];
            if (Properties.ContainsKey("LandmarksName"))
                LandmarkCollection.nameList = (List<string>)Properties["LandmarksName"];
            if (Properties.ContainsKey("QuickNotes"))
                NoteManager.quickNotes = (List<Note>)Properties["QuickNotes"];

        }
        protected override void OnResume()
        {
            LoadPersistedValues();
            // Handle when your app resumes
        }
    }
}
