using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EPostIt
{
    class AddNoteL : ContentPage, ITapLock
    {
        public TapLockVars TapLockVars
        { get; set; }
        Button ButtonGenerator(String s)
        {
            Button a = new Button { HorizontalOptions = LayoutOptions.FillAndExpand };
            a.Text = s;
            a.BackgroundColor = Color.Red;
            a.FontSize = 32;
            a.FontAttributes = FontAttributes.Bold;
            a.TextColor = Color.White;
            return a;
        }
        private Dictionary<string, int> noteType = new Dictionary<string, int> {
            {"Quick Note",0},
            {"Time-based Note",1},
            {"Location-based Note",2}
        };
        private Picker savedLandmarks;
        private Entry nameNewLandmark;
        private StackLayout saveNewLandmark;
        private StackLayout pageContent;
        private Label setNewLandmarkTitle;
        private bool eventSwitch;
        private Editor textArea;
        private double lat;
        private double lon;
        private Picker rangeP;
        private Picker currentType;
        public AddNoteL()
        {
            this.Padding = 20;
            //Trigger Range
            Label rangeT = new Label{ FontSize = 25, Text = "Trigger Range (meters)", FontAttributes = FontAttributes.Bold, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
            rangeP = new Picker {
                Title="Range",
                Items = {"0.25","0.5","1","2","3","4","5"}
            };
            rangeP.SelectedIndex = 2;
            //Print Saved Landmarks
            savedLandmarks = new Picker {
                Title = "Saved Landmarks  ",
                HorizontalOptions=LayoutOptions.FillAndExpand
            };
            Task.Run(()=> {
                if (LandmarkCollection.landmarks.Count==0)
                {
                    savedLandmarks.IsEnabled = false;
                    savedLandmarks.Title = "No landmark saved";
                } else
                {
                    foreach (var i in LandmarkCollection.landmarks)
                    {
                        savedLandmarks.Items.Add(i.name);
                    }
                    savedLandmarks.SelectedIndex = 0;
                }
            });
            savedLandmarks.SelectedIndexChanged += ChangeLandmarkSetting;
            eventSwitch = true;
            //Save New Landmark
            nameNewLandmark = new Entry { FontSize = 20, Placeholder = "Landmark name", HorizontalOptions = LayoutOptions.FillAndExpand, Text="" };
            nameNewLandmark.TextChanged += OnTexChanged;
            setNewLandmarkTitle = new Label { FontSize = 25, Text = "Set New Landmark", FontAttributes = FontAttributes.Bold, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
            saveNewLandmark = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    setNewLandmarkTitle,
                    nameNewLandmark
                }
            };
            //Load Landmark
            StackLayout loadLandmark = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label { FontSize = 25, Text = "Load Landmark", FontAttributes = FontAttributes.Bold, TextColor = Color.White, VerticalOptions = LayoutOptions.Center },
                    savedLandmarks,rangeT,rangeP
                }
            };
            // Location
            textArea = new Editor { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Text="" };
            textArea.BackgroundColor = Color.Yellow;
            textArea.TextColor = Color.Black;
            textArea.FontSize = 32;
            if (SaveDataSwitcher.isSwitchType)
            {
                Task.Run(() => {
                    textArea.Text = SaveDataSwitcher.savedText;
                    SaveDataSwitcher.isSwitchType = false;
                });
            }
            currentType = new Picker
            {
                Title = "Note Type"
            };
            foreach (var i in noteType.Keys)
            {
                currentType.Items.Add(i);
            }
            currentType.SelectedIndex = 2;
            currentType.SelectedIndexChanged += QuickSwitch;
            StackLayout quickSwitch = new StackLayout
            {
                HorizontalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    new Label { Text="Switch type: ", FontSize=25, FontAttributes=FontAttributes.Bold, TextColor=Color.White},
                    currentType
                }
            };
            //Put into Collection
            Button save = ButtonGenerator("Save");
            save.Clicked += async (sender, ea) => await Save(sender, ea);
            //If have text==>Popup confirm else back to previous page
            Button cancel = ButtonGenerator("Cancel");
            cancel.BackgroundColor = Color.Gray;
            cancel.Clicked += async (sender, ea) => await Cancel(sender, ea);
            pageContent = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Children = {quickSwitch,textArea,loadLandmark,saveNewLandmark,new StackLayout {
                    Orientation=StackOrientation.Horizontal,
                    HorizontalOptions=LayoutOptions.FillAndExpand,
                    Spacing=15,
                    Children= {cancel,save}
                } }
            };
            Content = pageContent;
        }

        void OnTexChanged(object sender, EventArgs ea)
        {
            if (savedLandmarks.SelectedIndex != 0)
            {
                return;
            }
            
            if (nameNewLandmark.Text.Length>20)
            {
                nameNewLandmark.Text=nameNewLandmark.Text.Remove(20);
            }
        }
        void ChangeLandmarkSetting(object sender, EventArgs ea)
        {
            if (savedLandmarks.SelectedIndex==0)
            {
                Task.Run(()=> {
                    lat = ManagerLocation.latitude;
                    lon = ManagerLocation.longitude;
                });
                if (!eventSwitch)
                {
                    nameNewLandmark.TextChanged += OnTexChanged;
                    eventSwitch = true;
                }
                nameNewLandmark.Text = "";
                nameNewLandmark.IsEnabled = true;
                setNewLandmarkTitle.Text = "Set New Landmark";
            } else
            {
                Task.Run(()=> {
                    Landmark landmarkHolder = LandmarkCollection.SearchName(savedLandmarks.Items[savedLandmarks.SelectedIndex]);
                    lat = landmarkHolder.latitude;
                    lon = landmarkHolder.longitude;
                });
                if (eventSwitch)
                {
                    nameNewLandmark.TextChanged -= OnTexChanged;
                    eventSwitch = false;
                }

                nameNewLandmark.Text = savedLandmarks.Items[savedLandmarks.SelectedIndex];
                nameNewLandmark.IsEnabled = false;
                setNewLandmarkTitle.Text = "Use Saved Landmark";
            }
        }
        async Task Save(object sender, EventArgs ea)
        {
            if (textArea.Text == null)
            {
                await DisplayAlert("Empty Text", "Please type in some text in order to save.", "OK");
            }
            else if (textArea.Text.Equals(""))
            {
                await DisplayAlert("Empty Text", "Please type in some text in order to save.", "OK");
            }
            else
            {
                //NoteManager.quickNotes.Add(new Note(textArea.Text));
                if (savedLandmarks.SelectedIndex == 0 && nameNewLandmark.Text.Equals(""))
                {
                    await DisplayAlert("", "Please type in the landmark name.", "OK");
                    return;
                }
                int landmarkIndex= LandmarkCollection.nameList.IndexOf(nameNewLandmark.Text);
                if (savedLandmarks.SelectedIndex==0 && landmarkIndex!=-1)
                {
                    await DisplayAlert("", "Landmark name has already existed.", "OK");
                    return;
                }
                double triggerRadius = Double.Parse(rangeP.Items[rangeP.SelectedIndex]);
                if (savedLandmarks.SelectedIndex==0)
                {
                    LandmarkCollection.CreateLandmark(nameNewLandmark.Text, lat, lon);
                    LandmarkCollection.landmarks[LandmarkCollection.landmarks.Count - 1].AssignEvent();
                } else
                {
                    LandmarkCollection.landmarks[landmarkIndex].AssignEvent();
                }
                NoteManager.locationNotes.Add(new LocationNote(textArea.Text,lat,lon,triggerRadius));
                bool backToMenu = await DisplayAlert("Note Saved", "Note successfully saved.", "Back To Menu", "Create New Note");
                if (backToMenu)
                {
                    textArea.Text = "";
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }
        async Task Cancel(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
            {
                if (textArea.Text == null)
                {
                    await Navigation.PopAsync();
                }
                else if (textArea.Text.Equals(""))
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    bool exitPage = await DisplayAlert("", "You haven't saved this note yet. Do you want to exit?", "Yes", "No");
                    if (exitPage)
                    {
                        await Navigation.PopAsync();
                    }
                }
                this.ReleaseTapLock();
            }
        }
        void QuickSwitch(object sender, EventArgs ea)
        {
            bool isNeedTransfer = true;
            if (textArea.Text == null)
            {
                isNeedTransfer = false;
            }
            else if (textArea.Text.Equals(""))
            {
                isNeedTransfer = false;
            }
            if (isNeedTransfer)
            {
                SaveDataSwitcher.TransferData(textArea.Text);
            }
            switch (currentType.SelectedIndex)
            {
                case 0:
                    Navigation.PopAsync();
                    Navigation.PushAsync(new AddNoteQ());
                    break;
                case 1:
                    Navigation.PopAsync();
                    Navigation.PushAsync(new AddNoteT());
                    break;
                case 2:
                    break;
            }
        }
    }
}
