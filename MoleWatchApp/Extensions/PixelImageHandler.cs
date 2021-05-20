using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using Xamarin.Forms;

namespace MoleWatchApp.Extensions
{
    public class PixelImageHandler
    {
        private SKBitmap resourceBitmap;
        private string resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";

        public PixelImageHandler(ImageSource source)
        {
            switch (source.ToString())
            {
                case "File: MaleFrontCrop.png":
                    resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";
                    break;

                case "File: MaleBackCrop.png":
                    resourceID = "MoleWatchApp.Extensions.ColorMaleBack.png";
                    break;

                case "File: FemaleFrontCrop.png":
                    resourceID = "MoleWatchApp.Extensions.ColorFemaleFront.png";
                    break;

                case "File: FemaleBackCrop.png":
                    resourceID = "MoleWatchApp.Extensions.ColorFemaleBack.png";
                    break;
            }
        }

        public string getPixelValue(int x, int y)
        {
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

            return colorValue;
        }

        private void getBodyPlacement(string color)
        {

        }
    }
}
