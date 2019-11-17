using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;
using TouchTracking.Forms;

namespace SketchyAs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SketchyCanvas : ContentPage
    {
        Dictionary<long, SketchyPath> inProgressPaths = new Dictionary<long, SketchyPath>();

        List<SketchyPath> completedPaths = new List<SketchyPath>();

        List<SketchyPath> undonePaths = new List<SketchyPath>();

        SKColor currentColour;

        float currentStrokeWidth;

        bool currentIsEraser;

        SKColor currentBGColour;
        public SketchyCanvas()
        {
            Navigation.RemovePage(App.lastPage);
            App.lastPage = this;
            InitializeComponent();
            currentColour = SKColors.Blue;
            currentStrokeWidth = 10;
            currentIsEraser = false;
            currentBGColour = SKColors.LightGray;
            canvasView.BackgroundColor = SkiaSharp.Views.Forms.Extensions.ToFormsColor(currentBGColour);
        }

        struct SketchyPath
        {
            public SKPath Path;
            public bool IsEraser;
            public SKPaint Paint;

            public SketchyPath(SKPath path, SKPaint paint, bool isEraser)
            {
                Path = path;
                Paint = paint;
                IsEraser = isEraser;
            }
        }
        public void OnClicked(object sender, EventArgs args)
        {
            // change colour
            Button b = (Button)sender;
            if (b.ClassId.Contains("Colour"))
                currentIsEraser = false;
            switch (b.ClassId)
            {
                case "ColourRed":
                    currentColour = SKColors.Red;
                    break;
                case "ColourBlue":
                    currentColour = SKColors.Blue;
                    break;
                case "ColourYellow":
                    currentColour = SKColors.Yellow;
                    break;
                case "ColourBlack":
                    currentColour = SKColors.Black;
                    break;
                case "ColourBrown":
                    currentColour = SKColors.Brown;
                    break;
                case "ColourGreen":
                    currentColour = SKColors.Green;
                    break;
                case "ColourWhite":
                    currentColour = SKColors.White;
                    break;
                case "Eraser":
                    currentColour = currentBGColour;
                    currentIsEraser = true;
                    break;
                case "Undo":
                    if (completedPaths.Count() > 0)
                    {
                        undonePaths.Add(completedPaths[completedPaths.Count - 1]);
                        completedPaths.RemoveAt(completedPaths.Count - 1);
                        canvasView.InvalidateSurface();
                    }
                    break;
                case "Redo":
                    if (undonePaths.Count() > 0)
                    {
                        completedPaths.Add(undonePaths[undonePaths.Count - 1]);
                        undonePaths.RemoveAt(undonePaths.Count - 1);
                        canvasView.InvalidateSurface();
                    }
                    break;
                case "Next":
                    EndTurn();
                    break;
                case "BrushSize":
                    if (currentStrokeWidth < 30)
                        currentStrokeWidth += 10;
                    else
                        currentStrokeWidth = 10;
                    break;
                case "Background":
                    if (currentBGColour == SKColors.LightGray)
                        currentBGColour = SKColors.Gray;
                    else
                        currentBGColour = SKColors.LightGray;
                    foreach (SketchyPath path in completedPaths)
                    {
                        if (path.IsEraser)
                            path.Paint.Color = currentBGColour;
                    }
                    canvasView.BackgroundColor = SkiaSharp.Views.Forms.Extensions.ToFormsColor(currentBGColour);
                    if (currentIsEraser)
                        currentColour = currentBGColour;
                    canvasView.InvalidateSurface();
                    break;
                default:
                    break;

            }
        }

        public void EndTurn()
        {
            // Save the drawing to an Image array for later use
            SKBitmap bitmap = new SKBitmap((int)canvasView.Width, (int)canvasView.Height);
            SKCanvas canvas = new SKCanvas(bitmap);
            canvas.Clear();

            foreach (SketchyPath path in completedPaths)
            {
                canvas.DrawPath(path.Path, path.Paint);
            }

            foreach (SketchyPath path in inProgressPaths.Values)
            {
                canvas.DrawPath(path.Path, path.Paint);
            }
            SKImage image = SKImage.FromBitmap(bitmap);
            App.playerDrawings.Add(image);
            // goto next screen
            Navigation.PushModalAsync(new PostTurnScreen());
        }
        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            undonePaths = new List<SketchyPath>();
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        SKPaint paint = new SKPaint
                        {
                            Style = SKPaintStyle.Stroke,
                            Color = currentColour,
                            StrokeWidth = currentStrokeWidth,
                            StrokeCap = SKStrokeCap.Round,
                            StrokeJoin = SKStrokeJoin.Round
                        };
                        path.MoveTo(ConvertToPixel(args.Location));
                        inProgressPaths.Add(args.Id, new SketchyPath(path,paint, currentIsEraser));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id].Path;
                        path.LineTo(ConvertToPixel(args.Location));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        completedPaths.Add(inProgressPaths[args.Id]);
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Cancelled: // I don't know when this happens
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        SKPoint ConvertToPixel(TouchTrackingPoint pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            foreach (SketchyPath path in completedPaths)
            {
                canvas.DrawPath(path.Path, path.Paint);
            }

            foreach (SketchyPath path in inProgressPaths.Values)
            {
                canvas.DrawPath(path.Path, path.Paint);
            }
        }
    }
}