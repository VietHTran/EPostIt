using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EPostIt
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (LandmarkCollection.landmarks==null)
            {
                LandmarkCollection.CreateLandmark("None", 0, 0);
                LandmarkCollection.CreateLandmark("Home", 40.053142, -75.067209);
                LandmarkCollection.CreateLandmark("NEHS", 40.055595, -75.071062);
                LandmarkCollection.CreateLandmark("Random Place", 40.057770, -75.065449);
            }
            NoteManager.Init();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
