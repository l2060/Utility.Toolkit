using System;
using System.Drawing;
using System.Threading.Tasks;
using Utility.Toolkit.Encodings;

namespace Utility.Toolkit.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 动态定义地图可行性（障碍物用Lambda定义）
            Func<Point, bool> isWalkable = (point) =>
            {
                int[,] grid =
                {
                { 0, 0, 0, 0, 0 },
                { 1, 1, 0, 1, 1 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };
                int x = point.X, y = point.Y;
                return x >= 0 && x < 5 &&
                       y >= 0 && y < 5 &&
                       grid[x, y] == 0; // 0 表示通路，1 表示障碍
            };


            var start = new Point(0, 0);
            var goal = new Point(4, 4);

            // 异步调用路径搜索
            var path = await AStar.FindPathAsync(start, goal, isWalkable);

            // 输出路径
            if (path.Count > 0)
            {
                Console.WriteLine("Path found:");
                foreach (var point in path)
                {
                    Console.WriteLine($"({point.X}, {point.Y})");
                }
            }
            else
            {
                Console.WriteLine("No path found.");
            }

            var s = 999999999999999999.ToHex94();

            Hex94.Parse(s, out var int64Value);

            Console.WriteLine("Hello, World!");


        }
    }
}
