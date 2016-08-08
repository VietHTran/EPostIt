using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

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
        public bool isFirst { get; set; } //In case the landmark have just been created
        public LocationNote(string s, Landmark l, double md, bool isFirst ) : base(s,true)
        {
            this.isTriggered = false;
            this.maxDistance = md;
            this.landmark = l;
            this.latitude = l.latitude;
            this.longitude = l.longitude;
            this.isNotified = false;
            this.isFirst = isFirst;
            Debug.WriteLine($"Is First new: {isFirst}");
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
