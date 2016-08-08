using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class AddNoteOptions : ContentPage, ITapLock
    {
        public TapLockVars TapLockVars
        { get; set; }
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
            Content = new StackLayout
            {
                Spacing = 15,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { quick, time, location, back }
            };

        }
        async void BackB(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                await Navigation.PopAsync();
                this.ReleaseTapLock();
            }
        }
        async void AddNoteQ(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                await Navigation.PushAsync(new AddNoteQ());
                this.ReleaseTapLock();
            }
        }
        async void AddNoteT(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                await Navigation.PushAsync(new AddNoteT());
                this.ReleaseTapLock();
            }
            /*if (this.AcquireTapLock())
            {
                
                this.ReleaseTapLock();
            }*/
        }
        async void AddNoteL(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                if (ManagerLocation.Latitude == 0 && ManagerLocation.Longitude == 0)
                {
                    await DisplayAlert("Location Unknown", "The app has yet to find the location of the device. Please come back in a few minutes", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new AddNoteL());
                }
                this.ReleaseTapLock();
            }
        }
    }
}
