using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utility.Toolkit.Enums;

namespace Utility.Toolkit.Generals
{

    public struct Orientation8
    {


        internal Orientation8(Direction8 forward, Direction8 left, Direction8 right)
        {
            Forward = forward;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// 
        /// </summary>
        public Direction8 Forward;

        /// <summary>
        /// 
        /// </summary>
        public Direction8 Left;

        /// <summary>
        /// 
        /// </summary>
        public Direction8 Right;
    }
}
