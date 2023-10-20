using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Simulation
    {
        public int maxTurns = 50;
        public int currentTurn = 0;
        public Map map { get; init; }

        public bool isGameOver = false;

        public Simulation()
        {
            map = new Map();
        }

        public void Start()
        {
            map.Draw();

            while (!isGameOver)
            {
                Console.Clear();
                Console.WriteLine($"Current turn:\t{currentTurn}/{maxTurns}\n");

                DoEntityTurns();
                map.Update();

                map.Draw();

                if (CheckGameOver())
                {
                    Console.WriteLine("\nGame Over.");
                    Console.WriteLine("Press Enter to quit...");
                    Console.ReadLine();
                    break;
                }

                currentTurn++;

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        public void DoEntityTurns()
        {
            foreach (var rabbit in map.entities.RabbitList.ToList())
            {
                map.GiveSurroundingsToEntities();

                rabbit.Turn(map);
            }

            foreach (var fox in map.entities.FoxList.ToList())
            {
                map.GiveSurroundingsToEntities();

                fox.Turn(map);
            }

            foreach (var grass in map.entities.GrassList.ToList())
            {
                map.GiveSurroundingsToEntities();

                grass.Turn();
            }
        }

        public bool CheckGameOver()
        {
            if (currentTurn == maxTurns) return true;
            else if (map.entities.RabbitList.Count == 0 || map.entities.FoxList.Count == 0) return true;
            else return false;
        }
    }
}
