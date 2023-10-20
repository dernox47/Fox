using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Entities
    {
        static Random r = new Random();
        const int row = 10; //Pálya magassága
        const int col = 10; //Pálya szélessége

        public List<Grass> GrassList { get; init; }
        public List<Rabbit> RabbitList { get; init; }
        public List<Fox> FoxList { get; init; }

        public Entities()
        {
            GrassList = GenerateGrassList();
            RabbitList = GenerateRabbitList();
            FoxList = GenerateFoxList();
        }

        //Élőlények listájának legenerálása
        public List<Grass> GenerateGrassList()
        {
            List<Grass> grassList = new List<Grass>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grassList.Add(new Grass(i, j));
                }
            }

            return grassList;
        }

        private static HashSet<(int, int)> usedCoordinates = new HashSet<(int, int)>();
        public List<Rabbit> GenerateRabbitList()
        {
            List<Rabbit> rabbitList = new List<Rabbit>();

            while (rabbitList.Count < 2)
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

            while (foxList.Count < 1)
            {
                int x = r.Next(0, col);
                int y = r.Next(0, row);

                var coordinates = (x, y);

                if (!usedCoordinates.Contains(coordinates))
                {
                    foxList.Add(new Fox(x, y));
                    usedCoordinates.Add(coordinates);
                }
            }

            return foxList;
        }
    }
}
