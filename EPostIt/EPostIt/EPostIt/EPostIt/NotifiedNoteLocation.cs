using System.Collections.Generic;
using Xamarin.Forms;

namespace EPostIt
{
    class NotifiedNoteLocation : CarouselPage, ITapLock
    {
        public TapLockVars TapLockVars { get; set; }
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
        private ScrollView wrapperLabel(Label a)
        {
            ScrollView holder = new ScrollView
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = DarkYellow,
                Content = a
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
        private Color DarkYellow = Color.FromHex("B7BC06");
        private Color Yellow = Color.FromHex("DED700");
        public NotifiedNoteLocation(List<NotifiedView> locationNotes)
        {
            for (int i = 0; i < locationNotes.Count; i++)
            {
                var landmark = wrapperLabel(GenerateLabel($"Landmark: {locationNotes[i].landmarkName}", Color.Black, 25, FontAttributes.None, TextAlignment.Start));
                var noteContent = GenerateLabel(locationNotes[i].noteContent, Color.Black, 32, FontAttributes.None, TextAlignment.Center);
                noteContent.BackgroundColor = Yellow;
                landmark.BackgroundColor = DarkYellow;
                landmark.VerticalOptions = LayoutOptions.Center;
                var content = new ContentPage
                {
                    Padding = new Thickness(0, Device.OnPlatform(40, 40, 0), 0, 0),
                    Content = new StackLayout
                    {
                        Children = {
                            landmark,
                            noteContent
                        }
                    }
                };
                this.Children.Add(content);
            }
        }
        public void GotoPage(int index)
        {
            this.CurrentPage = Children[index];
        }
    }
}
