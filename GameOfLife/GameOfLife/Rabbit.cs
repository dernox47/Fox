using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Rabbit
    {
        private int fullness;

        public int Fullness
        {
            get { return fullness; }
            init 
            { 
                if (value < 0) { fullness = 0; }
                else if (value > 5) { fullness = 5; }
                else { fullness = value; }
            }
        }

        public bool HasEaten { get; init; }
        public bool Alive => Fullness > 0;
        public bool Hungry => Fullness < 5;
        
        public void Eat(Grass grass)
        {

        }

        public void Reproduction()
        {

        }
    }
}