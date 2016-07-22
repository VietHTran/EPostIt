using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using EPostIt;
//using Acr.UserDialogs;

namespace EPostIt.Droid
{

    [Activity(Label = "EPostIt", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILocationListener
    {
        LocationManager locationManager;
        Location currentLocation;
        string provider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            //UserDialogs.Init(this);
            LoadApplication(new App());
            InitLocationManager();
        }
        void InitLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteria = new Criteria
            {
                Accuracy = Accuracy.High,
            };
            IList<String> providers = locationManager.GetProviders(criteria, true);
            if (providers.Any())
            {
                provider = providers.First();
            }
            else
            {
                provider = string.Empty;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (locationManager != null)
            {
                locationManager.RemoveUpdates(this);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (locationManager != null)
            {
                locationManager.RequestLocationUpdates(provider, 0, 0, this);
            }

        }
        /*
        public void OnLocationChanged(Location location)
        {

            currentLocation = location;
            if (currentLocation!=null)
            {
                ManagerLocation.latitude = currentLocation.Latitude;
                ManagerLocation.longitude = currentLocation.Longitude;
                //Check for nearby events
            }
        }
        */
        public void OnLocationChanged(Android.Locations.Location location)
        {
            currentLocation = location;
            if (currentLocation != null)
            {
                if (currentLocation.Latitude == 0 && currentLocation.Longitude == 0)
                {
                    return;
                }
                ManagerLocation.latitude = currentLocation.Latitude;
                ManagerLocation.longitude = currentLocation.Longitude;
                //Check for nearby events
            }
        }
        public void OnProviderDisabled(string p)
        {

        }
        public void OnProviderEnabled(string p)
        {

        }
        public void OnStatusChanged(string p, Availability status, Bundle extras)
        {

        }
        public void exitApp()
        {
            System.Environment.Exit(0);
        }
    }
}

