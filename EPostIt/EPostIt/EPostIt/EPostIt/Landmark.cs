using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostIt
{
    public class Landmark
    {
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime assignedTime { get; set; }
        public int assignedEvents { get; set; }
        public Landmark (string n, double lat, double lon)
        {
            this.name = n;
            this.latitude = lat;
            this.longitude = lon;
            this.assignedTime = DateTime.Now;
        }
        public void AssignEvent()
        {
            this.assignedEvents++;
        }
        public void UnassignEvent()
        {
            this.assignedEvents--;
        }
    }
}
