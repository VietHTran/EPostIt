using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class LandmarkListItem
    {
        public double distance { get; set; }
        public DateTime assignedTime { get; set; }
        public Landmark landmark { get; set; }
        private Frame distanceL;
        public LandmarkListItem(Landmark l)
        {
            this.landmark = l;
            this.assignedTime = l.assignedTime;
            this.distance = ManagerLocation.CalcDistance(l.latitude,l.longitude);
        }
        private Frame wrapperStackLayout(Layout a)
        {
            Frame holder = new Frame
            {
                Content = a,
                Padding = 0,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            return holder;
        }
        private Frame wrapperLandmark(Layout a)
        {
            Frame holder = new Frame
            {
                Content = a,
                Padding = 0,
                BackgroundColor = Color.Blue,
            };
            return holder;
        }
        private Label GenerateLabel(string text, Color colorText, int size, FontAttributes attr, TextAlignment al)
        {
            return new Label
            {
                Text = text,
                FontSize = size,
                TextColor = colorText,
                FontAttributes = attr,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = al,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
        }
        private StackLayout wrapperLabel(Label a)
        {
            StackLayout holder = new StackLayout
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { a }
            };
            return holder;
        }
        public Frame GenerateViewItem()
        {
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                HeightRequest = 90
            };
            string date = assignedTime.ToString("MM/dd/yyyy");
            Frame name = wrapperStackLayout(wrapperLabel(GenerateLabel(landmark.name, Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            Frame dateAssigned = wrapperStackLayout(wrapperLabel(GenerateLabel(date, Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            Frame noteCount;
            if (landmark.assignedEvents <= 99)
            {
                noteCount = wrapperStackLayout(wrapperLabel(GenerateLabel(landmark.assignedEvents.ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            }
            else
            {
                noteCount = wrapperStackLayout(wrapperLabel(GenerateLabel("+99", Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            }
            
            if (distance>999)
                distanceL = wrapperStackLayout(wrapperLabel(GenerateLabel("+999", Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            else
                distanceL = wrapperStackLayout(wrapperLabel(GenerateLabel(Math.Round(distance,1).ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            
            grid.Children.Add(name, 0, 0);
            Grid.SetColumnSpan(name, 4);
            grid.Children.Add(dateAssigned, 4, 0);
            Grid.SetColumnSpan(dateAssigned, 3);
            grid.Children.Add(noteCount, 7, 0);
            Grid.SetColumnSpan(noteCount, 2);
            grid.Children.Add(distanceL, 9, 0);
            Grid.SetColumnSpan(distanceL, 2);

            return wrapperLandmark(grid);
        }
        public void ReCalcDistance()
        {
            this.distance = ManagerLocation.CalcDistance(landmark.latitude, landmark.longitude);
            if (distance > 999)
                distanceL = wrapperStackLayout(wrapperLabel(GenerateLabel("+999", Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            else
                distanceL = wrapperStackLayout(wrapperLabel(GenerateLabel(Math.Round(distance, 1).ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center)));
        }
    }
}
