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
                BackgroundColor = DarkGreen,
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
            allNotesView = GenerateSectionStack();
            allNotesView.Padding = 0;
            foreach (var i in items.allNotes)
            {
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectNote(s, e);
                i.GestureRecognizers.Add(tgr);
                allNotesView.Children.Add(i);
            }
            holder.Children.Add(allNotesView);

            return holder;
        }
        private StackLayout GenerateQuickNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Note", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date\nCreated", Color.White, 25, FontAttributes.Bold, TextAlignment.Center));

            heading.Children.Add(nameH,0,0);
            Grid.SetColumnSpan(nameH,8);
            heading.Children.Add(dateH, 8, 0);
            Grid.SetColumnSpan(dateH, 3);

            holder.Children.Add(heading);
            quickNotesView = GenerateSectionStack();
            quickNotesView.Padding = 0;
            foreach (var i in items.quickNotes)
            {
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectNote(s, e);
                i.GestureRecognizers.Add(tgr);
                quickNotesView.Children.Add(i);
                //holder.Children.Add(i);
            }
            holder.Children.Add(quickNotesView);
            return holder;
        }
        private StackLayout GenerateTimeNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
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

            timeNotesView = GenerateSectionStack();
            timeNotesView.Padding = 0;
            holder.Children.Add(heading);
            foreach (var i in items.timeNotes)
            {
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectNote(s, e);
                i.GestureRecognizers.Add(tgr);
                timeNotesView.Children.Add(i);
            }
            holder.Children.Add(timeNotesView);

            return holder;
        }
        private StackLayout GenerateLocationNoteS()
        {
            StackLayout holder = GenerateSectionStack();
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
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

            locationNotesView = GenerateSectionStack();
            locationNotesView.Padding = 0;
            holder.Children.Add(heading);
            foreach (var i in items.locationNotes)
            {
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectNote(s, e);
                i.GestureRecognizers.Add(tgr);
                locationNotesView.Children.Add(i);
            }
            holder.Children.Add(locationNotesView);

            return holder;
        }
        private Button quickNoteS, timeNoteS, locationNoteS, allNoteS, currentButton, back, toggleMode, activate, delete, selectAll,deselectAll;
        private Grid tabButtons, buttonList, sorter;
        private StackLayout content, allNotes, quickNotes, timeNotes, locationNotes, currentContent;
        private StackLayout quickNotesView,timeNotesView,locationNotesView,allNotesView,currentView;
        private int selectMode; //if -1 => viewMode else => selectMode number of selected items
        public int tabID;
        //tabID: All(0), Quick(1), Time(2), Location(3)
        private Picker sortBy, sortType;
        private NoteViewList items;
        private Label empty;
        Color DarkGreen = Color.FromHex("#13470A");
        private bool isUpdate;
        public bool IsUpdate {
            get { return isUpdate; }
            set
            {
                if (isUpdate != value)
                {
                    isUpdate = value;
                    if (isUpdate)
                    {
                        UpdateNote();
                    }
                }
            }
        }
        public SeeNote()
        {
            TestInit();
            Initialization();
            selectMode = -1;
            tabID = 0;
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
            currentView = allNotesView;

            back = GenerateButton("Back", Color.White, Color.Gray);
            toggleMode = GenerateButton("View\nMode", Color.White, Color.Green);
            activate = GenerateButton("Turn\nOn", Color.White, Color.Green);
            delete= GenerateButton("Delete", Color.White, Color.Green);
            selectAll= GenerateButton("Select\nAll", Color.White, Color.Green);
            deselectAll= GenerateButton("Deselect\nAll", Color.White, Color.Green);

            toggleMode.Clicked += ToggleMode;
            selectAll.Clicked += SelectAll;
            deselectAll.Clicked += DeselectAll;
            back.Clicked += Back;
            delete.Clicked += async (sender, ea) => await Delete(sender, ea);

            buttonList = new Grid {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding=10,
            };
            buttonList.Children.Add(back,0,0);
            buttonList.Children.Add(toggleMode, 3, 0);

            empty = GenerateLabel("\n\n\n\n\nEmpty\n\n\n\n\n", Color.White, 30, FontAttributes.None, TextAlignment.Center);
            empty.HorizontalOptions = LayoutOptions.FillAndExpand;
            empty.VerticalOptions = LayoutOptions.FillAndExpand;

            InitPicker();
            sortBy.SelectedIndexChanged += SortByChanged;

            content = new StackLayout
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                Spacing=0,
                Children=
                {
                    tabButtons,
                    sorter,
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
        void Back(object sender, EventArgs ea)
        {
            Navigation.PopAsync();
        }
        void OpenAll(object sender, EventArgs ea)
        {
            OpenAll();
        }
        void OpenAll()
        {
            if (selectMode != -1)
            {
                DeselectAll();
            }
            content.Children.Remove(currentContent);
            currentContent = allNotes;
            currentView = allNotesView;
            content.Children.Insert(2, currentContent);
            currentButton = allNoteS;
            tabID = 0;
            InsertActiveBtn();
            UpdateButton();
            SetPickerAllNote();
        }
        void OpenQuick(object sender, EventArgs ea)
        {
            OpenQuick();
        }
        void OpenQuick()
        {
            if (selectMode!=-1)
            {
                DeselectAll();
            }
            content.Children.Remove(currentContent);
            currentContent = quickNotes;
            currentView = quickNotesView;
            content.Children.Insert(2, currentContent);
            currentButton = quickNoteS;
            tabID = 1;
            InsertActiveBtn();
            UpdateButton();
            SetPickerQuickNote();
        }
        void OpenTime(object sender, EventArgs ea)
        {
            OpenTime();
        }
        void OpenTime()
        {
            if (selectMode != -1)
            {
                DeselectAll();
            }
            content.Children.Remove(currentContent);
            currentContent = timeNotes;
            currentView = timeNotesView;
            content.Children.Insert(2, currentContent);
            currentButton = timeNoteS;
            tabID = 2;
            InsertActiveBtn();
            UpdateButton();
            SetPickerTimeNote();
        }
        void OpenLocation(object sender, EventArgs ea)
        {
            OpenLocation();
        }
        void OpenLocation()
        {
            if (selectMode != -1)
            {
                DeselectAll();
            }
            content.Children.Remove(currentContent);
            currentContent = locationNotes;
            currentView = locationNotesView;
            content.Children.Insert(2, currentContent);
            currentButton = locationNoteS;
            tabID = 3;
            InsertActiveBtn();
            UpdateButton();
            SetPickerLocationNote();
        }
        void ToggleMode(object sender, EventArgs ea)
        {
            if (selectMode==-1)
            {
                selectMode = 0;
                toggleMode.Text = "Select\nMode";
                SelectModeOn();
            } else
            {
                SelectModeOff();
                selectMode = -1;
                toggleMode.Text = "View\nMode";
            }
        }
        async void SelectNote(object sender, EventArgs ea)
        {
            var holder = sender as NoteView;
            if (selectMode==-1)
            {
                await ShowNote(holder);
            }
            else
            {
                if (holder.BackgroundColor==Color.Green)
                {
                    selectMode++;
                    holder.BackgroundColor = Color.Blue;
                    if (!delete.IsEnabled)
                    {
                        EnabelButton(delete);
                        EnabelButton(activate);
                    }
                } else
                {
                    selectMode--;
                    holder.BackgroundColor = Color.Green;
                    if (selectMode==0 && delete.IsEnabled)
                    {
                        DisabelButton(delete);
                        DisabelButton(activate);
                    }
                }
                
            }
        }
        void SelectAll(object sender, EventArgs ea)
        {
            SelectAll();
        }
        void SelectAll()
        {
            if (currentView.Children.Contains(empty))
            {
                return;
            }
            TurnAllBlue();
            selectMode = currentView.Children.Count;
            if (!delete.IsEnabled)
            {
                EnabelButton(delete);
                EnabelButton(activate);
            }
        }
        void DeselectAll(object sender, EventArgs ea)
        {
            DeselectAll();
        }
        void DeselectAll()
        {
            if (currentView.Children.Contains(empty))
            {
                return;
            }
            TurnAllGreen();
            selectMode = 0;
            if (delete.IsEnabled)
            {
                DisabelButton(delete);
                DisabelButton(activate);
            }
        }
        async Task Delete(object sender, EventArgs ea)
        {
            if (!(await DisplayAlert("Delete Verification", "Do you want to delete the selected note(s)", "Yes", "No")))
            {
                return;
            }
            for (int i = currentView.Children.Count - 1; i >= 0; i--)
            {
                if (currentView.Children[i].BackgroundColor==Color.Blue)
                {
                    var e = currentView.Children[i] as NoteView;
                    e.DeleteFromDatabase();
                    switch (tabID)
                    {
                        case 0:
                            allNotesView.Children.Remove(currentView.Children[i]);
                            if (e.Code == 0)
                            {
                                IEnumerable<View> quick = from a in (quickNotesView.Children) where (a as NoteView).note == e.note select a;
                                foreach (var j in quick.ToList())
                                {
                                    quickNotesView.Children.Remove(j);
                                }
                            } else if (e.Code==1) {
                                IEnumerable<View> time = from a in (timeNotesView.Children) where (a as NoteView).noteT == e.noteT select a;
                                foreach (var j in time.ToList())
                                {
                                    timeNotesView.Children.Remove(j);
                                }
                            }
                            else if (e.Code == 2)
                            {
                                IEnumerable<View> location = from a in (locationNotesView.Children) where (a as NoteView).noteL == e.noteL select a;
                                foreach (var j in location.ToList())
                                {
                                    locationNotesView.Children.Remove(j);
                                }
                            }
                            break;
                        case 1:
                            IEnumerable<View> all;
                            quickNotesView.Children.Remove(e);
                            all = from a in (allNotesView.Children) where (a as NoteView).note == e.note select a;
                            foreach (var j in all.ToList())
                            {
                                allNotesView.Children.Remove(j);
                            }
                            break;
                        case 2:
                            timeNotesView.Children.Remove(e);
                            all = from a in (allNotesView.Children) where (a as NoteView).noteT == e.noteT select a;
                            foreach (var j in all.ToList())
                            {
                                allNotesView.Children.Remove(j);
                            }
                            break;
                        case 3:
                            locationNotesView.Children.Remove(e);
                            all = from a in (allNotesView.Children) where (a as NoteView).noteL == e.noteL select a;
                            foreach (var j in all.ToList())
                            {
                                allNotesView.Children.Remove(j);
                            }
                            break;
                    }
                }
            }
            delete.IsEnabled = false;
            delete.BackgroundColor = DarkGreen;
            selectMode = 0;
            CheckEmptyTab(allNotesView);
            CheckEmptyTab(quickNotesView);
            CheckEmptyTab(timeNotesView);
            CheckEmptyTab(locationNotesView);
        }
        void CheckEmptyTab(StackLayout holder)
        {
            if (holder.Children.Count==0)
            {
                holder.Children.Add(empty);
            }
        }
        void SortByChanged(object sender, EventArgs ea)
        {
            if (sortBy.SelectedIndex == -1 || currentView.Children.Contains(empty))
            {
                return;
            }
            if (sortType.SelectedIndex==-1)
            {
                sortType.SelectedIndex = 0;
            }
            List<View> sorter= new List<View>();
            switch (sortBy.Items[sortBy.SelectedIndex])
            {
                case "Date Created":
                    Debug.WriteLine("Here!");
                    sorter = (from i in currentView.Children orderby (i as NoteView).note.dateCreated select i).ToList();
                    break;
                case "Type":
                    sorter = (from i in currentView.Children orderby (i as NoteView).Code select i).ToList();
                    break;
                case "Date Triggered":
                    sorter = (from i in currentView.Children orderby (i as NoteView).noteT.DateTimeSet select i).ToList();
                    break;
                case "Status":
                    sorter = (from i in currentView.Children orderby (i as NoteView).Status select i).ToList();
                    sorter.Reverse();
                    break;
                case "Landmark Name":
                    sorter = (from i in currentView.Children orderby (i as NoteView).noteL.landmark.name select i).ToList();
                    break;
            }
            if (sortType.SelectedIndex == 1)
            {
                sorter.Reverse();
            }
            currentView.Children.Clear();
            foreach (var i in sorter)
            {
                currentView.Children.Add(i as NoteView);
            }
        }
        void SortTypeChanged(object sender, EventArgs ea)
        {
            if (sortType.SelectedIndex == -1 || currentView.Children.Contains(empty))
            {
                return;
            }
            if (sortBy.SelectedIndex == -1)
            {
                sortBy.SelectedIndex = 0;
            }
        }
        void UpdateNote()
        {
            IsUpdate = false;
            
        }
        async Task ShowNote(NoteView holder)
        {
            bool deal;
            switch (holder.Code)
            {
                case 0:
                    deal=await DisplayAlert("", $"Type: Quick Note\nContent: {holder.note.NoteContent}", "OK", "Edit");
                    break;
                case 1:
                    deal = await DisplayAlert("", $"Type: Time-based Note\nTime Created: {holder.note.dateCreated}\nTime Triggered: {holder.noteT.DateTimeSet}\nStatus: {holder.Status}\nContent: {holder.note.NoteContent}", "OK", "Edit");
                    break;
                case 2:
                    deal = await DisplayAlert("", $"Type: Location-based Note\nTime Created: {holder.note.dateCreated}\nLandmark: {holder.noteL.landmark.name}\nDistance: {holder.CalcDistance()}\nTrigger Radius: {holder.noteL.maxDistance} meter(s)\nStatus: {holder.Status}\nContent: {holder.note.NoteContent}", "OK", "Edit");
                    break;
                default:
                    deal = await DisplayAlert("", $"Type: Quick Note\nContent:\n{holder.note.NoteContent}", "OK", "Edit");
                    break;
            }
            if (!deal)
            {
                sortBy.SelectedIndex = -1;
                sortType.SelectedIndex = -1;
                AppController.isEdit = true;
                AppController.Holder = holder;
                AppController.prevPage = this;
                if (tabID==0)
                {
                    if (holder.Code==0)
                    {
                        IEnumerable<View> quick = from e in (quickNotesView.Children) where (e as NoteView).note == AppController.Holder.note select e;
                        foreach (var i in quick)
                        {
                            AppController.Holder1 = i as NoteView;
                        }
                    }
                    else if (holder.Code == 1)
                    {
                        IEnumerable<View> time = from e in (timeNotesView.Children) where (e as NoteView).note == AppController.Holder.note select e;
                        foreach (var i in time)
                        {
                            AppController.Holder1 = i as NoteView;
                        }
                    }
                    else if (holder.Code == 2)
                    {
                        IEnumerable<View> location = from e in (locationNotesView.Children) where (e as NoteView).note == AppController.Holder.note select e;
                        foreach (var i in location)
                        {
                            AppController.Holder1 = i as NoteView;
                        }
                    }
                } else
                {
                    IEnumerable<View> all = from e in (allNotesView.Children) where (e as NoteView).note == AppController.Holder.note select e;
                    foreach (var i in all)
                    {
                        AppController.Holder1 = i as NoteView;
                    }
                }
                if (holder.Code==0)
                {
                    await Navigation.PushAsync(new AddNoteQ());
                } else if (holder.Code==1)
                {
                    await Navigation.PushAsync(new AddNoteT());
                }
                else if (holder.Code == 2)
                {
                    await Navigation.PushAsync(new AddNoteL());
                }
            }
        }
        void Initialization ()
        {
            items = new NoteViewList();
        }
        void InitPicker()
        {
            Label sortByT = GenerateLabel("Sort By: ",Color.White,25,FontAttributes.Bold, TextAlignment.Start);
            sortByT.HorizontalOptions = LayoutOptions.Center;
            sortBy = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions= LayoutOptions.FillAndExpand,
                Title="Sort By"
            };
            StackLayout sortByG = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { sortByT, sortBy }
            };

            Label sortTypeT = GenerateLabel("Sort Type: ", Color.White, 25, FontAttributes.Bold, TextAlignment.End);
            sortType = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Title = "Sort Type",
                Items=
                {
                    "Ascending",
                    "Descending"
                }
            };
            SetPickerAllNote();
            sorter = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
            };
            sorter.Children.Add(sortByG, 0, 0);
            Grid.SetColumnSpan(sortByG, 2);
            sorter.Children.Add(sortTypeT, 2, 0);
            sorter.Children.Add(sortType, 3, 0);
        }
        void SetPickerAllNote()
        {
            sortBy.Items.Clear();
            sortBy.Items.Add("Date Created");
            sortBy.Items.Add("Type");
            sortBy.SelectedIndex = -1;
            sortType.SelectedIndex = -1;
        }
        void SetPickerQuickNote()
        {
            sortBy.Items.Clear();
            sortBy.Items.Add("Date Created");
            sortBy.SelectedIndex = -1;
            sortType.SelectedIndex = -1;
        }
        void SetPickerTimeNote()
        {
            sortBy.Items.Clear();
            sortBy.Items.Add("Date Created");
            sortBy.Items.Add("Date Triggered");
            sortBy.Items.Add("Status");
            sortBy.SelectedIndex = -1;
            sortType.SelectedIndex = -1;
        }
        void SetPickerLocationNote()
        {
            sortBy.Items.Clear();
            sortBy.Items.Add("Date Created");
            sortBy.Items.Add("Landmark Name");
            sortBy.Items.Add("Status");
            sortBy.SelectedIndex = -1;
            sortType.SelectedIndex = -1;
        }
        void DisabelButton(Button b)
        {
            b.BackgroundColor = DarkGreen;
            b.IsEnabled = false;
        }
        void EnabelButton(Button b)
        {
            b.BackgroundColor = Color.Green;
            b.IsEnabled = true;
        }
        void SelectModeOn()
        {
            DisabelButton(delete);
            DisabelButton(activate);
            buttonList.Children.Add(selectAll, 3, 1);
            buttonList.Children.Add(deselectAll, 2, 1);
            buttonList.Children.Add(delete, 2, 0);
            InsertActiveBtn();
        }
        void SelectModeOff()
        {
            TurnAllGreen();
            buttonList.Children.Remove(selectAll);
            buttonList.Children.Remove(deselectAll);
            buttonList.Children.Remove(delete);
            buttonList.Children.Remove(activate);
        }
        void TurnAllGreen() //Deselect All
        {
            if (currentView.Children.Contains(empty))
            {
                return;
            }
            for (int i = 0; i < currentView.Children.Count; i++)
            {
                currentView.Children[i].BackgroundColor = Color.Green;
            }
        }
        void TurnAllBlue() //Select All
        {
            if (currentView.Children.Contains(empty))
            {
                return;
            }
            for (int i = 0; i < currentView.Children.Count; i++)
            {
                currentView.Children[i].BackgroundColor = Color.Blue;
            }
        }
        void InsertActiveBtn()
        {
            if (selectMode!=-1 && (tabID==2 || tabID==3))
            {
                buttonList.Children.Add(activate, 1, 0);
            } else if (buttonList.Children.Contains(activate))
            {
                buttonList.Children.Remove(activate);
            }
        }
        void TestInit()
        {
            if (NoteManager.quickNotes.Count != 0)
            {
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.quickNotes.Add(new Note("Go lorem ipsum yourself"));
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.timeNotes.Add(new TimeNote("You disrespect me, you disrespect my family", DateTime.Now));
            }
            for (int i = 0; i < 5; i++)
            {
                NoteManager.locationNotes.Add(new LocationNote("Wubba Lubba Dub Dub", LandmarkCollection.landmarks[2], 0.5));
            }
        }
    }
}
