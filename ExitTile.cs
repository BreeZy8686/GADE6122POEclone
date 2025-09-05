using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    
        // Exit tile the hero must reach to go to the next level.
        public class ExitTile : Tile
        {
            public ExitTile (Position position) : base(position) { }

            public override char Display => 'E';  
        }
    }

