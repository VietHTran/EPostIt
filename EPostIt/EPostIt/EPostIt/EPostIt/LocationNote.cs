using System;
using System.Linq;

namespace EPostIt
{
    public class LocationNote : Note
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Landmark landmark { get; set; }
        public double maxDistance { get; set; }
        public bool isTriggered { get; set; }
        private bool isNotified;
        public bool IsNotified {
            get { return isNotified; }
            set
            {
                isNotified = value;
                int val = value ? 1 : 0;
                App.mainDatabase.Query<LocationNoteDB>($"UPDATE [LN] SET [IsTriggeredL]={val} WHERE [_idl]={Id}");
            }
        }
        public bool isFirst { get; set; }
        private bool isUpdate = false;
        public LocationNote(string s, Landmark l, double md, bool isFirst ) : base(s,true)
        {
            isTriggered = false;
            maxDistance = md;
            landmark = l;
            latitude = l.latitude;
            longitude = l.longitude;
            isNotified = false;
            this.isFirst = isFirst;
            AddToDB();
        }
        public LocationNote(string s, string l, double md, bool isFirst, DateTime dC, bool isTrig, bool isNoti, int id) : base(s, dC, id)
        {
            landmark = (from i in LandmarkCollection.landmarks where i.name==l select i).ToList().First() as Landmark;
            latitude = landmark.latitude;
            longitude = landmark.longitude;
            maxDistance = md;
            this.isFirst = isFirst;
            isTriggered = isTrig;
            isNotified = isNoti;
        }
        public void AddToDB()
        {
            LocationNoteDB holder = new LocationNoteDB();
            holder.content = NoteContent;
            holder.landmark = landmark.name;
            holder.maxDistance = maxDistance;
            holder.isFirst = isFirst;
            holder.DateCreated = dateCreated;
            holder.IsTriggered = isTriggered;
            holder.isNotified = isNotified;
            App.mainDatabase.Insert(holder);
            if (isUpdate)
                App.mainDatabase.Delete<LocationNoteDB>(Id);
            Id = App.mainDatabase.Table<LocationNoteDB>().Last().Idl;
        }
        public void UpdateDB()
        {
            isUpdate = true;
            AddToDB();
            isUpdate = false;
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
