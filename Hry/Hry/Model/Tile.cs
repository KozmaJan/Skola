using System;
using System.Collections.Generic;
using System.Text;

namespace Hry.Model
{
    internal class Tile
    {
        public int x { get; }
        public int y { get; }
        public bool bomb;
        public Tile(int x, int y, bool bomb) { 
            this.x = x;
            this.y = y;
            this.bomb = bomb;
        }
    }
}
