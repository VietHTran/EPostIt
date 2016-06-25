using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
//using Acr.UserDialogs;

namespace EPostIt
{
    class LandmarkList : ContentPage
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
        private StackLayout wrapperLabel(Label a)
        {
            StackLayout holder = new StackLayout
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Navy,
                Children = { a }
            };
            return holder;
        }
        private Frame wrapperStackLayout(Layout a)
        {
            Frame holder = new Frame
            {
                Content = a,
                Padding = 0,
                BackgroundColor = Color.Navy,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            return holder;
        }
        private Frame wrapperLandmark(Layout a)
        {
            Frame holder = new Frame
            {
                Content = a,
                Padding = 0,
                BackgroundColor = Color.Blue,
            };
            return holder;
        }
        private Frame wrapperObject(View a)
        {
            Frame holder = new Frame
            {
                Content = a,
                Padding = 0,
                BackgroundColor = Color.Blue,
            };
            return holder;
        }
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
        private Grid GenerateInput()
        {
            Grid createNew = new Grid {
                HorizontalOptions=LayoutOptions.FillAndExpand,
                ColumnSpacing=10,
                RowSpacing=10
            };
            Label title = GenerateLabel("Name: ",Color.White,20,FontAttributes.Bold,TextAlignment.Start);
            Entry name = new Entry
            {

            };
            return createNew;
        }
        #region Variables
        private StackLayout content;
        private Picker sortBy;
        private Picker sortType;
        private Button delete;
        private Button deleter;
        private Button selectAll;
        private Button deselectAll;
        private Button back;
        private Button add;
        private Grid buttons;
        private Button reloadDistance;
        //private ScrollView list;
        private LandmarkViewList items;
        private StackLayout landmarkContainer;
        private bool selectMode;
        private int selectDelete;
        private Label empty;
        private Grid createNew;
        private Button cancel;
        private Button save;
        private Entry name;
        #endregion
        public LandmarkList()
        {

            selectMode = true;
            selectDelete = 0;
            Label a = new Label { };
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Grid heading = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Name", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date Assigned", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout noteCountH = wrapperLabel(GenerateLabel("Note set", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout distanceH = wrapperLabel(GenerateLabel("Distance (meters)", Color.White, 25, FontAttributes.None, TextAlignment.Center));

            heading.Children.Add(nameH, 0, 0);
            Grid.SetColumnSpan(nameH, 4);
            heading.Children.Add(dateH, 4, 0);
            Grid.SetColumnSpan(dateH, 3);
            heading.Children.Add(noteCountH, 7, 0);
            Grid.SetColumnSpan(noteCountH, 2);
            heading.Children.Add(distanceH, 9, 0);
            Grid.SetColumnSpan(distanceH, 2);

            Label pageTitle = GenerateLabel("Landmark List", Color.White, 32, FontAttributes.Bold, TextAlignment.Center);
            pageTitle.HorizontalOptions = LayoutOptions.Center;
            pageTitle.VerticalOptions = LayoutOptions.Center;
            Frame pageTitleW = wrapperObject(pageTitle);
            pageTitleW.Padding = 10;

            Label sortByT = GenerateLabel("Sort By: ", Color.White, 25, FontAttributes.Bold, TextAlignment.Start);
            sortByT.HorizontalOptions = LayoutOptions.Center;
            sortBy = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Title = "Sort By",
                Items = { "Name", "Date Created", "Note Set", "Distance" }
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
                Items = { "Ascending", "Descending" }
            };
            Grid sorter = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
            };
            sorter.Children.Add(sortByG, 0, 0);
            Grid.SetColumnSpan(sortByG, 2);
            sorter.Children.Add(sortTypeT, 2, 0);
            sorter.Children.Add(sortType, 3, 0);

            empty = GenerateLabel("\n\n\n\n\nNo landmark registered\n\n\n\n\n", Color.White, 30, FontAttributes.None, TextAlignment.Center);
            empty.HorizontalOptions = LayoutOptions.FillAndExpand;
            empty.VerticalOptions = LayoutOptions.FillAndExpand;

            delete = GenerateButton("Select\nMode", Color.White, Color.Blue);
            add = GenerateButton("Add\nLandmark", Color.White, Color.Blue);
            deleter = GenerateButton("Delete", Color.White, Color.Blue);
            selectAll = GenerateButton("Select\nAll", Color.White, Color.Blue);
            deselectAll = GenerateButton("Deselect\nAll", Color.White, Color.Blue);
            reloadDistance = GenerateButton("Reload", Color.White, Color.Blue);
            back = GenerateButton("Back", Color.White, Color.Gray);
            delete.HorizontalOptions = LayoutOptions.FillAndExpand;
            back.HorizontalOptions = LayoutOptions.FillAndExpand;
            reloadDistance.HorizontalOptions = LayoutOptions.FillAndExpand;
            buttons = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            buttons.Children.Add(back, 0, 0);
            buttons.Children.Add(reloadDistance, 2, 0);
            buttons.Children.Add(delete, 3, 0);
            buttons.Children.Add(add, 1, 0);

            deleter.Clicked += async (sender, ea) => await DeleteItems(sender, ea);
            add.Clicked += AddLandmark;
            selectAll.Clicked += SelectAll;
            deselectAll.Clicked += DeselectAll;
            reloadDistance.Clicked += Reload;
            back.Clicked += Back;
            delete.Clicked += ToggleMode;

            Initialization();

            content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    pageTitleW,
                    sorter,
                    heading,
                    landmarkContainer,
                    buttons
                }
            };

            createNew = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 10,
                RowSpacing = 10,
                Padding=10,
                BackgroundColor=Color.Navy
            };
            Label title = GenerateLabel("Name: ", Color.White, 25, FontAttributes.Bold, TextAlignment.Start);
            name = new Entry { Placeholder = "Landmark Name",FontSize=25, HorizontalOptions=LayoutOptions.FillAndExpand };
            name.TextChanged += OnTexChanged;
            save = GenerateButton("Save",Color.White,Color.Blue);
            cancel = GenerateButton("Cancel",Color.White,Color.Gray);
            createNew.Children.Add(title,0,0);
            createNew.Children.Add(name,1,0);
            createNew.Children.Add(save,4,0);
            createNew.Children.Add(cancel, 5, 0);
            Grid.SetColumnSpan(name, 3);
            cancel.Clicked += CancelLandmark;
            save.Clicked += SaveLandmark;
            /*
            Grid replaceContent = new Grid {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            replaceContent.Children.Add(pageTitleW,0,0);
            replaceContent.Children.Add(sorter, 0, 1);
            replaceContent.Children.Add(heading, 0, 2);
            replaceContent.Children.Add(list, 0, 3);
            replaceContent.Children.Add(buttons, 0, 9);

            Grid.SetRowSpan(heading,1);
            Grid.SetRowSpan(list, 6);
            */
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

            items = new LandmarkViewList();
            landmarkContainer = new StackLayout
            {
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
            };
            if (items.Count == 1)
            {
                landmarkContainer.Children.Add(empty);
                return;
            }
            for (int i = 1; i < items.Count; i++)
            {
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectLandmark(s, e);
                items[i].GestureRecognizers.Add(tgr);
                landmarkContainer.Children.Add(items[i]);
            }
            #region List
            /*
            list = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = landmarkContainer
            };
            */
            #endregion
        }
        void Back(object sender, EventArgs ea)
        {
            Navigation.PopAsync();
        }
        void ActivateDeleteMode()
        {
            buttons.Children.Add(deleter, 3, 1);
            buttons.Children.Add(selectAll, 2, 1);
            buttons.Children.Add(deselectAll, 1, 1);
            deleter.IsEnabled = false;
            deleter.BackgroundColor = Color.Navy;
        }
        void DeactivateDeleteMode()
        {
            buttons.Children.Remove(deleter);
            buttons.Children.Remove(selectAll);
            buttons.Children.Remove(deselectAll);
            deleter.IsEnabled = false;
            deleter.BackgroundColor = Color.Navy;
            TurnAllBlue();
        }
        void SelectAll(object sender, EventArgs ea)
        {
            selectDelete = items.Count - 1;
            if (!deleter.IsEnabled && selectDelete != 0)
            {
                deleter.IsEnabled = true;
                deleter.BackgroundColor = Color.Blue;
            }
            for (int i = 1; i < items.Count; i++)
            {
                items[i].BackgroundColor = Color.Green;
            }
        }
        void DeselectAll(object sender, EventArgs ea)
        {
            if (deleter.IsEnabled)
            {
                deleter.IsEnabled = false;
                deleter.BackgroundColor = Color.Navy;
            }
            TurnAllBlue();
            selectDelete = 0;
        }
        void TurnAllBlue()
        {
            for (int i = 1; i < items.Count; i++)
            {
                items[i].BackgroundColor = Color.Blue;
            }
        }
        void ToggleMode(object sender, EventArgs ea)
        {
            if (selectMode)
            {
                selectMode = false;
                delete.Text = "Delete\nMode";
                delete.BackgroundColor = Color.Navy;
                ActivateDeleteMode();
                DisableButton(add);
            }
            else
            {
                selectMode = true;
                delete.Text = "Select\nMode";
                delete.BackgroundColor = Color.Blue;
                selectDelete = 0;
                DeactivateDeleteMode();
                EnableButton(add);
            }
        }
        void SelectLandmark(object sender, EventArgs ea)
        {
            if (!selectMode)
            {
                Grid holder = sender as Grid;
                if (holder.BackgroundColor == Color.Blue)
                {
                    holder.BackgroundColor = Color.Green;
                    selectDelete++;
                }
                else
                {
                    holder.BackgroundColor = Color.Blue;
                    selectDelete--;
                }
                if (selectDelete > 0 && !deleter.IsEnabled)
                {
                    deleter.IsEnabled = true;
                    deleter.BackgroundColor = Color.Blue;
                }
                else if (selectDelete == 0 && deleter.IsEnabled)
                {
                    deleter.IsEnabled = false;
                    deleter.BackgroundColor = Color.Navy;
                }
            }
            else
            {
                LandmarkView holder = sender as LandmarkView;
                DisplayAlert(holder.landmark.name, $"Coordinates: ({holder.landmark.latitude},{holder.landmark.longitude}).", "OK");
            }
        }
        async Task DeleteItems(object sender, EventArgs ea)
        {
            if (!(await DisplayAlert("Delete Verification", "Do you want to delete the selected landmark(s)", "Yes", "No")))
            {
                return;
            }
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].BackgroundColor == Color.Green)
                {
                    if (items[i].landmark.assignedEvents > 0)
                    {
                        bool pause = await DisplayAlert("Unable to delete landmark", $"There are note(s) assigned to the {items[i].landmark.name} landmark. Please delete all of them/it in the See Note List section.", "Skip This Landmark", "Cancel Delete");
                        if (pause)
                        {
                            items[i].BackgroundColor = Color.Blue;
                            continue;
                        }
                        else
                        {
                            TurnAllBlue();
                            break;
                        }
                    }
                    landmarkContainer.Children.Remove(items[i]);
                    items.DeleteLandmark(items[i]);
                }
            }
            deleter.IsEnabled = false;
            deleter.BackgroundColor = Color.Navy;
            selectDelete = 0;
            if (items.Count == 1)
            {
                landmarkContainer.Children.Add(empty);
            }
        }
        void Reload(object sender, EventArgs ea)
        {
            if (items.Count == 1)
            {
                return;
            }
            (sender as Button).IsEnabled = false;
            /*
            Task.Run(()=>{
                
            });
            */
            for (int i = 0; i < landmarkContainer.Children.Count; i++)
            {
                //((LandmarkView)landmarkContainer.Children[i]).ReCalcDistance();
                landmarkContainer.Children.RemoveAt(i);
                items[i + 1].ReCalcDistance();
                landmarkContainer.Children.Insert(i, items[i + 1]);
            }
            content.Children.RemoveAt(3);
            content.Children.Insert(3, landmarkContainer);
            (sender as Button).IsEnabled = true;
        }
        void AddLandmark(object sender, EventArgs ea)
        {
            if (!landmarkContainer.Children.Contains(createNew))
            {
                landmarkContainer.Children.Add(createNew);
            }
            Button holder = sender as Button;
            DisableButton(holder);
            DisableButton(reloadDistance);
            DisableButton(delete);
        }
        void DisableButton(Button b)
        {
            b.BackgroundColor = Color.Navy;
            b.IsEnabled = false;
        }
        void EnableButton(Button b)
        {
            b.BackgroundColor = Color.Blue;
            b.IsEnabled = true;
        }
        void CancelLandmark(object sender, EventArgs ea)
        {
            landmarkContainer.Children.Remove(createNew);
            name.Text = "";
            EnableButton(add);
            EnableButton(reloadDistance);
            EnableButton(delete);
        }
        void OnTexChanged(object sender, EventArgs ea)
        {
            if (name.Text.Length > 20)
            {
                name.Text = name.Text.Remove(20);
            }
        }
        void SaveLandmark(object sender, EventArgs ea)
        {
            if (!CheckValidity())
            {
                return;
            } else
            {
                LandmarkCollection.CreateLandmark(name.Text,ManagerLocation.latitude,ManagerLocation.longitude);
                landmarkContainer.Children.Remove(createNew);
                items.Add(new LandmarkView(LandmarkCollection.landmarks[LandmarkCollection.landmarks.Count-1]));
                if (items.Count==2)
                {
                    landmarkContainer.Children.Remove(empty);
                }
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectLandmark(s, e);
                items[items.Count - 1].GestureRecognizers.Add(tgr);
                landmarkContainer.Children.Add(items[items.Count-1]);
                DisplayAlert("Added","Location successcully added","OK");
                name.Text = "";
                EnableButton(add);
                EnableButton(reloadDistance);
                EnableButton(delete);
            }
        }
        bool CheckValidity()
        {
            if (name.Text==null)
            {
                DisplayAlert("Empty Input", "Please enter landmark name in the textbox", "OK");
                return false;
            }
            if (name.Text.Equals("None"))
            {
                DisplayAlert("Invalid Name","You are not allowed to use this name. Try something else","OK");
                return false;
            } else if (name.Text.Equals(""))
            {
                DisplayAlert("Empty Input", "Please enter landmark name in the textbox", "OK");
                return false;
            }
            else
            {
                foreach (var i in LandmarkCollection.landmarks)
                {
                    if (name.Text.Equals(i.name))
                    {
                        DisplayAlert("Invalid Name", "The name is already used by another landmark", "OK");
                        return false;
                    }
                }
            }
            return true;    
        }
        void SortByChangeValue(object sender, EventArgs ea)
        {
            
        }
    }
}
