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
        public int Code { get; set; } // quick(0), time(1), location(2)
        public Note note { get; set; }
        public string shortText { get; set; }
        public string quickNText { get; set; }
        public string timeNText { get; set; }
        public LocationNote noteL { get; set; }
        public TimeNote noteT { get; set; }
        public string dateC { get; set; }
        public string Status { get; set; }
        private string noteType;
        //Location-based only
        double distance;
        public NoteView(Note n)
        {
            this.Code = 0;
            note = n;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Quick";
            CommonGenerate();
            GenerateQuickNote();
        }
        public NoteView (TimeNote n)
        {
            this.Code = 1;
            noteT = n;
            note = n;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Time-based";
            CommonGenerate();
            GenerateTimeNote();
        }
        public NoteView(LocationNote n)
        {
            this.Code = 2;
            note = n;
            noteL = n;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteType = "Location-based";
            CommonGenerate();
            GenerateLocationNote();
        }
        public string CalcDistance()
        {
            if (ManagerLocation.Latitude==0 && ManagerLocation.Longitude==0)
            {
                return "Not yet to determine location";
            }
            distance = ManagerLocation.CalcDistance(noteL.landmark.latitude, noteL.landmark.longitude);
            if (distance>999)
            {
                return "+999 meters";
            } else
            {
                return $"{Math.Round(distance, 1).ToString()} meter(s)";
            }
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
            Padding = 10;
            HeightRequest = 70;
            BackgroundColor = Color.Green;
            GenerateText();
            GenerateTextLimit(45);
            GenerateTextLimitTime(10);
            dateC = note.dateCreated.ToString("MM/dd/yyyy");
        }
        public void Update()
        {
            GenerateText();
            GenerateTextLimit(45);
            GenerateTextLimitTime(10);
            dateC = note.dateCreated.ToString("MM/dd/yyyy");
            GenerateAllNote();
            if (Code==0)
            {
                GenerateQuickNote();
            } else if (Code==1)
            {
                GenerateTimeNote();
            }
            else if (Code == 2)
            {
                GenerateLocationNote();
            }
        }
        public void UpdateAll()
        {
            CommonGenerate();
            Label name = Children[0] as Label;
            Label dateCreated = Children[2] as Label;
            Label type = Children[1] as Label;
            name.Text = shortText;
            dateCreated.Text = dateC;
            type.Text = noteType;
        }
        public void GenerateTimeNote()
        {
            Children.Clear();
            string dateT = noteT.DateTimeSet.ToString("MM/dd/yyyy");
            if (noteT.IsTime() && noteT.IsTriggered)
            {
                Status = "On";
            } else
            {
                Status = "Off";
            }
            Label name = GenerateLabel(shortText,Color.White,25,FontAttributes.None,TextAlignment.Center);
            Label dateCreated = GenerateLabel(dateC, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label dateTriggered = GenerateLabel(dateT, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label statusT = GenerateLabel(Status, Color.White, 25, FontAttributes.None, TextAlignment.Center);

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
            if (noteL.IsNotified)
            {
                Status = "On";
            }
            else
            {
                Status = "Off";
            }
            Label name = GenerateLabel(timeNText, Color.White, 25, FontAttributes.None, TextAlignment.Center);
            Label statusT = GenerateLabel(Status, Color.White, 25, FontAttributes.None, TextAlignment.Center);
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
            grid.noteType = noteType;
            grid.Code = Code;
            grid.Status = Status;
            if (Code==1)
            {
                grid.noteT = noteT;
            } else if (Code==2)
            {
                grid.noteL = noteL;
            }
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
        public void DeleteFromDatabase()
        {
            switch (Code)
            {
                case 0:
                    App.mainDatabase.Delete<QuickNoteDB>(note.Id);
                    NoteManager.quickNotes.Remove(note);
                    break;
                case 1:
                    if (!noteT.IsTime() && noteT.IsTriggered)
                        noteT.Alarm.Cancel(noteT.Id);
                    App.mainDatabase.Delete<TimeNoteDB>(noteT.Id);
                    NoteManager.timeNotes.Remove(noteT);
                    break;
                case 2:
                    LandmarkCollection.landmarks[LandmarkCollection.landmarks.IndexOf(noteL.landmark)].UnassignEvent();
                    App.mainDatabase.Delete<LocationNoteDB>(noteL.Id);
                    NoteManager.locationNotes.Remove(noteL);
                    break;
            }
        }
    }
}
