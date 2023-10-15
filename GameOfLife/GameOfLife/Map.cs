using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Map
    {
        const int row = 20;
        const int col = 30;
        public Grass[,] FieldMatrix { get; init; }
        public Rabbit[,] RabbitMatrix { get; init; }
        public Fox[,] FoxMatrix { get; init; }
        public string[,] MapMatrix { get; init; }

        public Map()
        {
            MapMatrix = new string[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    MapMatrix[i, j] = "_";
                }
            }

            FieldMatrix = new Grass[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    FieldMatrix[i, j] = new Grass(); 
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
