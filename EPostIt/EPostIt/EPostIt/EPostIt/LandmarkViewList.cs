using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

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
            if (r - l >= 1)
            {
                int mid = (l + r) / 2;
                List<LandmarkView> a = MergeSortNoteSet(h, l, mid);
                List<LandmarkView> b = MergeSortNoteSet(h, mid + 1, r);
                List<LandmarkView> c = new List<LandmarkView>();
                int ai = 0, bi = 0;
                while (ai < a.Count && bi < b.Count)
                {
                    if (a[ai].landmark.assignedEvents > b[bi].landmark.assignedEvents)
                    {
                        c.Add(b[bi]);
                        bi++;
                    }
                    else
                    {
                        c.Add(a[ai]);
                        ai++;
                    }
                }
                if (ai < a.Count)
                {
                    for (int i = ai; i < a.Count; i++)
                    {
                        c.Add(a[i]);
                    }
                }
                else if (bi < b.Count)
                {
                    for (int i = bi; i < b.Count; i++)
                    {
                        c.Add(b[i]);
                    }
                }
                return c;
            }
            List<LandmarkView> re = new List<LandmarkView>();
            re.Add(h[l]);
            return re;
        }
        List<LandmarkView> MergeSortDistance(List<LandmarkView> h, int l, int r)
        {
            if (r - l >= 1)
            {
                int mid = (l + r) / 2;
                List<LandmarkView> a = MergeSortDistance(h, l, mid);
                List<LandmarkView> b = MergeSortDistance(h, mid + 1, r);
                List<LandmarkView> c = new List<LandmarkView>();
                int ai = 0, bi = 0;
                while (ai < a.Count && bi < b.Count)
                {
                    if (a[ai].distance > b[bi].distance)
                    {
                        c.Add(b[bi]);
                        bi++;
                    }
                    else
                    {
                        c.Add(a[ai]);
                        ai++;
                    }
                }
                if (ai < a.Count)
                {
                    for (int i = ai; i < a.Count; i++)
                    {
                        c.Add(a[i]);
                    }
                }
                else if (bi < b.Count)
                {
                    for (int i = bi; i < b.Count; i++)
                    {
                        c.Add(b[i]);
                    }
                }
                return c;
            }
            List<LandmarkView> re = new List<LandmarkView>();
            re.Add(h[l]);
            return re;
        }
    }
}
