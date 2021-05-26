﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoleWatchApp.Extensions
{
    /// <summary>
    /// Klasse til at styre zoom og pan på patientmodellen
    /// Inspiration til denne kode fundet på https://forums.xamarin.com/discussion/77726/how-to-mix-the-pinch-and-pan-gesture-for-image-control-together
    /// </summary>

    public class PinchAndPanContainer : ContentView
    {
        /// <summary>
        /// Den nuværende skaleringsfaktor
        /// </summary>
        double currentScale = 1;

        /// <summary>
        /// Skaleringsfaktoren på billedet ved opstart
        /// </summary>
        double startScale = 1;

        /// <summary>
        /// Mængden af x-koordinat som patientmodellen er blevet rykket fra (0,0)
        /// </summary>
        double xOffset = 0;

        /// <summary>
        /// Mængden af y-koordinat som patientmodellen er blevet rykket fra (0,0)
        /// </summary>
        double yOffset = 0;

        /// <summary>
        /// Tidspunktet hvor det sidste pinch-event blev færdiggjort
        /// </summary>
        private DateTime lastPinchDateTime;

        /// <summary>
        /// Tiden mellem det sidste pinch-event blev færdigt og nu, for at indsætte et delay.
        /// </summary>
        private TimeSpan TimeSinceLastPinch;


        /// <summary>
        /// Skærm bredden i pixels
        /// </summary>
        private double ScreenWidth;

        /// <summary>
        /// Skærm højden i pixels
        /// </summary>
        private double ScreenHeight;


        /// <summary>
        /// Default constructor, som bliver brugt i XAML-koden.
        /// </summary>
        public PinchAndPanContainer()
        {
            ScreenWidth = Application.Current.MainPage.Width;
            ScreenHeight = Application.Current.MainPage.Height;

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            lastPinchDateTime = DateTime.Now;
        }

        /// <summary>
        /// Metoden retunere positionen af den placerede pin 
        /// </summary>
        /// <param name="Xpin"></param>
        /// <param name="Ypin"></param>
        /// <returns></returns>
        public int[] getPinPlacement(double Xpin, double Ypin)
        {
            double TrueXPosition;
            double TrueYPosition;


            TrueXPosition = (Xpin - xOffset) / currentScale;
            TrueYPosition = (Ypin - yOffset) / currentScale;

            int[] dataList = new int[2] { Convert.ToInt32(TrueXPosition), Convert.ToInt32(TrueYPosition) };

                
            return dataList;
        }


        /// <summary>
        /// Metoden opdatere billedet når der bruges pinch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }

            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor.
                Content.Scale = currentScale;
            }

            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;

                lastPinchDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Metoden opdatere billedet når der bruges pan 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {

            if (ScreenHeight == -1)
            {
               ScreenHeight = Application.Current.MainPage.Height;
            }
            if (ScreenWidth == -1)
            {
                ScreenWidth = Application.Current.MainPage.Width;
            }


            TimeSinceLastPinch = DateTime.Now - lastPinchDateTime;

            if (TimeSinceLastPinch.Milliseconds > 250)
            {
                
                switch (e.StatusType)
            {
                case GestureStatus.Running:
                    double newX = (e.TotalX * Scale) + xOffset;
                    double newY = (e.TotalY * Scale) + yOffset;

                    double width = (Content.Width * Content.Scale);
                    double height = (Content.Height * Content.Scale);

                    bool canMoveX = width > ScreenWidth;
                    bool canMoveY = height >  ScreenHeight;

                    if (canMoveX)
                    {
                        double minX = (width - (ScreenWidth / 2)) * -1 - width * 0.1;
                        double maxX = Math.Min(ScreenWidth / 2, width / 2);

                        if (newX < minX)
                        {
                            newX = minX;
                        }

                        if (newX > maxX)
                        {
                            newX = maxX;
                        }
                    }
                    else
                    {
                        newX = 0;
                    }

                    if (canMoveY)
                    {
                        double minY = (height - (ScreenHeight / 2)) * -1 - height * 0.1;
                        double maxY = Math.Min(ScreenHeight / 2, height / 2);

                        if (newY < minY)
                        {
                            newY = minY;
                        }

                        if (newY > maxY)
                        {
                            newY = maxY;
                        }
                    }
                    else
                    {
                        newY = 0;
                    }

                    Content.TranslationX = newX;
                    Content.TranslationY = newY;
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                    break;
            }

            }
        }
    }
}

