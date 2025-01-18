using System.Drawing;

namespace Utility.Toolkit.Generals
{
    /// <summary>
    /// Represents a fragment of a block.
    /// </summary>
    public interface IBlockFragment
    {

        /// <summary>
        /// Gets the location of the fragment.
        /// </summary>
        public Point Location { get; set; }


        /// <summary>
        /// Get/Set the size of the block fragment.
        /// </summary>
        public Size Size { get; set; }
    }
}
