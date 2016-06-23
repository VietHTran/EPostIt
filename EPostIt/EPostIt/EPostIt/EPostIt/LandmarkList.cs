using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class LandmarkList : ContentPage
    {
        private Label GenerateLabel(string text,Color colorText, int size, FontAttributes attr, TextAlignment al)
        {
            return new Label {
                Text = text,
                FontSize = size,
                TextColor = colorText,
                FontAttributes = attr,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = al,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
            };
        }
        private StackLayout wrapperLabel(Label a)
        {
            StackLayout holder = new StackLayout
            {
                Padding=10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor=Color.Navy,
                Children = {a}
            };
            return holder;
        }
        private Frame wrapperStackLayout(Layout a)
        {
            Frame holder = new Frame {
                Content = a,
                Padding = 0,
                BackgroundColor=Color.Navy,
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
            Button holder= new Button {
                Text=text,
                TextColor=textColor,
                BackgroundColor=backgroundColor,
                FontAttributes=FontAttributes.Bold,
                FontSize=25
            };
            return holder;
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
        private Grid buttons;
        private Button reloadDistance;
        //private ScrollView list;
        private LandmarkViewList items;
        private StackLayout landmarkContainer;
        private bool selectMode;
        private int selectDelete;
        #endregion
        public LandmarkList()
        {
            selectMode = true;
            selectDelete = 0;
            Label a = new Label { };
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            LandmarkListItem test = new LandmarkListItem(LandmarkCollection.landmarks[1]);
            Grid heading = new Grid {
                HorizontalOptions=LayoutOptions.FillAndExpand,
            };

            StackLayout nameH = wrapperLabel(GenerateLabel("Name",Color.White,25,FontAttributes.None,TextAlignment.Center));
            StackLayout dateH = wrapperLabel(GenerateLabel("Date Assigned", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout noteCountH = wrapperLabel(GenerateLabel("Note set", Color.White, 25, FontAttributes.None, TextAlignment.Center));
            StackLayout distanceH = wrapperLabel(GenerateLabel("Distance (meters)", Color.White, 25, FontAttributes.None, TextAlignment.Center));

            heading.Children.Add(nameH,0,0);
            Grid.SetColumnSpan(nameH,4);
            heading.Children.Add(dateH,4,0);
            Grid.SetColumnSpan(dateH, 3);
            heading.Children.Add(noteCountH, 7, 0);
            Grid.SetColumnSpan(noteCountH, 2);
            heading.Children.Add(distanceH, 9, 0);
            Grid.SetColumnSpan(distanceH, 2);

            Label pageTitle= GenerateLabel("Landmark List",Color.White,32,FontAttributes.Bold,TextAlignment.Center);
            pageTitle.HorizontalOptions = LayoutOptions.Center;
            pageTitle.VerticalOptions = LayoutOptions.Center;
            Frame pageTitleW = wrapperObject(pageTitle);
            pageTitleW.Padding = 10;

            Label sortByT = GenerateLabel("Sort By: ",Color.White,25,FontAttributes.Bold,TextAlignment.Start);
            sortByT.HorizontalOptions = LayoutOptions.Center;
            sortBy = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Title = "Sort By",
                Items = {"Name","Date Created","Note Set","Distance"}
            };
            StackLayout sortByG = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation=StackOrientation.Horizontal,
                Children = {sortByT,sortBy}
            };
            Label sortTypeT = GenerateLabel("Sort Type: ", Color.White, 25, FontAttributes.Bold, TextAlignment.End);
            sortType = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Title = "Sort Type",
                Items = {"Ascending","Descending"}
            };
            Grid sorter = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding=10,
            };
            sorter.Children.Add(sortByG,0,0);
            Grid.SetColumnSpan(sortByG, 2);
            sorter.Children.Add(sortTypeT, 2, 0);
            sorter.Children.Add(sortType, 3, 0);

            delete = GenerateButton("Select\nMode",Color.White,Color.Blue);
            deleter = GenerateButton("Delete", Color.White, Color.Blue);
            //deleter.Clicked += 
            selectAll= GenerateButton("Select\nAll", Color.White, Color.Blue);
            selectAll.Clicked += SelectAll;
            deselectAll = GenerateButton("Deselect\nAll", Color.White, Color.Blue);
            deselectAll.Clicked += DeselectAll;
            reloadDistance = GenerateButton("Reload", Color.White, Color.Blue);
            back = GenerateButton("Back", Color.White, Color.Gray);
            back.Clicked += Back;
            delete.HorizontalOptions = LayoutOptions.FillAndExpand;
            delete.Clicked += ToggleMode;
            back.HorizontalOptions = LayoutOptions.FillAndExpand;
            reloadDistance.HorizontalOptions = LayoutOptions.FillAndExpand;
            buttons = new Grid
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,
            };
            buttons.Children.Add(back,0,0);
            buttons.Children.Add(reloadDistance, 2, 0);
            buttons.Children.Add(delete, 3, 0);

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
                Spacing=10,
            };
            for (int i=1;i<items.Count;i++)
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
            buttons.Children.Add(deleter, 3, 2);
            buttons.Children.Add(selectAll, 3, 1);
            buttons.Children.Add(deselectAll, 2, 1);
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
            if (!deleter.IsEnabled)
            {
                deleter.IsEnabled = true;
                deleter.BackgroundColor = Color.Blue;
            }
            for (int i = 1; i < items.Count; i++)
            {
                items[i].BackgroundColor = Color.Green;
            }
            selectDelete = items.Count - 1;
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
            } else
            {
                selectMode = true;
                delete.Text = "Select\nMode";
                delete.BackgroundColor = Color.Blue;
                selectDelete = 0;
                DeactivateDeleteMode();
            }
        }
        void SelectLandmark(object sender, EventArgs ea)
        {
            if (selectMode)
                return;
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
            if (selectDelete>0 && !deleter.IsEnabled)
            {
                deleter.IsEnabled = true;
                deleter.BackgroundColor = Color.Blue;
            } else if (selectDelete==0 && deleter.IsEnabled)
            {
                deleter.IsEnabled = false;
                deleter.BackgroundColor = Color.Navy;
            }
        }
        async Task DeleteItems(object sender, EventArgs ea)
        {
            for (int i=1;i<items.Count;i++)
            {
                if (items[i].BackgroundColor==Color.Green)
                {
                    items.DeleteLandmark(items[i]);
                }
            }
        }
    }
}
