using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SketchyAs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreviewScreen : ContentPage
    {
        int currentTime;

        Timer timer;
        public PreviewScreen()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            SKImageImageSource source = new SKImageImageSource();
            source.Image = App.playerDrawings[App.playerDrawings.Count()-1];
            PreviewImage.Source = source;
            CheckOut.Text = String.Format("Check out what {0} drew", App.playerNames[App.playerNames.Count()-2]);
            // now start a timer, maybe they press ready? or maybe we just start it? idk
            currentTime = App.previewTime;
            TimerLabel.Text = currentTime.ToString();
            timer = new Timer(1000);
            timer.Elapsed += CheckTimer;
            timer.Enabled = true;
        }

        void CheckTimer(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                currentTime -= 1;
                TimerLabel.Text = currentTime.ToString();
                if (currentTime == 0)
                {
                    // this might have to be an await call
                    EndPreview();
                }
            });
        }


        public void EndPreview()
        {
            timer.Enabled = false;
            Navigation.PushModalAsync(new SketchyCanvas());
        }
        public void OnGoClicked(object sender, EventArgs args)
        {
            EndPreview();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}