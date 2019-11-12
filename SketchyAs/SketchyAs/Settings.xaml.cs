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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void OnExitClicked(object sender, EventArgs args)
        {
            // maybe look at a better way to do this, not sure if it works for IOS
            Navigation.PushAsync(new MainMenu());
        }
    }
}