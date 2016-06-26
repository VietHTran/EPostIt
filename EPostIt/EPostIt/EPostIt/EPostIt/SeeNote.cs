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
            

            //InitializeAllNote();
            holder.Children.Add(heading);
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
        private Button quickNoteS, timeNoteS, locationNoteS, allNoteS, currentButton;
        private Grid tabButtons;
        private StackLayout content, allNotes, quickNotes, timeNotes, locationNotes, currentContent;

        public SeeNote()
        {
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Title = "Note Lists";
            quickNoteS = GenerateButton("Quick Notes",Color.White,Color.Green);
            timeNoteS = GenerateButton("Time-based\nNotes", Color.White, Color.Green);
            locationNoteS = GenerateButton("Location-based\nNotes", Color.White, Color.Green);
            allNoteS = GenerateButton("All Notes", Color.White, Color.Green);
            tabButtons = new Grid
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                Padding=10,
            };
            tabButtons.Children.Add(quickNoteS,1,0);
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

            content = new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                Spacing=0,
                Children=
                {
                    tabButtons,
                    currentContent
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
    }
}
