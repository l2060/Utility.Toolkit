using System;
using System.Runtime.InteropServices;


namespace Utility.Toolkit.Generals
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union8
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public Int64 Int64;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public Double Double;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public Int32 Int32H;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(4)] public Int32 Int32L;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public Int16 Int16H1;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(2)] public Int16 Int16H2;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(4)] public Int16 Int16L1;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(6)] public Int16 Int16L2;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public Byte Byte1;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(1)] public Byte Byte2;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(2)] public Byte Byte3;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(3)] public Byte Byte4;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(4)] public Byte Byte5;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(5)] public Byte Byte6;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(6)] public Byte Byte7;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(7)] public Byte Byte8;

    }
}
