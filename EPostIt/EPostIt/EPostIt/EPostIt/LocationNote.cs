using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    public class LocationNote : Note
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Landmark landmark { get; set; }
        public double maxDistance { get; set; }
        public bool isTriggered { get; set; }
        public bool isNotified { get; set; }
        public LocationNote (string s, double lat, double lon, double md) : base (s,true)
        {
            this.latitude = lat;
            this.longitude = lon;
            this.isTriggered = false;
            this.maxDistance = md;
            this.isNotified = false;
        }
        public LocationNote(string s, Landmark l, double md) : base(s,true)
        {
            this.isTriggered = false;
            this.maxDistance = md;
            this.landmark = l;
            this.latitude = l.latitude;
            this.longitude = l.longitude;
            this.isNotified = false;
        }
        public bool IsPlace()
        {
            if (!isTriggered && ManagerLocation.CalcDistance(latitude,longitude)<=maxDistance)
            {
                return true;
            }
            return false;
        }
    }
}
