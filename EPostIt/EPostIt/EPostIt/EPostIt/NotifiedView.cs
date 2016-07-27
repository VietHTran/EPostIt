using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class NotifiedView : Grid
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
        private TimeNote noteT;
        private LocationNote noteL;
        private Note note;
        private Label shortText;
        private Label extraInfo;
        private int code; //time note (0), location note (1)
        private Color yellow = Color.FromHex("DED700");
        public string noteContent { get; set; }
        public string timeTrigger { get; set; }
        public string landmarkName { get; set; }
        public NotifiedView(TimeNote nT)
        {
            code = 0;
            noteT = nT;
            note = nT;
            timeTrigger = noteT.DateTimeSet.ToString("MM/dd/yyyy HH:mm");
            CommonGenerate();
        }
        public NotifiedView(LocationNote nL)
        {
            code = 1;
            noteL = nL;
            note = nL;
            landmarkName = nL.landmark.name;
            CommonGenerate();
        }
        void CommonGenerate()
        {
            Padding = 10;
            HeightRequest = 70;
            BackgroundColor = yellow;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            noteContent = note.NoteContent;
            GenerateShortText();
            GenerateExtraInfo();

            Children.Add(shortText,0,0);
            Grid.SetColumnSpan(shortText,5);
            Children.Add(extraInfo,5,0);
            Grid.SetColumnSpan(extraInfo, 3);
        }
        void GenerateShortText()
        {
            shortText = GenerateLabel(note.NoteContent,Color.White,25,FontAttributes.None,TextAlignment.Center);
            shortText.LineBreakMode = LineBreakMode.TailTruncation;
        }
        void GenerateExtraInfo()
        {
            if (code==0)
                extraInfo = GenerateLabel(timeTrigger,Color.White,25,FontAttributes.None,TextAlignment.Center);
            else
                extraInfo = GenerateLabel(landmarkName, Color.White, 25, FontAttributes.None, TextAlignment.Center);
        }
    }
}
