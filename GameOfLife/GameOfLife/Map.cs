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
        const int row = 10; //Pálya magassága
        const int col = 10; //Pálya szélessége

        public Entities entities = new Entities();
        public string[,] MapMatrix { get; set; } = new string[row, col];

        public Map()
        {
            Generate();
            GiveSurroundingsToEntities();
        }

        //Szöveges mátrix generálása
        public void Generate()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    MapMatrix[i, j] = "_";
                }
            }
            foreach (var rabbit in entities.RabbitList)
            {
                MapMatrix[rabbit.posY, rabbit.posX] = "N";
            }

            foreach (var fox in entities.FoxList)
            {
                MapMatrix[fox.posY, fox.posX] = "R";
            }

        }

        //Szöveges mátrix frissítése a listák változása alapján
        public void Update()
        {
            foreach (var grass in entities.GrassList)
            {
                if (grass.Size == 0)
                {
                    MapMatrix[grass.posY, grass.posX] = "_";
                }
                else if (grass.Size == 1)
                {
                    MapMatrix[grass.posY, grass.posX] = "~";
                }
                else
                {
                    MapMatrix[grass.posY, grass.posX] = "-";
                }
            }
            foreach (var rabbit in entities.RabbitList)
            {
                MapMatrix[rabbit.posY, rabbit.posX] = "N";
            }

            foreach (var fox in entities.FoxList)
            {
                MapMatrix[fox.posY, fox.posX] = "R";
            }
        }

        //A környezetet átadja a nyulaknak és a rókáknak
        public void GiveSurroundingsToEntities()
        {
            foreach (var rabbit in entities.RabbitList)
            {
                rabbit.GetSurroundings(MapMatrix, entities);
            }

            foreach (var fox in entities.FoxList)
            {
                fox.GetSurroundings(MapMatrix, entities);
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
