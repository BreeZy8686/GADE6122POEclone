using System;
using System.Linq;

namespace GADE6122
{
    /// <summary>
    /// Q2.1: Base class for all enemy types.
    /// - Extends CharacterTile
    /// - Marked abstract (no direct instances)
    /// - Defines standard constructor and abstract behaviours for movement/targeting
    /// </summary>
    public abstract class EnemyTile : CharacterTile
    {
        /// <summary>
        /// Creates an enemy.
        /// All parameters are forwarded to the CharacterTile base constructor.
        /// </summary>
        protected EnemyTile(Position position, int hitPoints, int attackPower)
            : base(position, hitPoints, attackPower)
        {
        }

        /// <summary>
        /// Decide the tile this enemy will move to.
        /// Returns true when a move is available, else false.
        /// </summary>
        /// <param name="tile">Out parameter that will be set to the chosen destination tile (or null if no move).</param>
        public abstract bool GetMove(out Tile? tile);

        /// <summary>
        /// Returns a list of targets this enemy can act upon (e.g. attack).
        /// For Part 2.2 (Grunt), this will return the Hero if in vision, else empty.
        /// </summary>
        public abstract CharacterTile[] GetTargets();
    }
}
