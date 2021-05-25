using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.Extensions
{
    /// <summary>
    /// Klasse til at hjælpe zoom og pan på patientmodellen
    /// //Inspiration til denne kode fundet på https://forums.xamarin.com/discussion/77726/how-to-mix-the-pinch-and-pan-gesture-for-image-control-together
    /// </summary>
    public static class DoubleExtensions
    {
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }
}
