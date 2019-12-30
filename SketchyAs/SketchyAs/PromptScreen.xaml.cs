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
    public partial class PromptScreen : ContentPage
    {
        public PromptScreen()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            Prompt.Text = App.Lang.getPrompt();
        }

        public void OnPromptClicked(object sender, EventArgs args)
        {
            Prompt.Text = App.Lang.getPrompt();
        }
        public void OnStartClicked(object sender, EventArgs args)
        {
            App.playerGuesses.Add(Prompt.Text);
            Navigation.PushModalAsync(new SketchyCanvas());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}