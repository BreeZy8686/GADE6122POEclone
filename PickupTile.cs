using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    /// <summary>
    /// Q4.1: Base class for all pickups.
    /// - Extends Tile
    /// - Abstract (no direct instances)
    /// - Constructor forwards Position to Tile
    /// - Declares ApplyEffect(CharacterTile target)
    /// </summary>
    public abstract class PickupTile : Tile
    {
        protected PickupTile(Position position) : base(position) { }

        /// <summary>
        /// Apply this pickup's effect to the given character (e.g., heal).
        /// </summary>
        public abstract void ApplyEffect(CharacterTile target);
        // Note: Display remains abstract from Tile and is implemented by concrete pickups.
    }
}
