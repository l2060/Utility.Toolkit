using System;
using System.Drawing;
using System.IO.Pipelines;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Utility.Toolkit.Encodings;

namespace Utility.Toolkit.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var sss = RSA.Default.Encrypt(System.Text.Encoding.UTF8.GetBytes("12345678"));


            var pipe = new Pipe();
            await pipe.Writer.WriteAsync(bytes);

            var result = await pipe.Reader.ReadAsync();




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








        private static void test()
        {
            // 定义角度 (90° -> 弧度)
            var angle = Math.PI / 2;

            // 构建绕Z轴旋转的 4x4 矩阵
            Double[][] Rz4x4 = [
              [Math.Cos(angle), -Math.Sin(angle), 0, 0],
              [Math.Sin(angle),  Math.Cos(angle), 0, 0],
              [0, 0, 1, 0],
              [0, 0, 0, 1]
            ];

            // 原始点 (1,0,0,1)
            Double[] P = [1, 0, 0, 1];

            // 矩阵乘法 Rz4x4 * P
            Double[] P_new = [
              Rz4x4[0][0]*P[0] + Rz4x4[0][1]*P[1] + Rz4x4[0][2]*P[2] + Rz4x4[0][3]*P[3],
              Rz4x4[1][0]*P[0] + Rz4x4[1][1]*P[1] + Rz4x4[1][2]*P[2] + Rz4x4[1][3]*P[3],
              Rz4x4[2][0]*P[0] + Rz4x4[2][1]*P[1] + Rz4x4[2][2]*P[2] + Rz4x4[2][3]*P[3],
              Rz4x4[3][0]*P[0] + Rz4x4[3][1]*P[1] + Rz4x4[3][2]*P[2] + Rz4x4[3][3]*P[3],
            ];

            Console.WriteLine("旋转后的新坐标:", P_new);
            // 输出: [0, 1, 0, 1]

        }
    }
}
