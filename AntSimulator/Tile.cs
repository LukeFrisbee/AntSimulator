using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
{
    internal class Tile
    {
        public int x;
        public int y;

        public bool isDirt;
        public bool isAir;
        public bool isTargeted;


        public int foodCount = 0;

        public bool hasAnt;
    }
}
