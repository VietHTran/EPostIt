using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace EPostIt
{
    class NoteView : Grid
    {
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

        private int code; // quick(0), time(1), location(2)
        public Note note { get; set; }
        public string shortText;
        public string quickNText;
        public LocationNote noteL { get; set; }
        public TimeNote noteT { get; set; }
        private string dateC;
        private string noteType;
        //Location-based only
        Label distanceL;
        double distance;
        public NoteView(Note n)
        {
            this.code = 0;
            note = n;
            Debug.WriteLine($"supperupper: {note.dateCreated.ToString("MM/dd/yyyy")}");
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Quick";
            CommonGenerate();
            GenerateQuickNote();
        }
        public NoteView (TimeNote n)
        {
            this.code = 1;
            noteT = n;
            note = n;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Time-based";
            CommonGenerate();
            GenerateTimeNote();
        }
        public NoteView(LocationNote n)
        {
            this.code = 2;
            note = n;
            noteL = n;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Location-based";
            CommonGenerate();
            GenerateLocationNote();
        }
        void GenerateText()
        {
            if (note.NoteContent.Length>=25)
            {
                shortText = note.NoteContent.Substring(0,25);
                shortText += "...";
            } else
            {
                shortText = note.NoteContent;
            }
        }
        void GenerateTextLimit(int l)
        {
            if (note.NoteContent.Length >= l)
            {
                quickNText = note.NoteContent.Substring(0, l);
                quickNText += "...";
            }
            else
            {
                quickNText = note.NoteContent;
            }
        }
        void CommonGenerate()
        {
            Padding = 0;
            HeightRequest = 90;
            BackgroundColor = Color.Green;
            GenerateText();
            GenerateTextLimit(35);
            //dateC = note.dateCreated.ToString("MM/dd/yyyy");
            Debug.WriteLine("I'm tired of your bullshit");
            try
            {
                Debug.WriteLine($"supperupper: {note.dateCreated.ToString("MM/dd/yyyy")}");
            } catch (NullReferenceException e)
            {
                Debug.WriteLine("I'm tired of your bullshit");
            }
            
            dateC = DateTime.Now.ToString("MM/dd/yyyy");
            Debug.WriteLine("supper");
        }
        void SpecialGenerate()
        {
            if (code==1)
            {

            }
        }
        public void GenerateTimeNote()
        {
            Children.Clear();
            string dateT = noteT.dateCreated.ToString("MM/dd/yyyy");
            string status;
            if (noteT.isTriggered)
            {
                status = "On";
            } else
            {
                status = "Off";
            }
            Label name = GenerateLabel(shortText,Color.White,25,FontAttributes.None,TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateTriggered = GenerateLabel(dateT, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label statusT = GenerateLabel(status, Color.White, 25, FontAttributes.None, TextAlignment.Center);

            Children.Add(name,0,0);
            Grid.SetColumnSpan(name,4);
            Children.Add(dateCreated, 4, 0);
            Grid.SetColumnSpan(dateCreated, 2);
            Children.Add(dateTriggered, 6, 0);
            Grid.SetColumnSpan(dateTriggered, 2);
            Children.Add(statusT, 8, 0);
        }
        public void GenerateLocationNote()
        {
            Children.Clear();
            string landmarkN = noteL.landmark.name;
            distance = ManagerLocation.CalcDistance(noteL.landmark.latitude,noteL.landmark.longitude);
            string status;
            if (noteT.isTriggered)
            {
                status = "On";
            }
            else
            {
                status = "Off";
            }
            Label name = GenerateLabel(shortText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label statusT = GenerateLabel(status, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label landmarkL = GenerateLabel(landmarkN, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            /*
            if (distance > 999)
            {
                distanceL = GenerateLabel("+999", Color.White, 25, FontAttributes.None, TextAlignment.Center);
            }
            else
            {
                distanceL = GenerateLabel(Math.Round(distance, 1).ToString(), Color.White, 25, FontAttributes.None, TextAlignment.Center);
            }
            */
            Children.Add(name,0,0);
            Grid.SetColumnSpan(name,4);
            Children.Add(dateCreated, 4, 0);
            Grid.SetColumnSpan(dateCreated, 3);
            Children.Add(landmarkL, 7, 0);
            Grid.SetColumnSpan(landmarkL, 4);
            //Children.Add(distanceL, 9, 0);
            //Grid.SetColumnSpan(distanceL, 2);
            Children.Add(statusT,11, 0);
            Grid.SetColumnSpan(statusT, 2);
        }
        public void GenerateQuickNote()
        {
            Children.Clear();
            Label name = GenerateLabel(quickNText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            
            Children.Add(name, 0, 0);
            Grid.SetColumnSpan(name, 7);
            Children.Add(dateCreated, 8, 0);
            Grid.SetColumnSpan(dateCreated, 3);
        }
        public NoteView GenerateAllNote()
        {
            NoteView grid= new NoteView(note);
            grid.Children.Clear();
            Label name = GenerateLabel(shortText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label typeL = GenerateLabel(noteType, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            grid.Children.Add(name, 0, 0);
            Grid.SetColumnSpan(name, 5);
            grid.Children.Add(typeL, 5, 0);
            Grid.SetColumnSpan(typeL, 3);
            grid.Children.Add(dateCreated, 8, 0);
            Grid.SetColumnSpan(dateCreated, 3);
            return grid;
        }
    }
}
