using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class LocationNote : Note
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double maxDistance { get; set; }
        public bool isTriggered { get; }
        public LocationNote (string s, double lat, double lon, double md) : base (s)
        {
            this.latitude = lat;
            this.longitude = lon;
            this.isTriggered = false;
            this.maxDistance = md;
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
