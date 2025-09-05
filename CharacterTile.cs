using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    // Base class for any character that can stand on a tile (e.g. player, enemy).
    // This class is abstract, so it cannot be created directly — only inherited.
    public abstract class CharacterTile : Tile
    {
        // Current health points of the character.
        protected int hitPoints;

        // Maximum health points the character can ever have.
        protected int maxHitPoints;

        // Attack power value, used when this character attacks another.
        protected int attackPower;

        // Array to store the 4 surrounding tiles (vision).
        // Index 0 = Up, 1 = Right, 2 = Down, 3 = Left
        protected Tile[] vision = new Tile[4];

        // Characters must define their own Display symbol
        // (for example, 'H' for hero, 'E' for enemy).
        public abstract override char Display { get; }

        // Constructor to create a character on the map.
        // Parameters:
        //   position   -> starting location of the character
        //   hitPoints  -> starting health value
        //   attackPower-> damage dealt when attacking
        protected CharacterTile(Position position, int hitPoints, int attackPower)
            : base(position)           // pass position to the base Tile class
        {
            this.hitPoints = hitPoints;        // set starting HP
            this.maxHitPoints = hitPoints;     // at start, max = current HP
            this.attackPower = attackPower;    // set attack power
        }

        // Updates the vision array with the 4 tiles around the character.
        // This looks at the map stored in the Level class.
        public void UpdateVision(Level level)
        {
            int x = Position.X;   // current x position
            int y = Position.Y;   // current y position

            Tile[,] tiles = level.Tiles;   // full grid of tiles in the level

            // Assign the surrounding tiles into the vision array
            vision[0] = tiles[x, y - 1]; // tile above
            vision[1] = tiles[x + 1, y]; // tile to the right
            vision[2] = tiles[x, y + 1]; // tile below
            vision[3] = tiles[x - 1, y]; // tile to the left
        }

        // Reduces HP by the damage amount.
        // HP will never drop below 0.
        public void TakeDamage(int amount)
        {
            if (amount < 0) amount = 0;      // prevent negative damage
            hitPoints -= amount;             // subtract from HP
            if (hitPoints < 0) hitPoints = 0; // clamp HP to 0 minimum
        }

        // Attacks another character by dealing this character's attackPower as damage.
        public void Attack(CharacterTile target)
        {
            if (target == null) return;      // skip if no target
            target.TakeDamage(attackPower);  // apply damage to target
        }

        // Returns true if this character's HP is 0 or less (dead).
        public bool IsDead => hitPoints <= 0;

        // Exposes the vision array to other classes if needed.
        public Tile[] Vision => vision;

        // inside CharacterTile class
        public void MoveTo(Position newPosition)
        {
            Position = newPosition; // allowed because setter is protected in Tile
        }
    }
}