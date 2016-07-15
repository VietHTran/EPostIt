﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostIt
{
     
    static class AppController
    {
        public static bool isEdit=false;
        public static NoteView Holder { get; set; }
        public static NoteView Holder1 { get; set; }
        public static SeeNote prevPage;
    }
    public interface ITapLock
    {
        TapLockVars TapLockVars { get; set; }
    }

    public struct TapLockVars
    {
        public bool Locked;
    }
    public static class TapLockExtensions
    {

        private static DateTime _lastTappedTime = DateTime.Now;
        public static bool AcquireTapLock(this ITapLock obj)
        {
            // if locked is true, return false
            // if locked is false, set to true and return true
            try
            {
                var vars = obj.TapLockVars;
                return (!vars.Locked && (vars.Locked = true) && (obj.TapLockVars = vars).Locked) ||
                       _lastTappedTime.AddSeconds(3) < DateTime.Now;
            }
            finally
            {
                _lastTappedTime = DateTime.Now;
            }


        }

        public static void ReleaseTapLock(this ITapLock obj)
        {
            var vars = obj.TapLockVars;
            vars.Locked = false;
            obj.TapLockVars = vars;
        }

    }
}
