using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MoleWatchApp.Extensions;
using MoleWatchApp.ViewModels;
using SkiaSharp.Views.Forms;
using TouchTracking;
using TouchTracking.Forms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    public partial class PatientModelPage : ContentPage
    {
        private TouchTrackingPoint viewPoint; 

        public PatientModelPage() 
        {
            InitializeComponent();

            PinchAndPanContainer PinAndPan = new PinchAndPanContainer();

            TouchEffect touchEffect = new TouchEffect();
            touchEffect.TouchAction += OnTouchEffectAction;
    
            //PatientCanvasView.Effects.Add(touchEffect);
            PatientModelImage.Effects.Add(touchEffect);
            
        }


        private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    viewPoint = args.Location;
                    
                    PViewModel.Title = "X: " + viewPoint.X.ToString() + ". Y: " + viewPoint.Y.ToString() + "ID: "+ args.Id; ;

                    break;
                    
            }


        }

    }
}