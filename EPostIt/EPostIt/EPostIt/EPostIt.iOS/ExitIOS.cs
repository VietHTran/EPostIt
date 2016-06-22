using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EPostIt.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExitIOS))]
namespace EPostIt.iOS
{
    class ExitIOS : IExit
    {
        public void exitApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
