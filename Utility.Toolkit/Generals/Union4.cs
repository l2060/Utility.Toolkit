using System;
using System.Runtime.InteropServices;


namespace Utility.Toolkit.Generals
{

    /// <summary>
    /// 四字节联合类型
    /// </summary>
     
    [StructLayout(LayoutKind.Explicit)]
    public struct Union4
    {
        /// <summary>
        /// 4字节Int32类型值
        /// </summary>
         
        [FieldOffset(0)] public Int32 Int32;
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public UInt32 UInt32;

        /// <summary>
        /// Int16 高位
        /// </summary>
        [FieldOffset(0)] public Int16 Int16H;

        /// <summary>
        /// Int16 低位
        /// </summary>
        [FieldOffset(2)] public Int16 Int16L;


        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)] public UInt16 UInt16H;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(2)] public UInt16 UInt16L;

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

    }
}
