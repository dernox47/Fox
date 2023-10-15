//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//namespace GameOfLife
//{
//    internal class Fox
//    {
//        public int Fullness { get; init; }
//        public int PozX { get; init; }
//        public int PozY { get; init; }

//        public Fox(int pozX, int poxY)
//        {
//            PozX = pozX;
//            PozY = poxY;
//        }

//        public int NeighboorFox(int y, int x)
//        {
//            bool anotherfox = false;
//            bool emptyplace = false;
//            char[,] foxmap = new char[5, 5];
//            int foxmapy = 0;
//            int foxmapx = 0;
//            for (int i = -2; i < 3; i++)
//            {
//                for (int j = -2; j < 3; j++)
//                {
//                    if (i != 0 || j != 0)
//                    {
//                            foxmap[foxmapy, foxmapx] += map[y - i, x - j];
//                    }
//                    foxmapx++;
//                }
//                foxmapy++;
//            }
//        }
//    }
//}