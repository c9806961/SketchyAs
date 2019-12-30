using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using SkiaSharp;

namespace SketchyAs
{
    public partial class App : Application
    {
        public static SketchyLang Lang;
        public static List<string> playerNames;
        public static List<string> playerGuesses;
        public static List<string> langPacks;
        public static List<SKImage> playerDrawings;
        public static List<int> playerTimes;
        public static int drawTime;
        public static int previewTime;
        public static int panicTime;
        public static int maxPlayers; // must be >= 2

        public static ContentPage lastPage;
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "CollectionView_Experimental"
            });
            //default to NSFW and regular word for the time being until settings implemented
            langPacks = new List<string>()
            { 
                "words",
                "nsfw" 
            };
            //if lang = lang - Dfault to engligh for now
            Lang = new English(langPacks);
            // default if nsfw and times to start - CHNAGE this
            drawTime = 60;
            previewTime = 5;
            MainPage = new NavigationPage(new MainMenu());
            maxPlayers = 10;
            panicTime = 10;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
