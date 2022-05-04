using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
{
    public class Tile
    {
        public bool isDirt;
        public bool isAir;
        public bool hasAnt;
        public bool isTargeted;
        public int foodCount = 0;
    }
}
