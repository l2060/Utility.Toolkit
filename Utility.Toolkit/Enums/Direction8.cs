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
        DIR_UP = 0,

        /// <summary>
        /// 向右上
        /// </summary>
        DIR_UPRIGHT = 1,
        /// <summary>
        /// 向右
        /// </summary>
        DIR_RIGHT = 2,
        /// <summary>
        /// 向右下
        /// </summary>
        DIR_DOWNRIGHT = 3,
        /// <summary>
        /// 向下
        /// </summary>
        DIR_DOWN = 4,
        /// <summary>
        /// 向左下
        /// </summary>
        DIR_DOWNLEFT = 5,
        /// <summary>
        /// 向左
        /// </summary>
        DIR_LEFT = 6,
        /// <summary>
        /// 向左上
        /// </summary>
        DIR_UPLEFT = 7,
    }
}
