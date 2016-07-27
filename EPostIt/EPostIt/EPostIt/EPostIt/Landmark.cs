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
            this.assignedEvents = 0;
            if (!n.Equals("None"))
                AddLandmarkToDB();
        }
        public Landmark (string n, double lat, double lon, DateTime dC, int aE)
        {
            name = n;
            latitude = lat;
            longitude = lon;
            assignedTime = dC;
            assignedEvents = aE;
        }
        public void AssignEvent()
        {
            this.assignedEvents++;
            App.mainDatabase.Query<LandmarkDB>($"UPDATE [Landmark] SET [AssignedEvents]={assignedEvents} WHERE [Name]='{name}'");
        }
        public void UnassignEvent()
        {
            this.assignedEvents--;
            App.mainDatabase.Query<LandmarkDB>($"UPDATE [Landmark] SET [AssignedEvents]={assignedEvents} WHERE [Name]='{name}'");
        }
        public void AddLandmarkToDB()
        {
            LandmarkDB holder = new LandmarkDB();
            holder.name = name;
            holder.latitude = latitude;
            holder.longitude = longitude;
            holder.assignedTime = assignedTime;
            holder.assignedEvents = assignedEvents;
            App.mainDatabase.Insert(holder);
        }
    }
}
