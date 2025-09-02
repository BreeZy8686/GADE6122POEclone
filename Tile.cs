using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    
    public abstract class Tile
    {
        // Fields
        private Position _position;

        // Constructor
        protected Tile(Position position)
        {
            _position = position;
        }

        // Properties
        public int X => _position.X;

        public int Y => _position.Y;

        public abstract char Display { get; }
    }
}