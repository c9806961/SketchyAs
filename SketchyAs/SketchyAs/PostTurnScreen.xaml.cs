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
    public partial class PostTurnScreen : ContentPage
    {
        public PostTurnScreen()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            if (App.playerNames.Count() == 1) // if first player
                EndButton.IsVisible = false;
        }
        public void OnNextClicked(object sender, EventArgs args)
        {
            if (App.playerNames.Count() == 1) // if first player
                App.playerGuesses[0] = Guess.Text;
            else
                App.playerGuesses.Add(Guess.Text);
            Navigation.PushModalAsync(new NameEntry());
        }
        public void OnEndClicked(object sender, EventArgs args)
        {
            App.playerGuesses.Add(Guess.Text);
            Navigation.PushModalAsync(new FinalScreen());
        }
    }
}