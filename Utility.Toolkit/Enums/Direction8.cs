using System;

namespace Utility.Toolkit.Enums
{
    /// <summary>
    /// 8方向枚举
    /// </summary>
    public enum Direction8 : Byte
    {
        /// <summary>
        /// 向上
        /// </summary>
        UP = 0,

        /// <summary>
        /// 向右上
        /// </summary>
        UPRIGHT = 1,
        /// <summary>
        /// 向右
        /// </summary>
        RIGHT = 2,
        /// <summary>
        /// 向右下
        /// </summary>
        DOWNRIGHT = 3,
        /// <summary>
        /// 向下
        /// </summary>
        DOWN = 4,
        /// <summary>
        /// 向左下
        /// </summary>
        DOWNLEFT = 5,
        /// <summary>
        /// 向左
        /// </summary>
        LEFT = 6,
        /// <summary>
        /// 向左上
        /// </summary>
        UPLEFT = 7,
    }
}
