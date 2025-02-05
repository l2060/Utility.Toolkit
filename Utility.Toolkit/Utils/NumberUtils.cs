using System;
using System.Drawing;
using System.Runtime.CompilerServices;



namespace Utility.Toolkit.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class NumberUtils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Combine(Int32 higth, Int32 low)
        {
            return ((Int64)higth << 32) | (uint)low;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Combine(UInt32 higth, UInt32 low)
        {
            return ((Int64)higth << 32) | (uint)low;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Combine(UInt32 higth, Int32 low)
        {
            return ((Int64)higth << 32) | (uint)low;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Combine(Int32 higth, UInt32 low)
        {
            return ((Int64)higth << 32) | (uint)low;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in Int64 value, out Int32 higth, out Int32 low)
        {
            higth = (Int32)(value >> 32);
            low = (Int32)(value & 0xFFFFFFFF);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in Int64 value, out UInt32 higth, out UInt32 low)
        {
            higth = (UInt32)(value >> 32);
            low = (UInt32)(value & 0xFFFFFFFF);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in Int64 value, out UInt32 higth, out Int32 low)
        {
            higth = (UInt32)(value >> 32);
            low = (Int32)(value & 0xFFFFFFFF);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in Int64 value, out Int32 higth, out UInt32 low)
        {
            higth = (Int32)(value >> 32);
            low = (UInt32)(value & 0xFFFFFFFF);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in UInt64 value, out Int32 higth, out Int32 low)
        {
            higth = (Int32)(value >> 32);
            low = (Int32)(value & 0xFFFFFFFF);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in UInt64 value, out UInt32 higth, out UInt32 low)
        {
            higth = (UInt32)(value >> 32);
            low = (UInt32)(value & 0xFFFFFFFF);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in UInt64 value, out Int32 higth, out UInt32 low)
        {
            higth = (Int32)(value >> 32);
            low = (UInt32)(value & 0xFFFFFFFF);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="higth"></param>
        /// <param name="low"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Split(in UInt64 value, out UInt32 higth, out Int32 low)
        {
            higth = (UInt32)(value >> 32);
            low = (Int32)(value & 0xFFFFFFFF);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value64"></param>
        /// <returns></returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ToPoint(this Int64 Value64)
        {
            int X = (Int32)(Value64 >> 32);
            int Y = (Int32)(((Int64)(Value64 >> 32 << 32)) | Value64);
            return new Point(X, Y);
        }



    }
}
