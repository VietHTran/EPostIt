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
                BackgroundColor = Color.Olive,
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
                VerticalOptions=LayoutOptions.FillAndExpand,
                Spacing=10
            };
        }
        NoteViewList items;
        private StackLayout GenerateAllNoteS()
        {
            StackLayout holder = new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                Padding = 10,
            };

            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date Created", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout typeH = wrapperLabel(GenerateLabel("Type", Color.White, 25, FontAttributes.None, TextAlignment.Center));

            heading.Children.Add(nameH,0,0);
            Grid.SetColumnSpan(nameH, 5);
            heading.Children.Add(dateH, 5, 0);
            Grid.SetColumnSpan(dateH, 3);
            heading.Children.Add(typeH, 8, 0);
            Grid.SetColumnSpan(typeH, 2);
            
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
            return holder;
        }
        private StackLayout GenerateTimeNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            return holder;
        }
        private StackLayout GenerateLocationNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            return holder;
        }
        private Button quickNoteS, timeNoteS, locationNoteS, allNoteS, currentButton, back, add, toggleMode, reload;
        private Grid tabButtons, buttonList;
        private StackLayout content, allNotes, quickNotes, timeNotes, locationNotes, currentContent;
        private bool selectMode;
        private Label empty;
        public SeeNote()
        {
            Initialization();
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

            Debug.WriteLine("Block A");
            allNotes = GenerateAllNoteS();
            Debug.WriteLine("Block B");
            quickNotes = GenerateQuickNoteS();
            Debug.WriteLine("Block C");
            timeNotes = GenerateTimeNoteS();
            Debug.WriteLine("Block D");
            locationNotes = GenerateLocationNoteS();
            Debug.WriteLine("Block E");
            currentContent = allNotes;

            back = GenerateButton("Back", Color.White, Color.Gray);
            add = GenerateButton("Add Note", Color.White, Color.Green);
            toggleMode = GenerateButton("Select Mode", Color.White, Color.Green);
            reload = GenerateButton("Reload", Color.White, Color.Green);
            
            buttonList = new Grid {
                HorizontalOptions=LayoutOptions.FillAndExpand
            };
            buttonList.Children.Add(back,0,0);
            //buttonList.Children.Add(reload, 1, 0);
            //buttonList.Children.Add(add,2,0);
            buttonList.Children.Add(toggleMode, 3, 0);
            
            empty = GenerateLabel("\n\n\n\n\nNo landmark registered\n\n\n\n\n", Color.White, 30, FontAttributes.None, TextAlignment.Center);
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
            Content = content;
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
            Task.Run(() => {
                items = new NoteViewList();
                /*
                foreach (var i in items.quickNotes)
                {
                    quickNotes.Children.Add(i);
                }
                foreach (var i in items.timeNotes)
                {
                    timeNotes.Children.Add(i);
                }
                foreach (var i in items.locationNotes)
                {
                    locationNotes.Children.Add(i);
                }
                */
            });
        }
    }
}
