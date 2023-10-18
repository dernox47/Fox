using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Map
    {
        static Random r = new Random();
        const int row = 20;
        const int col = 30;

        public Entities entities = new Entities();
        public string[,] Map { get; set; } = new string[row, col];

        public Map()
        {
            Generate();
        }

        //Szöveges mátrix generálása
        public void Generate()
        {
            foreach (var rabbit in entities.RabbitList)
            {
                Map[rabbit.posY, rabbit.posX] = "N";
            }

            foreach (var fox in entities.FoxList)
            {
                Map[fox.posY, fox.posX] = "R";
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Map[i, j] = ".";
                }
            }
        }

        public void Update()
        {
            foreach (var rabbit in entities.RabbitList)
            {
                Map[rabbit.posY, rabbit.posX] = "N";
            }

            foreach (var fox in entities.FoxList)
            {
                Map[fox.posY, fox.posX] = "R";
            }

            foreach (var grass in entities.GrassList)
            {
                if (grass.Size == 0)
                {
                    Map[grass.posY, grass.posX] = ".";
                }
                else if (grass.Size == 1)
                {
                    Map[grass.posY, grass.posX] = "-";
                }
                else
                {
                    Map[grass.posY, grass.posX] = "#";
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(Map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
