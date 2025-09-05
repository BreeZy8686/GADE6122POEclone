using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    public class GameEngine
    {
        private readonly Random _random = new Random();
        private readonly int _numberOfLevels;
        private int _currentIndex = 0;

        public Level CurrentLevel { get; private set; }

        public GameEngine(int numberOfLevels)
        {
            if (numberOfLevels <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfLevels));
            _numberOfLevels = numberOfLevels;
            // Start with a default sized level
            CurrentLevel = GenerateLevel(20, 10);
        }

        private Level GenerateLevel(int width, int height)
        {
            return new Level(width, height, _random);
        }

        public override string ToString()
        {
            return CurrentLevel.ToString();
        }
        // Tries to move the hero in the requested direction.
        // Returns true if the move succeeded (empty target), false otherwise.
        private bool MoveHero(Direction direction)
        {
            if (direction == Direction.None)
                return false;

            var hero = CurrentLevel.Hero;

            // get the tile next to the hero based on direction:
            // we can use (int)direction because enum values match vision indexes
            int idx = (int)direction;
            Tile target = hero.Vision[idx];

            // if the target tile is not empty, the move fails
            if (target is not EmptyTile)
                return false;

            // swap hero with the empty tile on the level grid
            CurrentLevel.SwapTiles(hero, target);

            // refresh hero's vision after the swap
            hero.UpdateVision(CurrentLevel);

            return true;
        }

        // Public entry point that the Form will call.
        public void TriggerMovement(Direction direction)
        {
            // move the hero; in Part 2 you’ll also move enemies here
            MoveHero(direction);
        }
    }
}
