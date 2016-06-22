using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class AddNoteOptions: ContentPage
    {
        private Button ButtonGenerator(String t)
        {
            Button a = new Button { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White, FontAttributes = FontAttributes.Bold };
            a.BackgroundColor = Color.Red;
            a.FontSize = 32;
            a.Text = t;
            return a;
        }
        public AddNoteOptions()
        {
            this.Padding = 20;
            Button quick = ButtonGenerator("Quick Note");
            quick.Clicked += AddNoteQ;
            Button time = ButtonGenerator("Time-based Note");
            time.Clicked += AddNoteT;
            Button location = ButtonGenerator("Location-based Note");
            location.Clicked += AddNoteL;
            Button back = ButtonGenerator("Back");
            back.Clicked += BackB;
            Content = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {quick,time,location,back}
            };
        }
        void BackB(object sender, EventArgs ea)
        {
            Navigation.PopAsync();
        }
        void AddNoteQ(object sender, EventArgs ea)
        {
            Navigation.PushAsync(new AddNoteQ());
        }
        void AddNoteT(object sender, EventArgs ea)
        {
            Navigation.PushAsync(new AddNoteT());
        }
        void AddNoteL(object sender, EventArgs ea)
        {
            if (ManagerLocation.latitude==0 && ManagerLocation.longitude==0)
            {
                DisplayAlert("Location Unknown","The app has yet to find the location of the device. Please come back in a few minutes","OK");
            } else
            {
                Navigation.PushAsync(new AddNoteL());
            }
        }
    }
}
