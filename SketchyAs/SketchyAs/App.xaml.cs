using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SketchyAs
{
    public partial class App : Application
    {
        public static SketchyLang Lang;
        public App()
        {
            InitializeComponent();
            //if lang = lang - Dfault to engligh for now
            Lang = new English();
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

    public interface ICloseApplication
    {
        void closeApplication();
    }
}
