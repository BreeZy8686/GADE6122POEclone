using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{

    public abstract class Tile
    {
        // location of this tile on the map
        public Position Position { get; set; }

        // single character used to draw the tile
        public abstract char Display { get; }

        // constructor: sets starting position
        protected Tile(Position position)
        {
            Position = position;
        }
    }
}