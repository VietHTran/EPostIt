using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class LandmarkView : Grid
    {
        public double distance { get; set; }
        public DateTime assignedTime { get; set; }
        public Landmark landmark { get; set; }
        private Label distanceL;
        public LandmarkView (Landmark l)
        {
            this.landmark = l;
            this.assignedTime = l.assignedTime;
            this.distance = ManagerLocation.CalcDistance(l.latitude, l.longitude);
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = 0;
            HeightRequest = 90;
            BackgroundColor = Color.Blue;
            string date = assignedTime.ToString("MM/dd/yyyy");
            Label name = GenerateLabel(landmark.name, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateAssigned = GenerateLabel(date, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label noteCount;
            
            if (landmark.assignedEvents<=99)
            {
                noteCount = GenerateLabel(landmark.assignedEvents.ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center);
            } else
            {
                noteCount = GenerateLabel("+99", Color.White, 25, FontAttributes.None, TextAlignment.Center);
            }
            if (distance>999)
            {
                distanceL = GenerateLabel("+999", Color.White, 25, FontAttributes.None, TextAlignment.Center);
            } else
            {
                distanceL = GenerateLabel(Math.Round(distance, 1).ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center);
            }
            Children.Add(name, 0, 0);
            Grid.SetColumnSpan(name, 4);
            Children.Add(dateAssigned, 4, 0);
            Grid.SetColumnSpan(dateAssigned, 3);
            Children.Add(noteCount, 7, 0);
            Grid.SetColumnSpan(noteCount, 2);
            Children.Add(distanceL, 9, 0);
            Grid.SetColumnSpan(distanceL, 2);
            BackgroundColor = Color.Blue;
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
        public void DeleteFromMainData()
        {
            LandmarkCollection.landmarks.Remove(landmark);
        }
    }
}
