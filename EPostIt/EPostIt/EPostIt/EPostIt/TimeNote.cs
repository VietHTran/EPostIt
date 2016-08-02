﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    public class TimeNote : Note
    {
        public DateTime DateTimeSet { get; set; }
        public INoteTimer Alarm { get; set; }
        //Only load once. To reload-> Change time
        public bool isTriggered { get; set; }
        public TimeNote(string s, DateTime setter) : base (s)
        {
            this.DateTimeSet = setter;
            this.isTriggered = false;
            Alarm = DependencyService.Get<INoteTimer>();
            Alarm.Remind(DateTimeSet,"Did you forget something?",NoteContent);
        }
        public bool IsTime()
        {
            if (DateTimeSet.CompareTo(DateTime.Now)<=0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static bool compareDateTime(DateTime d)
        {
            if (d.CompareTo(DateTime.Now)>0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
