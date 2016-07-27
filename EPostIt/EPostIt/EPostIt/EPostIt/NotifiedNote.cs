using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace EPostIt
{
    class NotifiedNote: ContentPage, ITapLock
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
                BackgroundColor = DarkYellow,
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
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10
            };
        }
        private void GenerateTimeHeading()
        {
            timeHeading = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand };
            StackLayout nameHT = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateTrigger = wrapperLabel(GenerateLabel("Date Triggered", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            timeHeading.Children.Add(nameHT, 0, 0);
            Grid.SetColumnSpan(nameHT, 5);
            timeHeading.Children.Add(dateTrigger, 5, 0);
            Grid.SetColumnSpan(dateTrigger, 3);
        } 
        private void GenerateLocationHeading()
        {
            locationHeading = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand };
            StackLayout nameHL = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout landmark = wrapperLabel(GenerateLabel("Landmark", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            locationHeading.Children.Add(nameHL, 0, 0);
            Grid.SetColumnSpan(nameHL, 5);
            locationHeading.Children.Add(landmark, 5, 0);
            Grid.SetColumnSpan(landmark, 3);
        }
        private Color DarkYellow = Color.FromHex("B7BC06");
        private Button back,timeNotes,locationNotes;
        private StackLayout content;
        private Grid timeHeading, locationHeading;
        private StackLayout timeNotesView, locationNotesView,currentView;
        private int tabID;
        public NotifiedNote()
        {
            Initialization();
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Title = "Notified Note";

            timeNotes = GenerateButton("Time\nNotes",Color.White,Color.Yellow);
            locationNotes= GenerateButton("Location\nNotes", Color.White, Color.Yellow);
            Grid tabButtons = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
            };
            tabButtons.Children.Add(timeNotes,0,0);
            tabButtons.Children.Add(locationNotes, 1, 0);

            GenerateTimeHeading();
            GenerateLocationHeading();

            back = GenerateButton("Back",Color.White,Color.Gray);
            Grid buttonList = new Grid
            {
                HorizontalOptions = LayoutOptions.Start,
                Padding = 10,
            };
            buttonList.Children.Add(back, 0, 0);

            tabID = 0;
            currentView = timeNotesView;

            content = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                Children=
                {
                    tabButtons,
                    timeHeading,
                    currentView,
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
        void Initialization()
        {
            timeNotesView = GenerateSectionStack();
            locationNotesView = GenerateSectionStack();
            timeNotesView.Padding = 0;
            locationNotesView.Padding = 0;
            for (int i=0;i<NoteManager.timeNotes.Count;i++)
            {
                if (!NoteManager.timeNotes[i].isTriggered)
                {
                    timeNotesView.Children.Add(new NotifiedView(NoteManager.timeNotes[i]));
                }
            }
            for (int i = 0; i < NoteManager.locationNotes.Count; i++)
            {
                if (!NoteManager.locationNotes[i].isTriggered)
                {
                    locationNotesView.Children.Add(new NotifiedView(NoteManager.locationNotes[i]));
                }
            }
        }
    }
}
