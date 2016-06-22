using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace EPostIt
{
    class MainPage : ContentPage
    {

        public MainPage ()
        {
            this.Padding = 20;
            Button add = new Button {Text="Add New Note" , BackgroundColor= Color.Red, FontSize=32, VerticalOptions= LayoutOptions.FillAndExpand, HorizontalOptions=LayoutOptions.FillAndExpand, TextColor=Color.White, FontAttributes=FontAttributes.Bold};
            add.Clicked += AddNote;
            Button check = new Button { Text = "See Note List", BackgroundColor = Color.Green, FontSize = 32, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White, FontAttributes = FontAttributes.Bold };
            Button list = new Button { Text = "Landmark List", BackgroundColor = Color.Blue, FontSize = 32, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White, FontAttributes = FontAttributes.Bold };
            list.Clicked += LandmarkList;
            Button exit = new Button {
                Text = "Exit   ",
                BackgroundColor = Color.Yellow,
                FontSize = 32,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
            };
            exit.Clicked += async (sender, ea) => await Exit(sender, ea);
            /*
            this.Content = new StackLayout {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Children = { new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing=15,
                        Children= {add,check}
                    }, new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing=15,
                        Children= {help,exit}
                    }
                }
            };
            */
            var grid = new Grid {
                ColumnSpacing = 15,
                RowSpacing=15,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //Children= {add,check,help,exit}
            };
            grid.Children.Add(add,0,0);
            grid.Children.Add(check, 1, 0);
            grid.Children.Add(list, 0, 1);
            grid.Children.Add(exit, 1, 1);
            Content = grid;
        }
        void AddNote(object sender, EventArgs ea)
        {
            Navigation.PushAsync(new AddNoteOptions());
        }
        void LandmarkList(object sender, EventArgs ea)
        {
            if (ManagerLocation.latitude == 0 && ManagerLocation.longitude == 0)
            {
                DisplayAlert("Location Unknown", "The app has yet to find the location of the device. Please come back in a few minutes", "OK");
            }
            else
            {
                Navigation.PushAsync(new LandmarkList());
            }
        }
        async Task Exit(object sender, EventArgs ea)
        {
            bool isExit=await DisplayAlert("", "You haven't saved this note yet. Do you want to exit?", "Yes", "No");
            if(!isExit)
            {
                return;
            }
            DependencyService.Get<IExit>().exitApp();
        }
    }
}
