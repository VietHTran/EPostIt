using EPostIt.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExitAndroid))]
namespace EPostIt.Droid
{
    
    class ExitAndroid : IExit
    {
        
        public void exitApp()
        {
            System.Environment.Exit(0);
        }
    }
}