using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Rabbit
    {
        static Random r = new Random();
        
        const int row = 20;
        const int col = 30;
        public int value = 3;

        public string[,] Map { get; set; } = new string[row, col];
        public Entities Entities { get; set; } 

        public int posX { get; init; }
        public int posY { get; init; }

        private int fullness;
        public int Fullness
        {
            get { return fullness; }
            set 
            { 
                if (value < 0) fullness = 0;
                else if (value > 5) fullness = 5;
                else fullness = value;
            }
        }

        public bool Alive => Fullness > 0;
        public bool Hungry => Fullness < 5;
        

        public Rabbit(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;

            Fullness = r.Next(0, 6);
        }

        public void GetSurroundings(string[,] map, Entities entities)
        {
            Map = map;
            Entities = entities;
        }

        public void Scan()
        {

        }

        public void Move()
        {

        }

        public void Eat(Grass grass)
        {
            Fullness += grass.Size;
            grass.Eaten();
        }

        public bool CanReproduce()
        {
            return GetNeighbors().Keys.Contains("N") && GetNeighbors().Keys.Contains("F");
        }

        public Dictionary<string, int[]> GetNeighbors()
        {
            Dictionary<string, int[]> result = new Dictionary<string, int[]>();
            for (int i = posY - 1; i < posY + 2; i++)
            {
                for (int j = posX - 1; j < posX + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= row || j >= col)))
                    {
                        if (!(Map[i, j] == "R" || Map[i, j] == "N"))
                        {
                            result["F"] = new int[] { i, j };
                        }
                        else if (Map[i,j] == "N") result["N"] = new int[] { i, j };
                    }
                }
            }
            return result;
        }
    }
}