using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Grass
    {
        private int size;

        public int Size
        {
            get { return size; }
            init 
            { 
                if (value > 2)
                {
                    size = 2;
                }
                else if (value < 0)
                {
                    size = 0;
                }
            }
        }

        public int[,] Field = new int[,];

        public Grass()
        {
            Size = 0;
        }

        public void Growth()
        {
            size++;
        }

        public void Eaten(int x, int y)
        {
            size -= 2;
        }
    }
}
