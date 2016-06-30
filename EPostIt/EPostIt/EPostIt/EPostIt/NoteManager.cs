using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
