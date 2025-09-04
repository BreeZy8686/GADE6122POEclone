using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    public class EmptyTile : Tile
    {
        public EmptyTile(Position position) : base(position)
        {
        }

        public override char Display => '.';
    }
}