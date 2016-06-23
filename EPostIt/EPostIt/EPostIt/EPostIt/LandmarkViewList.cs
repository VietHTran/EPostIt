using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class LandmarkViewList:List<LandmarkView>
    {
        public LandmarkViewList ()
        {
            foreach (var i in LandmarkCollection.landmarks)
            {
                Add(new LandmarkView(i));
            }
        }
        public void DeleteLandmark(LandmarkView l)
        {
            l.DeleteFromMainData();
            Remove(l);
        }
    }
}
