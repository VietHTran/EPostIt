using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Locations;

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
                Accuracy = Accuracy.Fine,
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
            /*
            if (locationManager != null)
            {
                locationManager.RemoveUpdates(this);
            }
            */
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (locationManager != null)
            {
                locationManager.RequestLocationUpdates(provider, 0, 0, this);
            }
        }
        public void OnLocationChanged(Android.Locations.Location location)
        {
            currentLocation = location;
            if (currentLocation != null)
            {
                if (currentLocation.Latitude == 0 && currentLocation.Longitude == 0)
                {
                    return;
                }
                ManagerLocation.Latitude = currentLocation.Latitude;
                ManagerLocation.Longitude = currentLocation.Longitude;
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

