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
        
        const int row = 10; //Pálya magassága
        const int col = 10; //Pálya szélessége
        static int value = 3; //Tápérték a róka számára

        public string[,] Map { get; set; } = new string[row, col];
        public Entities Entities { get; set; }

        public int posX { get; set; }
        public int posY { get; set; }

        static int maxReproductionCD = 3; //Maximum 3 körönként tud szaporodni
        private int lastReproduction; //Hány köre szaporodott utoljára
        public int LastReproduction
        {
            get { return lastReproduction; }
            set 
            { 
                if (value < 0) lastReproduction = 0;
                else if (value > 3) lastReproduction = 3;
                else lastReproduction = value;
            }
        }

        static int maxFullness = 5;
        private int fullness; //Telítettség
        public int Fullness
        {
            get { return fullness; }
            set 
            { 
                if (value < -1) fullness = -1;
                else if (value > 5) fullness = 5;
                else fullness = value;
            }
        }

        public bool Alive => Fullness > 0; //Ha 0 érték alá esik a telítettsége, akkor meghal
        public bool Hungry => Fullness < 5; //Ha 5 érték alatt van a telítettsége, akkor éhes

        public Grass grass => GetGrass();

        public Rabbit(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;

            Fullness = r.Next(1, 6);
        }

        public void GetSurroundings(string[,] map, Entities entities)
        {
            Map = map;
            Entities = entities;
        }

        public void Turn(Map map)
        {
            Move(); //elmozdul

            if (CanEat()) Eat(); //eszik, ha van lehetőség rá

            if (CanReproduce()) Reproduce(map); //szaporodik, ha van lehetőség rá

            if (!Alive) Die(map); //meghal, ha a telítettsége 0 érték alá csökken

            Fullness--; //kör végén csökken 1-gyel a telítettsége
        }

        public void Move()
        {
            int randomPos = r.Next(0, GetNeighbors().Count);
            posX = GetNeighbors().ElementAt(randomPos).Key[0];
            posY = GetNeighbors().ElementAt(randomPos).Key[1];
        }

        public Grass GetGrass()
        {
            return Entities.GrassList.First(x => x.posX == posX && x.posY == posY);
        }

        public bool CanEat()
        {
            return GetGrass().Size > 0 && Hungry && GetGrass().Size + Fullness !> maxFullness;
        }

        public void Eat()
        {
            Fullness += GetGrass().Size;
            GetGrass().Eaten();
        }

        public void Die(Map map)
        {
            map.entities.RabbitList.Remove(map.entities.RabbitList.First(x => x.posX == posX && x.posY == posY));
        }

        //Tud-e a nyúl szaporodni (nincs mellette róka, de van mellette nyúl és szabad hely)
        public bool CanReproduce()
        {
            return GetNeighbors().ContainsValue("N") && 
                GetNeighbors().ContainsValue("F") && 
                !GetNeighbors().ContainsValue("R") && 
                lastReproduction == maxReproductionCD;
        }

        //Szaporodik, hozzáadja a példányt a Map osztályban lévő Entities osztály segítségével a listájához
        public void Reproduce(Map map)
        {
            int randomPos = r.Next(0, GetNeighbors().Count);

            Rabbit newRabbit = new Rabbit(GetNeighbors().ElementAt(randomPos).Key[0], GetNeighbors().ElementAt(randomPos).Key[1]);

            map.entities.RabbitList.Add(newRabbit);
        }

        //Egy szótárba menti a körülötte lévő mezőket az alábbi formában [posX, posY] => "élőlény"
        public Dictionary<int[], string> GetNeighbors()
        {
            Dictionary<int[], string> result = new Dictionary<int[], string>();
            for (int i = posY - 1; i < posY + 2; i++)
            {
                for (int j = posX - 1; j < posX + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= row || j >= col)))
                    {
                        if (Map[i, j] != "R" && Map[i, j] != "N")
                        {
                            result[new int[] { i, j }] = "F";
                        }
                        else if (Map[i, j] == "N" && (i != posY && j != posX)) result[new int[] { i, j }] = "N";
                        else if (Map[i, j] == "R") result[new int[] { i, j }] = "R";
                    }
                }
            }
            return result;
        }
    }
}