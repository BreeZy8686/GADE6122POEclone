using GADE6122;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GADE6122
{
    /// <summary>
    /// Q2.2: Basic enemy type "Grunt".
    /// - Extends EnemyTile
    /// - 10 HP, 1 ATK (set via base constructor)
    /// - Uses the CharacterTile vision array to choose random empty adjacent tiles
    /// - Displays 'X' (alive) or 'x' (dead)
    /// </summary>
    public class GruntTile : EnemyTile
    {
        private static readonly Random _rng = new Random();

        public GruntTile(Position position)
            : base(position, hitPoints: 10, attackPower: 1)
        {
        }

        // 'X' when alive, 'x' when dead
        public override char Display => IsDead ? 'x' : 'X';


        /// <summary>
        /// Picks a random empty tile from the vision array.
        /// If none are empty, returns false and sets out parameter to null.
        /// </summary>
        public override bool GetMove(out Tile? tile)
        {
            // Vision is inherited from CharacterTile (exposed via property)
            var empties = Vision?.Where(t => t is EmptyTile).ToList() ?? new List<Tile>();
            if (empties.Count == 0)
            {
                tile = null;
                return false;
            }

            tile = empties[_rng.Next(empties.Count)];
            return true;
        }

        /// <summary>
        /// If a HeroTile is visible, return it as the only target; otherwise return an empty array.
        /// </summary>
        public override CharacterTile[] GetTargets()
        {
            if (Vision == null) return Array.Empty<CharacterTile>();

            var hero = Vision.OfType<HeroTile>().FirstOrDefault();
            return hero != null ? new CharacterTile[] { hero } : Array.Empty<CharacterTile>();
        }
    }
}