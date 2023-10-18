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
        public Map Map { get; init; }

        public Simulation()
        {
            Map = new Map();
        }

        public void Start()
        {

        }
    }
}
