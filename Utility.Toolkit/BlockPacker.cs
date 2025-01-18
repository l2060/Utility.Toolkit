using System;
using System.Collections.Generic;
using System.Drawing;
using Utility.Toolkit.Generals;

namespace Utility.Toolkit
{
    /// <summary>
    /// A class that can pack a set of blocks into a single block.
    /// </summary>
    public sealed class BlockPacker
    {
        /// <summary>
        /// Packs the given blocks into the given area.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="blocks"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void PackBlocks(Size area, List<IBlockFragment> blocks)
        {
            // Step 1: Sort blocks by height and then by width for better packing
            blocks.Sort((a, b) =>
            {
                int heightComparison = b.Size.Height.CompareTo(a.Size.Height); // 优先按高度排序
                if (heightComparison == 0)
                    return a.Size.Width.CompareTo(b.Size.Width); // 如果高度相同，按宽度排序
                return heightComparison;
            });

            // Step 2: Set up the packing space
            int currentX = 0;  // 当前行的起始位置
            int currentY = 0;  // 当前行的纵坐标
            int currentRowHeight = 0;  // 当前行的最大高度
            int rowMaxHeight = 0;  // 整个区域的最大高度

            foreach (var block in blocks)
            {
                // Step 3: Check if block fits in current row, if not, move to the next row
                if (currentX + block.Size.Width > area.Width)
                {
                    // Move to the next row
                    currentY += currentRowHeight;  // Move down by the row's height
                    currentX = 0;  // Reset X position
                    currentRowHeight = 0;  // Reset row height
                }

                // Step 4: Place the block at the current (currentX, currentY) position
                block.Location = new Point(currentX, currentY);

                // Step 5: Update the X position for the next block in the current row
                currentX += block.Size.Width;

                // Step 6: Update the current row's maximum height
                currentRowHeight = Math.Max(currentRowHeight, block.Size.Height);

                // Update the overall maximum height if necessary
                rowMaxHeight = Math.Max(rowMaxHeight, currentRowHeight);
            }
        }

        public static void PackBlocks2(Size area, List<IBlockFragment> blocks)
        {
            // Step 1: Sort blocks by height and width (same as before)
            blocks.Sort((a, b) =>
            {
                int heightComparison = b.Size.Height.CompareTo(a.Size.Height);
                if (heightComparison == 0)
                    return a.Size.Width.CompareTo(b.Size.Width);
                return heightComparison;
            });

            // Step 2: Initialize DP table
            int n = blocks.Count;
            var dp = new int[area.Width + 1, area.Height + 1];

            // Fill DP table with a large value initially (indicating impossible states)
            for (int i = 0; i <= area.Width; i++)
            {
                for (int j = 0; j <= area.Height; j++)
                {
                    dp[i, j] = int.MaxValue;
                }
            }

            // Base case: 0 area, 0 blocks, 0 used space
            dp[0, 0] = 0;

            // Step 3: Dynamic programming to find the optimal packing
            for (int i = 0; i < n; i++)
            {
                var block = blocks[i];
                int blockWidth = block.Size.Width;
                int blockHeight = block.Size.Height;

                for (int w = area.Width; w >= blockWidth; w--) // Try placing block horizontally
                {
                    for (int h = area.Height; h >= blockHeight; h--) // Try placing block vertically
                    {
                        // Calculate the new space after placing the block
                        if (dp[w - blockWidth, h - blockHeight] != int.MaxValue)
                        {
                            dp[w, h] = Math.Min(dp[w, h], dp[w - blockWidth, h - blockHeight] + blockWidth * blockHeight);
                        }
                    }
                }
            }

            // Step 4: Recover the best placement from dp table
            int currentX = 0;
            int currentY = 0;
            int remainingWidth = area.Width;
            int remainingHeight = area.Height;

            // Fill the locations of blocks based on dp result
            foreach (var block in blocks)
            {
                // Try to find the location for this block using dp
                for (int w = remainingWidth; w >= block.Size.Width; w--)
                {
                    for (int h = remainingHeight; h >= block.Size.Height; h--)
                    {
                        if (dp[w, h] == dp[w - block.Size.Width, h - block.Size.Height] + block.Size.Width * block.Size.Height)
                        {
                            block.Location = new Point(currentX, currentY);
                            currentX += block.Size.Width;
                            currentY += block.Size.Height;
                            break;
                        }
                    }
                }
            }
        }


        public static void PackBlocks3(Size area, List<IBlockFragment> blocks)
        {
            // 按宽度降序排列方块
            blocks.Sort((a, b) =>
            {
                int widthComparison = b.Size.Width.CompareTo(a.Size.Width); // 优先宽度大的
                if (widthComparison == 0)
                    return b.Size.Height.CompareTo(a.Size.Height); // 宽度相同时，高度大的优先
                return widthComparison;
            });

            // DP 表，记录每个状态的最优放置
            int[,] dp = new int[area.Width + 1, area.Height + 1];

            // 初始化 DP 表
            for (int x = 0; x <= area.Width; x++)
                for (int y = 0; y <= area.Height; y++)
                    dp[x, y] = int.MaxValue;

            dp[0, 0] = 0; // 起点

            // 当前的横纵坐标
            int currentX = 0;
            int currentY = 0;
            int currentRowHeight = 0;

            foreach (var block in blocks)
            {
                int blockWidth = block.Size.Width;
                int blockHeight = block.Size.Height;

                // 尝试放置方块在当前行
                if (currentX + blockWidth <= area.Width)
                {
                    block.Location = new Point(currentX, currentY); // 放置方块
                    currentX += blockWidth; // 横向坐标增加
                    currentRowHeight = Math.Max(currentRowHeight, blockHeight); // 更新行高
                }
                else
                {
                    // 换行并放置方块
                    currentX = 0; // 新行从最左侧开始
                    currentY += currentRowHeight; // 累加上一行高度
                    currentRowHeight = blockHeight; // 新行高度更新为当前方块高度

                    // 检查区域是否足够放置当前方块
                    if (currentY + blockHeight > area.Height)
                    {
                        throw new InvalidOperationException("无法将所有方块放入指定区域内！");
                    }

                    block.Location = new Point(currentX, currentY); // 放置方块
                    currentX += blockWidth; // 横向坐标更新
                }
            }
        }

    }
}
