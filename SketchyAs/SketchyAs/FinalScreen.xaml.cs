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
        }
        public void OnExitClicked(object sender, EventArgs args)
        {
            App.lastPage = this;
            Navigation.PushModalAsync(new MainMenu());
        }
    }
}