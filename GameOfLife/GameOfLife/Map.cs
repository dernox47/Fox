using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Map
    {
        public string[,]? MapMatrix { get; init; }
        public int row { get; init; }
        public int col { get; init; }

        public Map(int row = 20, int col = 30)
        {
            this.row = row++;
            this.col = col++;
            MapMatrix = new string[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == 0 || i == row - 2 || j == 0 || j == col - 2)
                    {
                        MapMatrix[i, j] = "=";
                    }
                    else { MapMatrix[i, j] = "#"; }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(MapMatrix[i, j]);
                }
                Console.WriteLine();
            }
            
        }
    }
}
