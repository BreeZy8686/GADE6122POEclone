using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{

    public abstract class Tile
    {
        public Position Position { get; protected set; }
        public abstract char Display { get; }

        protected Tile(Position position)
        {
            Position = position;
        }
    }
}