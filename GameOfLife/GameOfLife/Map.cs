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
        public List<Grass> GrassList { get; init; }
        public List<Rabbit> RabbitList { get; init; }
        public List<Fox> FoxList { get; init; }
        public string[,] MapMatrix { get; init; }

        public Map()
        {
            GrassList = GenerateGrassList();
            RabbitList = GenerateRabbitList();
            FoxList = GenerateFoxList();
            
            MapMatrix = new string[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    MapMatrix[i, j] = "_";
                }
            }
        }

        public List<Grass> GenerateGrassList()
        {
            List<Grass> grassList = new List<Grass>();

            for (int i = 0; i < row * col; i++)
            {
                grassList.Add(new Grass());
            }

            return grassList;
        }

        private static HashSet<(int, int)> usedCoordinates = new HashSet<(int, int)>();
        public List<Rabbit> GenerateRabbitList()
        {
            List<Rabbit> rabbitList = new List<Rabbit>();
            
            while (rabbitList.Count < 5)
            {
                int x = r.Next(0, col);
                int y = r.Next(0, row);

                var coordinates = (x, y);

                if (!usedCoordinates.Contains(coordinates))
                {
                    rabbitList.Add(new Rabbit(x, y));
                    usedCoordinates.Add(coordinates);
                }
            }

            return rabbitList;
        }

        public List<Fox> GenerateFoxList()
        {
            List<Fox> foxList = new List<Fox>();

            while (foxList.Count < 4)
            {
                int x = r.Next(0, col);
                int y = r.Next(0, row);

                var coordinates = (x, y);

                if (!usedCoordinates.Contains(coordinates))
                {
                    foxList.Add(new Fox());
                    usedCoordinates.Add(coordinates);
                }
            }

            return foxList;
        }

        public void GenerateMap()
        {
            foreach (var rabbit in RabbitList)
            {
                MapMatrix[rabbit.posX, rabbit.posY] = "N";
            }

            foreach (var fox in FoxList)
            {
                MapMatrix[fox.PozX, fox.PozY] = "R";
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
