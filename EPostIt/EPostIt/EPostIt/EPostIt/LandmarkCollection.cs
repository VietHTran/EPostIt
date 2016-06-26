using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EPostIt
{
    public class LandmarkCollection
    {
        public static List<Landmark> landmarks { get; set; }
        public static List<String> nameList { get; set; }
        public static void ClearLandmark(string name)
        {
            foreach (var i in landmarks)
            {
                if (i.name.Equals(name))
                {
                    landmarks.Remove(i);
                    break;
                }
            }
        }
        public static void CreateLandmark(string n, double lat, double lon)
        {
            if (landmarks==null)
            {
                landmarks = new List<Landmark>();
                nameList = new List<string>();
            }
            landmarks.Add(new Landmark(n, lat, lon));
            nameList.Add(n);
        }
        public static Landmark SearchName(string n)
        {
            int i=nameList.IndexOf(n);
            if (i==-1)
            {
                return landmarks[0];
            } else
            {
                return landmarks[i];
            }
            
        }
        public static void ResetList()
        {
            landmarks.Clear();
            nameList.Clear();
            landmarks.Add(new Landmark("None", 0, 0));
        }
    }
}
