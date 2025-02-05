



using System.Numerics;

namespace System.Drawing
{
    /// <summary>
    /// 
    /// </summary>
    public static class RectangleExtends
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Rectangle Add(this Rectangle rectangle, Point point)
        {
            return new Rectangle(rectangle.Left + point.X, rectangle.Top + point.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Rectangle Sub(this Rectangle rectangle, Point point)
        {
            return new Rectangle(rectangle.Left - point.X, rectangle.Top - point.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Rectangle Add(this Rectangle rectangle, Vector2 point)
        {
            return new Rectangle(rectangle.Left + (Int32)point.X, rectangle.Top + (Int32)point.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point Add(this Point origin, Point point)
        {
            return new Point(origin.X + point.X, origin.Y + point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point Sub(this Point origin, Point point)
        {
            return new Point(origin.X - point.X, origin.Y - point.Y);
        }
    }
}
