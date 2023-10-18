using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GameOfLife
{
    internal class Fox
    {
        static Random r = new Random();

        const int row = 10; //Pálya magassága
        const int col = 10; //Pálya szélessége
        public int value = 3;

        public int posX { get; init; }
        public int posY { get; init; }

        private int fullness;
        public int Fullness
        {
            get { return fullness; }
            set
            {
                if (value < 0) fullness = 0;
                else if (value > 10) fullness = 10;
                else fullness = value;
            }
        }

        public bool Alive => Fullness > 0;
        public bool Hungry => Fullness < 10;

        public Fox(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;

            Fullness = r.Next(0, 11);
        }

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