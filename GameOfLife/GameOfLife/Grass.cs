using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Grass
    {
        public int posX { get; init; }
        public int posY { get; init; }

        //mező: maximum növés 2 (0 -> 1, 1 -> 2) (?)
        
        private int size;
        public int Size
        {
            get { return size; }
            set 
            {
                if (value < 0) size = 0;
                else if (value > 2) size = 2;
                else size = value;
            }
        }
        
        public Grass(int posX, int posY)
        {
        Size = 0;
        this.posX = posX;
        this.posY = posY;
        }

    }
}
