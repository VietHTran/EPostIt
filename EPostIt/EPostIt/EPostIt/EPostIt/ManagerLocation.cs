using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;


namespace EPostIt
{
    public class ManagerLocation
    {
        private static double latitude;
        public static double Latitude {
            get { return latitude; }
            set { latitude = value; }
        }
        private static double longitude;
        public static double Longitude {
            get { return longitude; }
            set {
                longitude = value;
                if (AppController.LocationNotification)
                    Task.Run(() => {
                        for (int i = 0; i < NoteManager.locationNotes.Count; i++)
                        {
                            if (!NoteManager.locationNotes[i].isNotified && 
                            !NoteManager.locationNotes[i].isFirst && 
                            CalcDistance(NoteManager.locationNotes[i].latitude, NoteManager.locationNotes[i].longitude) <= NoteManager.locationNotes[i].maxDistance)
                            {
                                NoteManager.locationNotes[i].isNotified = true;
                                ILocationNotification holder = DependencyService.Get<ILocationNotification>();
                                //remember to get ID
                                holder.Remind(NoteManager.locationNotes[i].landmark.name, NoteManager.locationNotes[i].NoteContent, 0);
                            }
                            else if (NoteManager.locationNotes[i].isFirst &&
                            !NoteManager.locationNotes[i].isNotified &&
                            CalcDistance(NoteManager.locationNotes[i].latitude, NoteManager.locationNotes[i].longitude) > NoteManager.locationNotes[i].maxDistance)
                            {
                                NoteManager.locationNotes[i].isFirst = false;
                            }
                        }
                    });
            }
        }
        private const double metersPerLat = 111131.75;
        private const double metersPerLong = 78846.81;
        public static double CalcDistance(double lat, double lon)
        {
            double d = Math.Sqrt(Math.Pow((lat - latitude) * metersPerLat, 2) + Math.Pow((lon - longitude) * metersPerLong, 2));
            return d;
        }
    }
}
