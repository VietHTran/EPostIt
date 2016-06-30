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
        public string timeNText;
        public LocationNote noteL { get; set; }
        public TimeNote noteT { get; set; }
        private string dateC;
        private string noteType;
        //Location-based only
        double distance;
        public NoteView(Note n)
        {
            this.code = 0;
            note = n;
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
            Debug.WriteLine("salude4");
        }
        void GenerateText()
        {
            int u=-1;
            string[] holder = note.NoteContent.Split('\n');
            for (int i=0;i<holder.Length;i++)
            {
                if (!holder[i].Equals(""))
                {
                    u = i;
                    break;
                }
            }
            if (u == -1) u = 0;
            if (holder[u].Length>=20)
            {
                shortText = holder[u].Substring(0,20);
                shortText += "...";
            } else
            {
                shortText = holder[u];
            }
        }
        void GenerateTextLimit(int l)
        {
            int u = -1;
            string[] holder = note.NoteContent.Split('\n');
            for (int i = 0; i < holder.Length; i++)
            {
                if (!holder[i].Equals(""))
                {
                    u = i;
                    break;
                }
            }
            if (u == -1) u = 0;
            if (holder[u].Length >= l)
            {
                quickNText = holder[u].Substring(0, l);
                quickNText += "...";
            }
            else
            {
                quickNText = holder[u];
            }
        }
        void GenerateTextLimitTime(int l)
        {
            if (note.NoteContent.Length >= l)
            {
                timeNText = note.NoteContent.Substring(0, l);
                timeNText += "...";
            }
            else
            {
                timeNText = note.NoteContent;
            }
        }
        void CommonGenerate()
        {
            Padding = 0;
            HeightRequest = 70;
            BackgroundColor = Color.Green;
            GenerateText();
            GenerateTextLimit(45);
            GenerateTextLimitTime(10);
            dateC = note.dateCreated.ToString("MM/dd/yyyy");
        }
        /*void SpecialGenerate()
        {
            if (code==1)
            {

            }
        }*/
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
            Grid.SetColumnSpan(name,7);
            Children.Add(dateCreated, 7, 0);
            Grid.SetColumnSpan(dateCreated, 4);
            Children.Add(dateTriggered, 11, 0);
            Grid.SetColumnSpan(dateTriggered, 4);
            Children.Add(statusT, 15, 0);
            Grid.SetColumnSpan(statusT, 3);
        }
        public void GenerateLocationNote()
        {
            Children.Clear();
            string landmarkN = noteL.landmark.name;
            distance = ManagerLocation.CalcDistance(noteL.landmark.latitude,noteL.landmark.longitude);
            string status;
            if (noteL.isTriggered)
            {
                status = "On";
            }
            else
            {
                status = "Off";
            }
            Label name = GenerateLabel(timeNText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label statusT = GenerateLabel(status, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label landmarkL = GenerateLabel(landmarkN, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Children.Add(name,0,0);
            Grid.SetColumnSpan(name,4);
            Children.Add(dateCreated, 4, 0);
            Grid.SetColumnSpan(dateCreated, 3);
            Children.Add(landmarkL, 7, 0);
            Grid.SetColumnSpan(landmarkL, 4);
            Children.Add(statusT,11, 0);
            Grid.SetColumnSpan(statusT, 2);
        }
        public void GenerateQuickNote()
        {
            Children.Clear();
            Label name = GenerateLabel(quickNText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            
            Children.Add(name, 0, 0);
            Grid.SetColumnSpan(name, 8);
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
            grid.Children.Add(typeL, 8, 0);
            Grid.SetColumnSpan(typeL, 3);
            grid.Children.Add(dateCreated, 5, 0);
            Grid.SetColumnSpan(dateCreated, 3);
            return grid;
        }
    }
}
