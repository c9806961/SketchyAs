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
    public partial class PreviewScreen : ContentPage
    {
        public PreviewScreen()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            SKImageImageSource source = new SKImageImageSource();
            source.Image = App.playerDrawings[App.playerDrawings.Count()-1];
            PreviewImage.Source = source;
            CheckOut.Text = String.Format("Check out what {0} drew", App.playerNames[App.playerNames.Count()-2]);
        }

        public void OnGoClicked(object sender, EventArgs args)
        {
          Navigation.PushModalAsync(new SketchyCanvas());
        }
    }
}