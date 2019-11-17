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
        public static List<SKImage> playerDrawings;
        public static bool nsfw;

        public static ContentPage lastPage;
        public App()
        {
            InitializeComponent();
            //if lang = lang - Dfault to engligh for now
            Lang = new English();
            // default if nsfw to start - CHNAGE this
            nsfw = true;
            MainPage = new NavigationPage(new MainMenu());
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
