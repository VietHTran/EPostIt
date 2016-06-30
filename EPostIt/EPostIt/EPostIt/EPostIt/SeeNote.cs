using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace EPostIt
{
    class SeeNote : ContentPage, ITapLock
    {
        public TapLockVars TapLockVars
        { get; set; }
        private Button GenerateButton(string text, Color textColor, Color backgroundColor)
        {
            Button holder = new Button
            {
                Text = text,
                TextColor = textColor,
                BackgroundColor = backgroundColor,
                FontAttributes = FontAttributes.Bold,
                FontSize = 25
            };
            return holder;
        }
        private StackLayout wrapperLabel(Label a)
        {
            StackLayout holder = new StackLayout
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#13470A"),
                Children = { a }
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
        private StackLayout GenerateSectionStack()
        {
            return new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                Padding=10
            };
        }
        private StackLayout GenerateAllNoteS()
        {
            StackLayout holder = new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                Padding = 10
            };

            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date\nCreated", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout typeH = wrapperLabel(GenerateLabel("Type", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));

            heading.Children.Add(nameH,0,0);
            Grid.SetColumnSpan(nameH, 5);
            heading.Children.Add(dateH, 5, 0);
            Grid.SetColumnSpan(dateH, 3);
            heading.Children.Add(typeH, 8, 0);
            Grid.SetColumnSpan(typeH, 3);
            
            holder.Children.Add(heading);
            foreach (var i in items.allNotes)
            {
                holder.Children.Add(i);
            }
            
            return holder;
        }
        private StackLayout GenerateQuickNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date\nCreated", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));

            heading.Children.Add(nameH,0,0);
            Grid.SetColumnSpan(nameH,8);
            heading.Children.Add(dateH, 8, 0);
            Grid.SetColumnSpan(dateH, 3);

            holder.Children.Add(heading);
            foreach (var i in items.quickNotes)
            {
                holder.Children.Add(i);
            }

            return holder;
        }
        private StackLayout GenerateTimeNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date\nCreated", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateTH = wrapperLabel(GenerateLabel("Date\nTriggered", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout statusH = wrapperLabel(GenerateLabel("Status", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            
            heading.Children.Add(nameH, 0, 0);
            Grid.SetColumnSpan(nameH, 7);
            heading.Children.Add(dateH, 7, 0);
            Grid.SetColumnSpan(dateH, 4);
            heading.Children.Add(dateTH, 11, 0);
            Grid.SetColumnSpan(dateTH, 4);
            heading.Children.Add(statusH, 15, 0);
            Grid.SetColumnSpan(statusH, 3);

            holder.Children.Add(heading);
            foreach (var i in items.timeNotes)
            {
                holder.Children.Add(i);
            }

            return holder;
        }
        private StackLayout GenerateLocationNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date\nCreated", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout landmarkH = wrapperLabel(GenerateLabel("Landmark", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout statusH = wrapperLabel(GenerateLabel("Status", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));

            heading.Children.Add(nameH, 0, 0);
            Grid.SetColumnSpan(nameH, 4);
            heading.Children.Add(dateH, 4, 0);
            Grid.SetColumnSpan(dateH, 3);
            heading.Children.Add(landmarkH, 7, 0);
            Grid.SetColumnSpan(landmarkH, 4);
            heading.Children.Add(statusH, 11, 0);
            Grid.SetColumnSpan(statusH, 2);

            holder.Children.Add(heading);
            foreach (var i in items.locationNotes)
            {
                holder.Children.Add(i);
            }

            return holder;
        }
        private Button quickNoteS, timeNoteS, locationNoteS, allNoteS, currentButton, back, add, toggleMode, reload;
        private Grid tabButtons, buttonList;
        private StackLayout content, allNotes, quickNotes, timeNotes, locationNotes, currentContent;
        //private bool selectMode;
        private NoteViewList items;
        private Label empty;
        public SeeNote()
        {
            TestInit();
            Debug.WriteLine("Heisenberg");
            Initialization();
            Debug.WriteLine("Say my name");
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Title = "Note Lists";
            quickNoteS = GenerateButton("Quick Notes", Color.White, Color.Green);
            timeNoteS = GenerateButton("Time-based\nNotes", Color.White, Color.Green);
            locationNoteS = GenerateButton("Location-based\nNotes", Color.White, Color.Green);
            allNoteS = GenerateButton("All Notes", Color.White, Color.Green);
            tabButtons = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
            };
            tabButtons.Children.Add(quickNoteS, 1, 0);
            tabButtons.Children.Add(timeNoteS, 2, 0);
            tabButtons.Children.Add(locationNoteS, 3, 0);
            tabButtons.Children.Add(allNoteS, 0, 0);
            allNoteS.Clicked += OpenAll;
            quickNoteS.Clicked += OpenQuick;
            locationNoteS.Clicked += OpenLocation;
            timeNoteS.Clicked += OpenTime;
            currentButton = allNoteS;
            UpdateButton();
            
            allNotes = GenerateAllNoteS();
            quickNotes = GenerateQuickNoteS();
            timeNotes = GenerateTimeNoteS();
            locationNotes = GenerateLocationNoteS();
            currentContent = allNotes;

            back = GenerateButton("Back", Color.White, Color.Gray);
            add = GenerateButton("Add Note", Color.White, Color.Green);
            toggleMode = GenerateButton("Select\nMode", Color.White, Color.Green);
            reload = GenerateButton("Reload", Color.White, Color.Green);

            buttonList = new Grid {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding=10,
            };
            buttonList.Children.Add(back,0,0);
            //buttonList.Children.Add(reload, 1, 0);
            //buttonList.Children.Add(add,2,0);
            buttonList.Children.Add(toggleMode, 3, 0);
            
            empty = GenerateLabel("\n\n\n\n\nEmpty\n\n\n\n\n", Color.White, 30, FontAttributes.None, TextAlignment.Center);
            empty.HorizontalOptions = LayoutOptions.FillAndExpand;
            empty.VerticalOptions = LayoutOptions.FillAndExpand;

            content = new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                Spacing=0,
                Children=
                {
                    tabButtons,
                    currentContent,
                    buttonList
                }
            };
            ScrollView wrapperScroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = content,
            };
            Content = wrapperScroll;
        }

        void UpdateButton ()
        {
            allNoteS.IsEnabled = true;
            quickNoteS.IsEnabled = true;
            timeNoteS.IsEnabled = true;
            locationNoteS.IsEnabled = true;
            currentButton.IsEnabled = false;
        }
        void OpenAll(object sender, EventArgs ea)
        {
            content.Children.Remove(currentContent);
            currentContent = allNotes;
            content.Children.Insert(1,currentContent);
            currentButton = allNoteS;
            UpdateButton();
        }
        void OpenQuick(object sender, EventArgs ea)
        {
            content.Children.Remove(currentContent);
            currentContent = quickNotes;
            content.Children.Insert(1, currentContent);
            currentButton = quickNoteS;
            UpdateButton();
        }
        void OpenTime(object sender, EventArgs ea)
        {
            content.Children.Remove(currentContent);
            currentContent = timeNotes;
            content.Children.Insert(1, currentContent);
            currentButton = timeNoteS;
            UpdateButton();
        }
        void OpenLocation(object sender, EventArgs ea)
        {
            content.Children.Remove(currentContent);
            currentContent = locationNotes;
            content.Children.Insert(1, currentContent);
            currentButton = locationNoteS;
            UpdateButton();
        }
        void Initialization ()
        {
            items = new NoteViewList();
        }
        void TestInit ()
        {
            if (NoteManager.quickNotes.Count!=0)
            {
                return;
            }
            for (int i=0;i<5;i++)
            {
                NoteManager.quickNotes.Add(new Note("Go lorem ipsum yourself"));
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.timeNotes.Add(new TimeNote("You disrespect me, you disrespect my family",DateTime.Now));
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.locationNotes.Add(new LocationNote("Wubba Lubba Dub Dub",LandmarkCollection.landmarks[1],1));
            }
        }
    }
}
