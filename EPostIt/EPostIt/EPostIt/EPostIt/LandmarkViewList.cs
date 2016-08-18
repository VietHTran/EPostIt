using System;
using System.Collections.Generic;
using System.Linq;

namespace EPostIt
{
    class LandmarkViewList : List<LandmarkView>
    {
        public LandmarkViewList()
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
        public void MassReload()
        {
            for (int i = 1; i < this.Count; i++)
            {
                this[i].ReCalcDistance();
            }
        }
        public List<LandmarkView> NameSort(int code)
        {
            SortedDictionary<string, LandmarkView> sorted = new SortedDictionary<string, LandmarkView>();
            for (int i=1;i<Count;i++)
            {
                sorted.Add(this[i].landmark.name, this[i]);
            }
            if (code==0)
                return sorted.Values.ToList();
            List<LandmarkView> holder = sorted.Values.ToList();
            holder.Reverse();
            return holder;
        }
        public List<LandmarkView> DateSort(int code)
        {
            SortedDictionary<DateTime, LandmarkView> sorted = new SortedDictionary<DateTime, LandmarkView>();
            for (int i = 1; i < Count; i++)
            {
                sorted.Add(this[i].landmark.assignedTime, this[i]);
            }
            if (code == 0)
                return sorted.Values.ToList();
            List<LandmarkView> holder = sorted.Values.ToList();
            holder.Reverse();
            return holder;
        }
        public List<LandmarkView> NoteSetSort(int code)
        {
            List<LandmarkView> holder = MergeSortNoteSet(this,1,Count-1);
            if (code==0)
                return holder;
            else
            {
                holder.Reverse();
                return holder;
            }
        }
        public List<LandmarkView> DistanceSort(int code)
        {
            List<LandmarkView> holder = MergeSortDistance(this, 1, Count - 1);
            if (code == 0)
                return holder;
            else
            {
                holder.Reverse();
                return holder;
            }
        }
        List<LandmarkView> MergeSortNoteSet(List<LandmarkView> h, int l, int r)
        {
            var sorter = (from i in this orderby i.landmark.assignedEvents select i).ToList();
            return sorter;
        }
        List<LandmarkView> MergeSortDistance(List<LandmarkView> h, int l, int r)
        {
            var sorter = (from i in this orderby i.distance select i).ToList();
            return sorter;
        }
    }
}
