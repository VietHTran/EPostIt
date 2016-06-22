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

        private StackLayout content;
        private Picker sortBy;
        private Picker sortType;
        private Button delete;
        private Button back;
        private Button reloadDistance;
        private ScrollView list;
        private List<LandmarkListItem> items;
        private StackLayout landmarkContainer;
        private bool selectMode;
        private int selectDelete;

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

            Frame nameH = wrapperStackLayout(wrapperLabel(GenerateLabel("Name",Color.White,25,FontAttributes.None,TextAlignment.Center)));
            Frame dateH = wrapperStackLayout(wrapperLabel(GenerateLabel("Date Assigned", Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            Frame noteCountH = wrapperStackLayout(wrapperLabel(GenerateLabel("Note set", Color.White, 25, FontAttributes.None, TextAlignment.Center)));
            Frame distanceH = wrapperStackLayout(wrapperLabel(GenerateLabel("Distance (meters)", Color.White, 25, FontAttributes.None, TextAlignment.Center)));

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
            reloadDistance= GenerateButton("Reload", Color.White, Color.Blue);
            back = GenerateButton("Back", Color.White, Color.Gray);
            back.Clicked += Back;
            delete.HorizontalOptions = LayoutOptions.FillAndExpand;
            delete.Clicked += ToggleMode;
            back.HorizontalOptions = LayoutOptions.FillAndExpand;
            reloadDistance.HorizontalOptions = LayoutOptions.FillAndExpand;
            Grid buttons = new Grid
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
            
            items = new List<LandmarkListItem>();
            landmarkContainer = new StackLayout
            {
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing=10,
            };
            foreach (var i in LandmarkCollection.landmarks)
            {
                if (i.name.Equals("None"))
                {
                    continue;
                }

                items.Add(new LandmarkListItem(i));
                Frame h = items[items.Count - 1].GenerateViewItem();
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => SelectLandmark(s, e);
                h.GestureRecognizers.Add(tgr);
                //h.GestureRecognizers.Add(new TapGestureRecognizer((view) =>SelectLandmark()));
                landmarkContainer.Children.Add(h);
            }

            list = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = landmarkContainer
            };

        }
        void Back(object sender, EventArgs ea)
        {
            Navigation.PopAsync();
        }
        void ToggleMode(object sender, EventArgs ea)
        {
            if (selectMode)
            {
                selectMode = false;
                delete.Text = "Delete\nMode";
                delete.BackgroundColor = Color.Navy;
            } else
            {
                selectMode = true;
                delete.Text = "Select\nMode";
                delete.BackgroundColor = Color.Blue;
                selectDelete = 0;
                //Deselect All Items
            }
        }
        void SelectLandmark(object sender, EventArgs ea)
        {
            if (selectMode)
                return;
            Frame holder = sender as Frame;
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
        }
    }
}
