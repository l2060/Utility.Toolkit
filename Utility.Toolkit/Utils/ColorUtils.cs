using System;
using System.Drawing;

namespace Utility.Toolkit.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ColorUtils
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="htmlColor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Color HtmlColor(this String htmlColor)
        {
            if ((htmlColor[0] == '#') && ((htmlColor.Length == 7) || (htmlColor.Length == 9)))
            {
                var A = 255;
                var R = 0;
                var G = 0;
                var B = 0;
                if (htmlColor.Length == 7)
                {
                    R = (Byte)Convert.ToInt32(htmlColor.Substring(1, 2), 16);
                    G = (Byte)Convert.ToInt32(htmlColor.Substring(3, 2), 16);
                    B = (Byte)Convert.ToInt32(htmlColor.Substring(5, 2), 16);
                }
                else
                {
                    A = (Byte)Convert.ToInt32(htmlColor.Substring(1, 2), 16);
                    R = (Byte)Convert.ToInt32(htmlColor.Substring(3, 2), 16);
                    G = (Byte)Convert.ToInt32(htmlColor.Substring(5, 2), 16);
                    B = (Byte)Convert.ToInt32(htmlColor.Substring(7, 2), 16);
                }
                return Color.FromArgb(A, R, G, B);
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
