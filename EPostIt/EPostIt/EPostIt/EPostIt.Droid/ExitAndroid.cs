using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EPostIt.Droid;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(ExitAndroid))]
namespace EPostIt.Droid
{
    
    class ExitAndroid : IExit
    {
        
        public void exitApp()
        {
            System.Diagnostics.Debug.Write("a\n");
            System.Environment.Exit(0);
        }
    }
}