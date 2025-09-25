using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Toolkit
{

    /// <summary>
    /// AStar路径搜索算法
    /// </summary>
    public class AStar
    {
        // 定义八个方向（上下左右和四个对角线方向）
        private static readonly Point[] Directions =
        [
            new Point(0, -1),  // 上
            new Point(0, 1),   // 下
            new Point(-1, 0),  // 左
            new Point(1, 0),   // 右
            new Point(-1, -1), // 左上
            new Point(1, -1),  // 右上
            new Point(-1, 1),  // 左下
            new Point(1, 1)    // 右下
        ];

        /// <summary>
        /// 异步查找路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <param name="isWalkable"></param>
        /// <returns></returns>
        public static async Task<List<Point>> FindPathAsync(Point start, Point goal, Func<Point, bool> isWalkable)
        {
            // 异步路径搜索
            return await Task.Run(() => FindPath(start, goal, isWalkable));
        }

        /// <summary>
        /// 查找路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <param name="isWalkable"></param>
        /// <returns></returns>
        public static List<Point> FindPath(Point start, Point goal,  Func<Point, bool>  isWalkable)
        {
            // 开放列表：使用优先队列
            var openList = new PriorityQueue<Node, int>();
            var closedList = new HashSet<Point>();
            var openSet = new HashSet<Point>();  // 用于标记 openList 中存在的节点

            var startNode = new Node(start, null, 0, Heuristic(start, goal));
            openList.Enqueue(startNode, startNode.F);
            openSet.Add(startNode.Position); // 记录入队的节点

            var cameFrom = new Dictionary<Point, Point>();

            while (openList.Count > 0)
            {
                // 获取F值最小的节点
                var currentNode = openList.Dequeue();
                openSet.Remove(currentNode.Position); // 从 openSet 中移除

                // 到达目标点
                if (currentNode.Position == goal)
                {
                    return ReconstructPath(cameFrom, currentNode.Position);
                }

                closedList.Add(currentNode.Position);

                // 扩展八个方向
                foreach (var direction in Directions)
                {
                    var neighbor = new Point(currentNode.Position.X + direction.X, currentNode.Position.Y + direction.Y);

                    // 检查邻居是否可行
                    if (closedList.Contains(neighbor) || !isWalkable(neighbor))
                    {
                        continue;
                    }

                    int g = currentNode.G + 1;
                    int h = Heuristic(neighbor, goal);
                    var neighborNode = new Node(neighbor, currentNode, g, h);

                    // 如果该节点不在 openSet 中或找到更优路径
                    if (!openSet.Contains(neighborNode.Position) || g < neighborNode.G)
                    {
                        openList.Enqueue(neighborNode, neighborNode.F);
                        openSet.Add(neighborNode.Position);  // 记录入队的节点
                        cameFrom[neighbor] = currentNode.Position;
                    }
                }
            }

            // 无法找到路径
            return new List<Point>();
        }

        private static List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var path = new List<Point>();
            while (cameFrom.ContainsKey(current))
            {
                path.Insert(0, current);
                current = cameFrom[current];
            }
            return path;
        }

        // 计算切比雪夫距离作为启发式函数
        private static int Heuristic(Point a, Point b)
        {
            return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y)); // 切比雪夫距离
        }

        // 节点类
        private class Node : IComparable<Node>
        {
            public Point Position { get; }
            public Node Parent { get; }
            public int G { get; }
            public int H { get; }
            public int F => G + H;

            public Node(Point position, Node parent, int g, int h)
            {
                Position = position;
                Parent = parent;
                G = g;
                H = h;
            }

            public int CompareTo(Node other)
            {
                return F.CompareTo(other.F);
            }

            public override bool Equals(object obj)
            {
                return obj is Node node && Position.Equals(node.Position);
            }

            public override int GetHashCode()
            {
                return Position.GetHashCode();
            }
        }
    }

}
 