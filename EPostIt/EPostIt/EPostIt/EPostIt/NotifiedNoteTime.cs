using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class NotifiedNoteTime : CarouselPage, ITapLock
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
        private Label GenerateLabel(Color colorText, int size, FontAttributes attr, TextAlignment al)
        {
            return new Label
            {
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
        public NotifiedNoteTime(List<NotifiedView> timeNotes)
        {
            ItemTemplate = new DataTemplate(()=>{
                var timeNotify = wrapperLabel(GenerateLabel(Color.Black, 25, FontAttributes.None, TextAlignment.Start));
                var noteContent = GenerateLabel(Color.Black,32,FontAttributes.None,TextAlignment.Center);
                noteContent.BackgroundColor = Yellow;
                timeNotify.VerticalOptions = LayoutOptions.Center;
                noteContent.SetBinding(Label.TextProperty,"noteContent");
                timeNotify.SetBinding(Label.TextProperty, "timeTrigger");
                return new ContentPage
                {
                    Padding = new Thickness(0, Device.OnPlatform(40, 40, 0), 0, 0),
                    Content = new StackLayout
                    {
                        Children = {
                            timeNotify,
                            noteContent
                        }
                    }
                };
            });
            ItemsSource = timeNotes;
        }
        public void GotoPage(int index)
        {
            this.CurrentPage = Children[index];
        }
    }
}
