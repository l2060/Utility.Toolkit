using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Utility.Toolkit.Enums;

namespace System
{
    public static class BaseTypeExtensions
    {

        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetDescription(this Enum obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }
            return descAttr.Description;
        }


        public static Int64 ToInt64(this Point p)
        {
            Int64 Value = p.X;
            return (Value << 32) | (Int64)p.Y;
        }

        public static Point ToPoint(this Int64 Value64)
        {
            int X = (Int32)(Value64 >> 32);
            int Y = (Int32)(((Int64)(Value64 >> 32 << 32)) | Value64);
            return new Point(X, Y);
        }


        /// <summary>
        /// 测量 两点之间距离
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static Int32 Distance(this Point point1, Point point2)
        {
            return (Int32)Math.Sqrt(Math.Pow(Math.Abs(point1.X - point2.X), 2) + Math.Pow(Math.Abs(point1.Y - point2.Y), 2));
        }




        /// <summary>
        /// 获取4方向
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Direction8 Direction8(this Point start, Point end)
        {
            var Rad = Math.PI / 180;
            Double a = 0;
            Double c = 0;
            var Value = new PointF(end.X - start.X, end.Y - start.Y);
            if (Value.X == 0 && Value.Y == 0)
            {
                return Utility.Toolkit.Enums.Direction8.DIR_UP;
            }
            if (Math.Abs(Value.X) < Math.Abs(Value.Y))
            {
                a = Math.PI / 2 - Math.Atan(Value.X / Value.Y);
                if (Value.Y < 0)
                    a -= Math.PI;
            }
            else
            {
                a = Math.Atan(Value.Y / Value.X);
                if (Value.X < 0)
                {
                    if (Value.Y >= 0)
                        a += Math.PI;
                    else
                        a -= Math.PI;
                }
            }
            c = a / Rad - 22.5;
            if (c < 0)
                c += 360;
            var n = (Int32)(c / 45) - 5;
            if (n < 0)
                n = n + 8;
            return (Direction8)n;
        }

        /// <summary>
        /// 下一个坐标点
        /// </summary>
        /// <param name="source">源坐标</param>
        /// <param name="dir">方向</param>
        /// <param name="step">步进</param>
        /// <returns></returns>
        public static Point NextPoint(this Point source, Direction8 dir, Int32 step)
        {
            switch (dir)
            {
                case Utility.Toolkit.Enums.Direction8.DIR_UP:
                    return new Point(source.X, source.Y - step);
                case Utility.Toolkit.Enums.Direction8.DIR_UPRIGHT:
                    return new Point(source.X + step, source.Y - step);
                case Utility.Toolkit.Enums.Direction8.DIR_RIGHT:
                    return new Point(source.X + step, source.Y);
                case Utility.Toolkit.Enums.Direction8.DIR_DOWNRIGHT:
                    return new Point(source.X + step, source.Y + step);
                case Utility.Toolkit.Enums.Direction8.DIR_DOWN:
                    return new Point(source.X, source.Y + step);
                case Utility.Toolkit.Enums.Direction8.DIR_DOWNLEFT:
                    return new Point(source.X - step, source.Y + step);
                case Utility.Toolkit.Enums.Direction8.DIR_LEFT:
                    return new Point(source.X - step, source.Y);
                case Utility.Toolkit.Enums.Direction8.DIR_UPLEFT:
                    return new Point(source.X - step, source.Y - step);
                default:
                    break;
            }
            return source;
        }


        private static readonly Direction8[][] DIRECTION8_DEFINES = [
            [Utility.Toolkit.Enums.Direction8.DIR_UPLEFT, Utility.Toolkit.Enums.Direction8.DIR_UPRIGHT],
            [Utility.Toolkit.Enums.Direction8.DIR_UP, Utility.Toolkit.Enums.Direction8.DIR_RIGHT],
            [Utility.Toolkit.Enums.Direction8.DIR_UPRIGHT, Utility.Toolkit.Enums.Direction8.DIR_DOWNRIGHT],
            [Utility.Toolkit.Enums.Direction8.DIR_RIGHT, Utility.Toolkit.Enums.Direction8.DIR_DOWN],
            [Utility.Toolkit.Enums.Direction8.DIR_DOWNRIGHT, Utility.Toolkit.Enums.Direction8.DIR_DOWNLEFT],
            [Utility.Toolkit.Enums.Direction8.DIR_DOWN, Utility.Toolkit.Enums.Direction8.DIR_LEFT],
            [Utility.Toolkit.Enums.Direction8.DIR_DOWNLEFT, Utility.Toolkit.Enums.Direction8.DIR_UPLEFT],
            [Utility.Toolkit.Enums.Direction8.DIR_LEFT, Utility.Toolkit.Enums.Direction8.DIR_UP],
            ];



        /// <summary>
        /// 获取左右两边的方向
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>0:LEFT DIR, 1:RIGHT DIR</returns>
        public static Direction8[] Sides(this Direction8 direction)
        {
            return DIRECTION8_DEFINES[(Byte)direction];
        }





        /// <summary>
        /// 16方向
        /// </summary>
        /// <param name="start">当前位置</param>
        /// <param name="end">目标位置</param>
        /// <returns></returns>
        public static Direction16 Direction16(this Point start, Point end)
        {
            Int32 result = 0;
            Double fx = end.X - start.X;
            Double fy = end.Y - start.Y;
            Int32 sx = 0;
            Int32 sy = 0;
            if (fx == 0)
            {
                if (fy < 0)
                    result = 0;
                else
                    result = 8;
                return (Direction16)result;
            }
            if (fy == 0)
            {
                if (fx < 0)
                    result = 12;
                else
                    result = 4;
                return (Direction16)result;
            }
            if (fx > 0 && fy < 0)
            {
                result = 4;
                if (-fy > fx / 4) result = 3;
                if (-fy > fx / 1.9) result = 2;
                if (-fy > fx * 1.4) result = 1;
                if (-fy > fx * 4) result = 0;
            }
            if ((fx > 0) && (fy > 0))
            {
                result = 4;
                if (fy > fx / 4) result = 5;
                if (fy > fx / 1.9) result = 6;
                if (fy > fx * 1.4) result = 7;
                if (fy > fx * 4) result = 8;
            }
            if ((fx < 0) && (fy > 0))
            {
                result = 12;
                if (fy > -fx / 4) result = 11;
                if (fy > -fx / 1.9) result = 10;
                if (fy > -fx * 1.4) result = 9;
                if (fy > -fx * 4) result = 8;
            }
            if ((fx < 0) && (fy < 0))
            {
                result = 12;
                if (-fy > -fx / 4) result = 13;
                if (-fy > -fx / 1.9) result = 14;
                if (-fy > -fx * 1.4) result = 15;
                if (-fy > -fx * 4) result = 0;
            }
            return (Direction16)result;
        }



        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        /// <typeparam name="T">要转换的结构体类型</typeparam>
        /// <param name="bytes">Bytes数组</param>
        /// <returns>结构体对象</returns>
        public static T ToStruct<T>(this byte[] bytes) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            if (size > bytes.Length)
                return default(T);
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            T obj = (T)Marshal.PtrToStructure(structPtr, typeof(T));
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }


    }
}
