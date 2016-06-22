using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using EPostIt.WinPhone;

[assembly: Dependency(typeof(ExitWindow))]
namespace EPostIt.WinPhone
{
    class ExitWindow : IExit
    {
        public void exitApp()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
