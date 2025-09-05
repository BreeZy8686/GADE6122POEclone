using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    // Possible movement directions for the hero (and later, enemies).
    // Values match the indexes of the vision array in CharacterTile.
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        None = 4


    }
}