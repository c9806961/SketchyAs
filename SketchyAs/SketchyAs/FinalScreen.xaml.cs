using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SketchyAs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalScreen : ContentPage
    {
        public FinalScreen()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();

            List<SKImageImageSource> playerImages = new List<SKImageImageSource>();
            for (int i = 0; i < App.playerDrawings.Count; i++)
            {
                SKImageImageSource source = new SKImageImageSource();
                source.Image = App.playerDrawings[i];
                playerImages.Add(source);
            }

            MyCarousel.ItemsSource = playerImages;
        }
        public void OnExitClicked(object sender, EventArgs args)
        {
            App.lastPage = this;
            Navigation.PushModalAsync(new MainMenu());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void MyCarousel_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            PlayerName.Text = "Artist Name: " + App.playerNames[e.FirstVisibleItemIndex];
            PlayerGuess.Text = "Title: " + App.playerGuesses[e.FirstVisibleItemIndex];
            TimeTaken.Text = "Drawn in " + App.playerTimes[e.FirstVisibleItemIndex] + " second" + (App.playerTimes[e.FirstVisibleItemIndex] == 1 ? "" : "s");
        }
    }
}