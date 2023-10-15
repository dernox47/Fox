using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GameOfLife
{
    internal class Fox
    {
        public int Fullness { get; init; }
        public int PozX { get; init; }
        public int PozY { get; init; }
        public struct Point
        {
            public int X;
            public int Y;
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        static string Breed(int pointX, int pointY)
        {
            List<Point> zeroPositions = new List<Point>();
            int[,] mat = { { 1, 2, 3, 4, 5 }, { 4, 5, 6, 7, 8 }, { 1, 2, 3, 4, 5 } };
            int height = mat.GetLength(0);
            int width = mat.GetLength(1);
            int left = Math.Max(pointX - 1, 0);
            int right = Math.Min(pointX + 1, width);
            int top = Math.Max(pointY - 1, 0);
            int bottom = Math.Min(pointY + 1, height);
            int foxneighbour = 0;
            zeroPositions.Clear();
            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    if (mat[x, y] == 'F')
                    {
                        foxneighbour++;
                    }
                    else if (mat[x, y] == '#')
                    {
                        zeroPositions.Add(new Point(x, y));
                    }
                }
            }
            if  (zeroPositions.Count>0 && foxneighbour > 0)
            {
                return zeroPositions[1].X.ToString() +';'+ zeroPositions[1].Y.ToString();
            }
            else
            {
                return "NO";
            }
        }


    }
}