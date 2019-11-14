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
            InitializeComponent();
        }

        public void OnQuitClicked(object sender, EventArgs args)
        {
            // maybe look at a better way to do this, not sure if it works for IOS
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void OnPlayClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new SketchyCanvas());
        }

        public void OnSettingsClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Settings());
        }
    }

}
