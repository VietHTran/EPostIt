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
            timeHeading = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Padding=10 };
            StackLayout nameHT = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateTrigger = wrapperLabel(GenerateLabel("Time\nTriggered", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            timeHeading.Children.Add(nameHT, 0, 0);
            Grid.SetColumnSpan(nameHT, 5);
            timeHeading.Children.Add(dateTrigger, 5, 0);
            Grid.SetColumnSpan(dateTrigger, 3);
        } 
        private void GenerateLocationHeading()
        {
            locationHeading = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Padding = 10 };
            StackLayout nameHL = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout landmark = wrapperLabel(GenerateLabel("Landmark\nName", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            locationHeading.Children.Add(nameHL, 0, 0);
            Grid.SetColumnSpan(nameHL, 5);
            locationHeading.Children.Add(landmark, 5, 0);
            Grid.SetColumnSpan(landmark, 3);
        }
        private void GenerateSwitch()
        {
            Label titleT = GenerateLabel("Notify notes: ",Color.White,25,FontAttributes.Bold,TextAlignment.End);
            Label titleL = GenerateLabel("Notify notes: ", Color.White, 25, FontAttributes.Bold, TextAlignment.End);
            isNotiTime = new Switch {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsToggled=AppController.TimeNotification
            };
            isNotiLocation = new Switch
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsToggled=AppController.LocationNotification
            };
            isNotiTime.Toggled += timeNotiToggle;
            isNotiLocation.Toggled += locationNotiToggle;
            notiLocationSect = GenerateSectionStack();
            notiTimeSect = GenerateSectionStack();
            notiTimeSect.Orientation = StackOrientation.Horizontal;
            notiLocationSect.Orientation = StackOrientation.Horizontal;
            notiLocationSect.Children.Add(titleL);
            notiLocationSect.Children.Add(isNotiLocation);
            notiTimeSect.Children.Add(titleT);
            notiTimeSect.Children.Add(isNotiTime);
        }
        private Color DarkYellow = Color.FromHex("B7BC06");
        private Color yellow = Color.FromHex("DED700");
        private Button back,timeNotes,locationNotes;
        private StackLayout content,notiTimeSect,notiLocationSect,notiCurrentSect;
        private Switch isNotiTime, isNotiLocation;
        private Grid timeHeading, locationHeading, currentHeading;
        private StackLayout timeNotesView, locationNotesView,currentView;
        private Label empty;
        private List<NotifiedView> timeNList, locationNList;
        private int tabID;
        private NotifiedNoteTime carouselT;
        private NotifiedNoteLocation carouselL;
        private Task generateCarousel;
        public NotifiedNote()
        {
            TestInit();
            empty = GenerateLabel("\n\n\n\n\nNo notes notified\n\n\n\n\n", Color.White, 30, FontAttributes.None, TextAlignment.Center);
            empty.HorizontalOptions = LayoutOptions.FillAndExpand;
            empty.VerticalOptions = LayoutOptions.FillAndExpand;

            Initialization();
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Title = "Notified Note";
            timeNotes = GenerateButton("Time\nNotes",Color.White,yellow);
            locationNotes= GenerateButton("Location\nNotes", Color.White, yellow);
            Grid tabButtons = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
            };
            tabButtons.Children.Add(timeNotes,0,0);
            tabButtons.Children.Add(locationNotes, 1, 0);
            timeNotes.Clicked += OpenTimeTab;
            locationNotes.Clicked += OpenLocationTab;

            GenerateTimeHeading();
            GenerateLocationHeading();
            GenerateSwitch();

            back = GenerateButton("Back",Color.White,Color.Gray);
            back.Clicked += Back;
            Grid buttonList = new Grid
            {
                HorizontalOptions = LayoutOptions.Start,
                Padding = 10,
            };
            buttonList.Children.Add(back, 0, 0);

            tabID = 0;
            currentView = timeNotesView;
            currentHeading = timeHeading;
            notiCurrentSect = notiTimeSect;
            DisableButton(timeNotes);

            content = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                Children =
                {
                    tabButtons,
                    notiCurrentSect,
                    currentHeading,
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
        void OpenTimeTab(object sender, EventArgs ea)
        {
            OpenTimeTab();
        }
        void OpenLocationTab(object sender, EventArgs ea)
        {
            OpenLocationTab();
        }
        void Back(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                Navigation.PopAsync();
                this.ReleaseTapLock();
            }
        }
        void timeNotiToggle(object sender, EventArgs ea)
        {
            AppController.TimeNotification = isNotiTime.IsToggled;
        }
        void locationNotiToggle(object sender, EventArgs ea)
        {
            AppController.LocationNotification = isNotiLocation.IsToggled;
        }
        void OpenTimeCarousel(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                NotifiedView holder = sender as NotifiedView;
                generateCarousel.Wait();
                //carouselT.GotoPage(timeNList.IndexOf(holder));
                carouselT.CurrentPage = carouselT.Children[timeNList.IndexOf(holder)];
                Navigation.PushAsync(carouselT);
                this.ReleaseTapLock();
            }
        }
        void OpenLocationCarousel(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                NotifiedView holder = sender as NotifiedView;
                generateCarousel.Wait();
                carouselL.CurrentPage = carouselL.Children[locationNList.IndexOf(holder)];
                Navigation.PushAsync(carouselL);
                this.ReleaseTapLock();
            }
        }
        void DisableButton(Button b)
        {
            b.IsEnabled = false;
            b.BackgroundColor = DarkYellow;
        }
        void EnableButton(Button b)
        {
            b.IsEnabled = true;
            b.BackgroundColor = yellow;
        }
        void OpenLocationTab()
        {
            RemoveContent();
            notiCurrentSect = notiLocationSect;
            currentHeading = locationHeading;
            currentView = locationNotesView;
            DisableButton(locationNotes);
            EnableButton(timeNotes);
            tabID = 1;
            InsertContent();
        }
        void OpenTimeTab()
        {
            RemoveContent();
            notiCurrentSect = notiTimeSect;
            currentHeading = timeHeading;
            currentView = timeNotesView;
            DisableButton(timeNotes);
            EnableButton(locationNotes);
            tabID = 0;
            InsertContent();
        }
        void RemoveContent()
        {
            content.Children.Remove(notiCurrentSect);
            content.Children.Remove(currentHeading);
            content.Children.Remove(currentView);
        }
        void InsertContent()
        {
            content.Children.Insert(1,notiCurrentSect);
            content.Children.Insert(2,currentHeading);
            content.Children.Insert(3,currentView);
        }
        void Initialization()
        {
            timeNList = new List<NotifiedView>();
            locationNList = new List<NotifiedView>();
            timeNotesView = GenerateSectionStack();
            locationNotesView = GenerateSectionStack();
            timeNotesView.Padding = 10;
            locationNotesView.Padding = 10;
            for (int i=0;i<NoteManager.timeNotes.Count;i++)
            {
                if (DateTime.Now.CompareTo(NoteManager.timeNotes[i].DateTimeSet)>0 && NoteManager.timeNotes[i].isTriggered)
                {
                    timeNList.Add(new NotifiedView(NoteManager.timeNotes[i]));
                    var tgr = new TapGestureRecognizer();
                    tgr.Tapped += (s, e) => OpenTimeCarousel(s, e);
                    timeNList.Last().GestureRecognizers.Add(tgr);
                    timeNotesView.Children.Add(timeNList.Last());
                }
            }
            for (int i = 0; i < NoteManager.locationNotes.Count; i++)
            {
                if (NoteManager.locationNotes[i].isNotified)
                {
                    locationNList.Add(new NotifiedView(NoteManager.locationNotes[i]));
                    var tgr = new TapGestureRecognizer();
                    tgr.Tapped += (s, e) => OpenLocationCarousel(s, e);
                    locationNList.Last().GestureRecognizers.Add(tgr);
                    locationNotesView.Children.Add(locationNList.Last());
                }
            }
            generateCarousel = Task.Run(() =>
            {
                carouselT = new NotifiedNoteTime(timeNList);
                carouselL = new NotifiedNoteLocation(locationNList);
            });
            if (timeNotesView.Children.Count == 0)
                timeNotesView.Children.Add(empty);
            if (locationNotesView.Children.Count == 0)
                locationNotesView.Children.Add(empty);
        }
        void TestInit()
        {
            if (NoteManager.timeNotes.Count != 0)
            {
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.timeNotes.Add(new TimeNote("You disrespect me, you disrespect my family", DateTime.Now));
                NoteManager.timeNotes.Last().isTriggered = true;
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.locationNotes.Add(new LocationNote("Wubba Lubba Dub Dub", LandmarkCollection.landmarks[1], 0.5));
                NoteManager.locationNotes.Last().isTriggered = true;
                NoteManager.locationNotes.Last().isNotified = true;
            }
        }
    }
}
