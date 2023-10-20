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

        public string[,] Map { get; set; } = new string[row, col];
        public Entities Entities { get; set; }

        public int posX { get; set; }
        public int posY { get; set; }

        static int maxReproductionCD = 4; //Maximum 4 körönként tud szaporodni
        private int lastReproduction = 0; //Hány köre szaporodott utoljára
        public int LastReproduction
        {
            get { return lastReproduction; }
            set
            {
                if (value < 0) lastReproduction = 0;
                else if (value > 4) lastReproduction = 4;
                else lastReproduction = value;
            }
        }

        static int maxFullness = 10;
        private int fullness; //Telítettség
        public int Fullness
        {
            get { return fullness; }
            set
            {
                if (value < -1) fullness = -1;
                else if (value > 10) fullness = 10;
                else fullness = value;
            }
        }

        public bool Alive => Fullness > 0; //Ha 0 érték alá esik a telítettsége, akkor meghal
        public bool Hungry => Fullness < 10; //Ha 10 érték alatt van a telítettsége, akkor éhes

        public Dictionary<int[], string> Neighbors; //teszt


        public Fox(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;

            Fullness = r.Next(1, 11);
        }

        public void GetSurroundings(string[,] map, Entities entities)
        {
            Map = map;
            Entities = entities;
        }

        public void Turn(Map map)
        {
            Neighbors = GetNeighbors(); //Minden kör elején megkapja a környezetét

            Move(); //Elmozdul

            if (CanEat()) Eat(map); //Eszik, ha van lehetőség rá

            if (CanReproduce()) Reproduce(map); //szaporodik, ha van lehetőség rá

            //if (!Alive) Die(map); //Meghal, ha a telítettsége 0 érték alá csökken

            Fullness--; //Kör végén csökken 1-gyel a telítettsége

            LastReproduction++; //Kör végén növeli, hogy hány köre szaporodott utoljára
        }

        public void Move() //Elmozdul a helyéről, előnyben részesíti a mezőt ahol a fű kifejlett (érték: 2) 
        {
            if (Neighbors.ContainsValue("N"))
            {
                var rabbitPositions = Neighbors
                                .Where(kv => kv.Value == "N")
                                .Select(kv => kv.Key)
                                .ToList();

                int randomIndex = r.Next(rabbitPositions.Count);
                int[] selectedPosition = rabbitPositions[randomIndex];
                posX = selectedPosition[1];
                posY = selectedPosition[0];
                
            }
            else if (Neighbors.ContainsValue("F"))
            {
                var grassPositions = Neighbors
                                .Where(kv => kv.Value == "F")
                                .Select(kv => kv.Key)
                                .ToList();

                int randomIndex = r.Next(grassPositions.Count);
                int[] selectedPosition = grassPositions[randomIndex];
                posX = selectedPosition[1];
                posY = selectedPosition[0];
            }

       
        }

        public bool CanEat() 
        {
            return Hungry && 
                3 + Fullness !> maxFullness &&
                Entities.RabbitList.Exists(x => x.posX == this.posX && x.posY == this.posY);
        }

        public void Eat(Map map)
        {
            var rabbit = Entities.RabbitList.First(x => x.posX == this.posX && x.posY == this.posY);
            rabbit.Die(map);
            Fullness += 3;
        }

        public void Die(Map map) //Meghal, kiszedi a példányt a rókák listájából
        {
            map.entities.FoxList.Remove(map.entities.FoxList.First(x => x.posX == posX && x.posY == posY));
        }

        public bool CanReproduce()
        {
            return Neighbors.ContainsValue("R") &&
                Neighbors.ContainsValue("F") &&
                lastReproduction == maxReproductionCD;
        }

        //Szaporodik, hozzáadja a példányt a Map osztályban lévő Entities osztály segítségével a listájához
        public void Reproduce(Map map)
        {
            int randomPos = r.Next(0, Neighbors.Count);

            Fox newFox = new Fox(Neighbors.ElementAt(randomPos).Key[1], Neighbors.ElementAt(randomPos).Key[0]);

            map.entities.FoxList.Add(newFox);

            lastReproduction = 0;
        }

        //Egy szótárba menti a körülötte lévő mezőket az alábbi formában [posX, posY] => "milyen mező"
        public Dictionary<int[], string> GetNeighbors()
        {
            Dictionary<int[], string> result = new Dictionary<int[], string>();
            for (int i = posY - 2; i < posY + 3; i++)
            {
                for (int j = posX - 2; j < posX + 3; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= row || j >= col)))
                    {
                        if (Map[i, j] != "R" && Map[i, j] != "N")
                        {
                            result[new int[] { i, j }] = "F";
                        }
                        else if (Map[i, j] == "R" && (i != posY && j != posX)) result[new int[] { i, j }] = "R";
                        else if (Map[i, j] == "N") result[new int[] { i, j }] = "N";
                    }
                }
            }
            return result;
        }
    }
}