using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class Note
    {
        public string NoteContent { get; set; }
        public DateTime dateCreated { get; set; }
        public Note(string text)
        {
            this.NoteContent = text;
            dateCreated = DateTime.Now;
        }

    }
}
