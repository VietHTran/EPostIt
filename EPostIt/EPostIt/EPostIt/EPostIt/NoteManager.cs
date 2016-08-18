using System.Collections.Generic;

namespace EPostIt
{
    class NoteManager
    {
        public static List<Note> quickNotes;
        public static List<LocationNote> locationNotes;
        public static List<TimeNote> timeNotes;
        public static void Init()
        {
            quickNotes = new List<Note>();
            locationNotes = new List<LocationNote>();
            timeNotes = new List<TimeNote>();
        }
    }
}
