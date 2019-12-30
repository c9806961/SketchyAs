using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SketchyAs
{
    [DesignTimeVisible(false)]
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
        }

        public void OnQuitClicked(object sender, EventArgs args)
        {
            // maybe look at a better way to do this, not sure if it works for IOS
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void OnPlayClicked(object sender, EventArgs args)
        {
            App.playerNames = new List<string>();
            App.playerGuesses = new List<string>();
            App.playerDrawings = new List<SKImage>();
            App.playerTimes = new List<int>();
            Navigation.PushModalAsync(new NameEntry());
        }

        public void OnSettingsClicked(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new Settings());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }

}
