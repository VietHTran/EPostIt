using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPostIt
{
    public class ManagerLocation
    {
        public static double latitude=0.000000;
        public static double longitude=0.000000;
        private const double metersPerLat = 111131.75;
        private const double metersPerLong = 78846.81;
        public static double CalcDistance(double lat, double lon)
        {
            double d = Math.Sqrt(Math.Pow((lat - latitude) * metersPerLat, 2) + Math.Pow((lon - longitude) * metersPerLong, 2));
            return d;
        }
    }
}
