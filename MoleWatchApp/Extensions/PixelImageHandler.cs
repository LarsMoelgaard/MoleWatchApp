using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using SkiaSharp;
using Xamarin.Forms;

namespace MoleWatchApp.Extensions
{
    /// <summary>
    /// Klasse til at identificere en kropsdel på patienten ud fra et koordinat 
    /// </summary>
    public class PixelImageHandler
    {
        private SKBitmap resourceBitmap;
        private string resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";
        private bool isFrontFacing = true;

        public PixelImageHandler(ImageSource source)
        {
            switch (source.ToString())
            {
                case "File: MaleFrontCrop.png":
                    isFrontFacing = true;
                    resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";
                    break;

                case "File: MaleBackCrop.png":
                    isFrontFacing = false;
                    resourceID = "MoleWatchApp.Extensions.ColorMaleBack.png";
                    break;

                case "File: FemaleFrontCrop.png":
                    isFrontFacing = true;
                    resourceID = "MoleWatchApp.Extensions.ColorFemaleFront.png";
                    break;

                case "File: FemaleBackCrop.png":
                    isFrontFacing = false;
                    resourceID = "MoleWatchApp.Extensions.ColorFemaleBack.png";
                    break;
            }
        }


        /// <summary>
        /// Metoden finder en farve på en kropdel på et farvelagt billede af patienten og ud fra farven retunere metoden den kropsdel som koordinatet er sat på 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string getPixelValue(int x, int y)
        {
            string resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                resourceBitmap = SKBitmap.Decode(stream);
            }

            double RelativeXCoordinate = Convert.ToDouble(x) / 10000 *
                                         resourceBitmap.Width;
            double RelativeYCoordinate = Convert.ToDouble(y) / 10000 *
                                         resourceBitmap.Height;

            SKColor color = resourceBitmap.GetPixel(Convert.ToInt32(RelativeXCoordinate), Convert.ToInt32(RelativeYCoordinate));
            var colorValue = color.ToString();

            colorValue = colorValue.Remove(0, 3);

            return getBodyPlacement(colorValue);
        }

        private string getBodyPlacement(string color)
        {
            string bodyPart = "";

            switch (color)
            {
                case "1302fa":
                    if (isFrontFacing)
                    {
                        bodyPart = "Højre hånd";
                    }
                    else
                    {
                        bodyPart = "Venstre hånd";
                    }
                    break;

                case "02f3fa":
                    if (isFrontFacing)
                    {
                        bodyPart = "Højre overarm";
                    }
                    else
                    {
                        bodyPart = "Venstre overarm";
                    }
                    break;

                case "03f94a":
                    if (isFrontFacing)
                    {
                        bodyPart = "Højre ben";
                    }
                    else
                    {
                        bodyPart = "Venstre ben";
                    }
                    break;

                case "ef03f9":
                    if (isFrontFacing)
                    {
                        bodyPart = "Venstre hånd";
                    }
                    else
                    {
                        bodyPart = "Højre hånd";
                    }
                    break;

                case "6a0c4f":
                    if (isFrontFacing)
                    {
                        bodyPart = "Venstre overarm";
                    }
                    else
                    {
                        bodyPart = "Højre overarm";
                    }
                    break;

                case "f9c803":
                    if (isFrontFacing)
                    {
                        bodyPart = "Venstre ben";
                    }
                    else
                    {
                        bodyPart = "Højre ben";
                    }
                    break;

                case "fa0202":
                    bodyPart = "Overkrop";
                    break;

                case "245122":
                    bodyPart = "Hoved";
                    break;

                case "000000":
                    bodyPart = "Uden for krop";
                    break;

                default: bodyPart = "Ukendt kropsdel";
                    break;
            }

            return bodyPart;
        }
    }
}
