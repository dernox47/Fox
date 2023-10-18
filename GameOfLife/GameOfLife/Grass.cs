using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Grass
    {
        const int row = 10; //Pálya magassága
        const int col = 10; //Pálya szélessége
        private int size;
        public int posX { get; init; }
        public int posY { get; init; }

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

        //public void Growth()
        //{
        //    for (int i = 0; i < ; i++)
        //    {
        //        for (int j = 0; j < ; j++)
        //        {
        //            map.FieldMatrix[i, j] += 1;
        //        }
        //    }
        //}

        public void Eaten()
        {
            Size = 0;
        }
    }
}
