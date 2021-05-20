using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;

namespace MoleWatchApp.Extensions
{
    public class PixelImageHandler
    {
        private SKBitmap resourceBitmap;

        public PixelImageHandler()
        {
        }

        public string getPixelValue(int x, int y)
        {
            string resourceID = "MoleWatchApp.Extensions.ColorMaleFront.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                resourceBitmap = SKBitmap.Decode(stream);
            }

            SKColor color = resourceBitmap.GetPixel(x, y);
            return color.ToString();
        }
    }
}
