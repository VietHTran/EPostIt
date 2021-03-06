﻿using System;
using Xamarin.Forms;

namespace EPostIt
{
    public class Note
    {
        public string NoteContent { get; set; }
        public DateTime dateCreated { get; set; }
        public int Id { get; set; }
        public Note(string text,bool isInherit)
        {
            this.NoteContent = text;
            dateCreated = DateTime.Now;
        }
        public Note(string text)
        {
            this.NoteContent = text;
            dateCreated = DateTime.Now;
            Id = App.nextID;
            App.nextID++;
        }
        public Note(string text, DateTime dateC, int id)
        {
            Id = id;
            NoteContent = text;
            dateCreated = dateC;
        }
    }
}
