using Xamarin.Forms;

namespace EPostIt
{
    class PageManager
    {
        public static NavigationPage Init()
        {
            NavigationPage nav = new NavigationPage(new MainPage());
            return nav;
        }
    }
}
