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

        public Simulation()
        {
            
        }

        public void Start()
        {

        }

        public void DoEntityTurns()
        {
            foreach (var rabbit in map.entities.RabbitList)
            {
                rabbit.Turn(map);
            }

            //PATO: Rókák köre

            //GEDEON: Füvek köre (csak növés, ha nem áll rajta nyúl)
        }
    }
}
