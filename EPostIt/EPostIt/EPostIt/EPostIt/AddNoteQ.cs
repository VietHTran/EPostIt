using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPostIt
{
    class AddNoteQ : ContentPage, ITapLock
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
        private Editor textArea;
        private Picker currentType;
        public AddNoteQ()
        {
            this.Padding = 20;
            textArea = new Editor { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Text = "" };
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
            currentType.SelectedIndex = 0;
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
            cancel.Clicked += async (sender, ea) => await Cancel(sender, ea);
            cancel.BackgroundColor = Color.Gray;
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Children = {quickSwitch,textArea, new StackLayout {
                    Orientation=StackOrientation.Horizontal,
                    HorizontalOptions=LayoutOptions.FillAndExpand,
                    Spacing=15,
                    Children= {cancel,save}
                } }
            };
            if (AppController.isEdit)
            {
                LoadEditContent();
            }
        }
        void LoadEditContent()
        {
            textArea.Text = AppController.Holder.note.NoteContent;
        }
        async Task Save(object sender, EventArgs ea)
        {
            if (this.AcquireTapLock())
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
                    if (AppController.isEdit)
                    {
                        await DisplayAlert("Note Saved", "Note successfully saved.", "OK");
                        int index = NoteManager.quickNotes.IndexOf(AppController.Holder.note);
                        NoteManager.quickNotes[index].NoteContent = textArea.Text;
                        NoteManager.quickNotes[index].dateCreated= DateTime.Now;
                        AppController.Holder.note = NoteManager.quickNotes[index];
                        AppController.Holder.Update();
                        AppController.isEdit = false;
                        AppController.prevPage.IsUpdate = true;
                        await Navigation.PopAsync();
                        return;
                    }
                    else
                    {
                        NoteManager.quickNotes.Add(new Note(textArea.Text));
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
                this.ReleaseTapLock();
            }
        }
        async Task Cancel(object sender, EventArgs ea)
        {
            /*if (this.AcquireTapLock())
            {
             this.ReleaseTapLock();
            }
             */
            if (this.AcquireTapLock())
            {
                if (AppController.isEdit)
                {
                    AppController.isEdit = false;
                    await Navigation.PopAsync();
                    this.ReleaseTapLock();
                    return;
                }
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
                    break;
                case 1:
                    Navigation.PopAsync();
                    Navigation.PushAsync(new AddNoteT());
                    break;
                case 2:
                    if (ManagerLocation.latitude == 0 && ManagerLocation.longitude == 0)
                    {
                        DisplayAlert("Location Unknown", "The app has yet to find the location of the device. Please come back in a few minutes", "OK");
                        currentType.SelectedIndex = 0;
                    }
                    else
                    {
                        textArea.Text = "";
                        Navigation.PopAsync();
                        Navigation.PushAsync(new AddNoteL());
                    }
                    break;
            }
        }
    }
}
