using System.Globalization;
using System.Windows.Media;

namespace ZVS.Global.Extensions
{
    public static class DrawingExtensions
    {
        public static Color GetColorFromString(string colorString)
        {
            byte alpha = byte.Parse(colorString.Substring(0, 2), NumberStyles.HexNumber);
            byte red = byte.Parse(colorString.Substring(2, 2), NumberStyles.HexNumber);
            byte green = byte.Parse(colorString.Substring(4, 2), NumberStyles.HexNumber);
            byte blue = byte.Parse(colorString.Substring(6, 2), NumberStyles.HexNumber);
            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}