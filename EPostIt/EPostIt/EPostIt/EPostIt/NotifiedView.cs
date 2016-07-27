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

        public NotifiedView(TimeNote nT)
        {
            code = 0;
            noteT = nT;
            note = nT;
            CommonGenerate();
        }
        public NotifiedView(LocationNote nL)
        {
            code = 1;
            noteL = nL;
            note = nL;
            CommonGenerate();
        }
        void CommonGenerate()
        {
            Padding = 0;
            HeightRequest = 70;
            BackgroundColor = Color.Yellow;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            GenerateShortText();
            GenerateExtraInfo();

            Children.Add(shortText);
            Grid.SetColumnSpan(shortText,5);
            Children.Add(extraInfo);
            Grid.SetColumnSpan(extraInfo, 3);
        }
        void GenerateShortText()
        {
            shortText = GenerateLabel(note.NoteContent,Color.White,25,FontAttributes.None,TextAlignment.Start);
            shortText.LineBreakMode = LineBreakMode.TailTruncation;
        }
        void GenerateExtraInfo()
        {
            if (code==0)
                extraInfo = GenerateLabel(noteT.DateTimeSet.ToString("MM/dd/yyyy HH:mm"),Color.White,25,FontAttributes.None,TextAlignment.End);
            else
                extraInfo = GenerateLabel(noteL.landmark.name, Color.White, 25, FontAttributes.None, TextAlignment.End);
        }
    }
}
