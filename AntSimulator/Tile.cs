﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
{
    public class Tile
    {
        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;

        public bool isDirt;
        public bool isAir;
        public List<Ant> ants = new List<Ant>();
        public int foodCount = 0;
    }
}
