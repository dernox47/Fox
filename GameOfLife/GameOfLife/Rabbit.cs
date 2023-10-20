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
        private int lastReproduction = 0; //Hány köre szaporodott utoljára
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

        public Grass getGrass => GetGrass(); //teszt
        public Dictionary<int[], string> Neighbors; //teszt


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
            Neighbors = GetNeighbors(); //Minden kör elején megkapja a környezetét

            Move(); //Elmozdul

            if (CanEat()) Eat(); //Eszik, ha van lehetőség rá

            if (CanReproduce()) Reproduce(map); //szaporodik, ha van lehetőség rá

            //if (!Alive) Die(map); //Meghal, ha a telítettsége 0 érték alá csökken

            Fullness--; //Kör végén csökken 1-gyel a telítettsége

            LastReproduction++; //Kör végén növeli, hogy hány köre szaporodott utoljára
        }

        public void Move() //Elmozdul a helyéről, előnyben részesíti a mezőt ahol a fű kifejlett (érték: 2) 
        {
            if (Neighbors.ContainsValue("F"))
            {
                foreach (var grass in Entities.GrassList)
                {
                    var fullGrassPositions = Neighbors.Where(kv =>
                    {
                        int[] neighborPosition = kv.Key;
                        return neighborPosition[0] == grass.posY &&
                                neighborPosition[1] == grass.posX &&
                                grass.Size == 2;
                    }).Select(kv => kv.Key).ToList();

                    if (fullGrassPositions.Count > 0)
                    {
                        int randomIndex = r.Next(fullGrassPositions.Count);
                        int[] selectedPosition = fullGrassPositions[randomIndex];
                        posX = selectedPosition[1];
                        posY = selectedPosition[0];
                    }
                    else
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
            }
            
        }

        public Grass GetGrass() //A pocíciójában lévő füvet adja vissza
        {
            return Entities.GrassList.First(x => x.posX == posX && x.posY == posY);
        }

        public bool CanEat() //Ehető-e a fű, a mérete nagyobb 0-nál, meg tudja enni análkül, hogy a telítettségét meghaladná
        {
            return GetGrass().Size > 0 && Hungry && GetGrass().Size + Fullness !> maxFullness;
        }

        public void Eat()
        {
            Fullness += GetGrass().Size;
            GetGrass().Eaten();
        }

        public void Die(Map map) //Meghal, kiszedi a példányt a nyulak listájából
        {
            map.entities.RabbitList.Remove(map.entities.RabbitList.First(x => x.posX == posX && x.posY == posY));
        }

        //Tud-e szaporodni (nincs mellette róka, de van mellette nyúl és szabad hely)
        public bool CanReproduce()
        {
            return Neighbors.ContainsValue("N") &&
                Neighbors.ContainsValue("F") && 
                !Neighbors.ContainsValue("R") && 
                lastReproduction == maxReproductionCD;
        }

        //Szaporodik, hozzáadja a példányt a Map osztályban lévő Entities osztály segítségével a listájához
        public void Reproduce(Map map)
        {
            int randomPos = r.Next(0, Neighbors.Count);

            Rabbit newRabbit = new Rabbit(Neighbors.ElementAt(randomPos).Key[1], Neighbors.ElementAt(randomPos).Key[0]);

            map.entities.RabbitList.Add(newRabbit);

            lastReproduction = 0;
        }

        //Egy szótárba menti a körülötte lévő mezőket az alábbi formában [posX, posY] => "milyen mező"
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