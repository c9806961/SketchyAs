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
    public partial class NameEntry : ContentPage
    {
        public NameEntry()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            PlayerName.Text = string.Format("Player {0}", App.playerNames.Count + 1);
        }

        public void OnStartClicked(object sender, EventArgs args)
        {
            App.playerNames.Add(PlayerName.Text);
            if (App.playerNames.Count == 1) // first player get a prompt
              Navigation.PushModalAsync(new PromptScreen());
            else // show the last players drawing
              Navigation.PushModalAsync(new PreviewScreen());
        }
    }
}