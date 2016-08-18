using System.Collections.Generic;

namespace EPostIt
{
    class NoteViewList : List<NoteView>
    {
        public List<NoteView> allNotes;
        public List<NoteView> quickNotes;
        public List<NoteView> timeNotes;
        public List<NoteView> locationNotes;
        public void QuickNotesInit()
        {
            foreach (var i in NoteManager.quickNotes)
            {
                quickNotes.Add(new NoteView(i));
                allNotes.Add(quickNotes[quickNotes.Count - 1].GenerateAllNote());
            }
        }
        public void TimeNotesInit()
        {
            foreach (var i in NoteManager.timeNotes)
            {
                timeNotes.Add(new NoteView(i));
                allNotes.Add(timeNotes[timeNotes.Count - 1].GenerateAllNote());
            }
        }
        public void LocationNotesInit()
        {
            foreach (var i in NoteManager.locationNotes)
            {
                locationNotes.Add(new NoteView(i));
                allNotes.Add(locationNotes[locationNotes.Count - 1].GenerateAllNote());
            }
        }
        public NoteViewList()
        {
            allNotes = new List<NoteView>();
            quickNotes = new List<NoteView>();
            timeNotes = new List<NoteView>();
            locationNotes = new List<NoteView>();
            QuickNotesInit();
            TimeNotesInit();
            LocationNotesInit();
        }
        public List<NoteView> All()
        {
            return allNotes;
        }
        public List<NoteView> Quick()
        {
            return quickNotes;
        }
        public List<NoteView> Time()
        {
            return timeNotes;
        }
        public List<NoteView> Location()
        {
            return locationNotes;
        }
    }
}
