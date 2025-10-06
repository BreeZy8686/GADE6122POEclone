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

        private GameState _state = GameState.InProgress;

        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 20;

        public GameEngine(int numberOfLevels)
        {
            _numberOfLevels = numberOfLevels;
            CurrentLevel = GenerateLevel();   // random size
        }


        private Level GenerateLevel(int width, int height)
        {
            int enemies = GetEnemiesForLevel(_currentIndex);
            return new Level(width, height, enemies);
        }


        // NEW: pick a random inner size within limits
        private (int w, int h) RandomSize()
        {
            int w = _random.Next(MIN_SIZE, MAX_SIZE + 1);
            int h = _random.Next(MIN_SIZE, MAX_SIZE + 1);
            return (w, h);
        }


        // NEW: create a level with random size; optionally reuse the same hero
        // NEW: create a level with random size and correct enemy count;
        // optionally reuse the same hero object
        private Level GenerateLevel(HeroTile? hero = null)
        {
            var (w, h) = RandomSize();
            int enemies = GetEnemiesForLevel(_currentIndex);
            return new Level(w, h, enemies);
        }


        // Moves to the next level, carrying the same HeroTile object across.
        private void NextLevel()
        {
            _currentIndex++;                                // advance index
            HeroTile hero = CurrentLevel.Hero;              // keep the same hero object
            CurrentLevel = GenerateLevel(hero);             // new level, same hero
        }

        // Q3.1 — Hero attacks in a direction.
        // Returns true if an attack happened (i.e., a CharacterTile was in that direction).
        private bool HeroAttack(Direction direction)
        {
            if (direction == Direction.None)
                return false;

            var hero = CurrentLevel.Hero;

            // 0=Up, 1=Right, 2=Down, 3=Left (same indexing used for movement)
            int idx = (int)direction;
            Tile target = hero.Vision[idx];

            // Only attack if there is a character there (e.g., an enemy)
            if (target is CharacterTile character)
            {
                hero.Attack(character);   // uses your character attack logic
                return true;
            }

            return false; // nothing to attack
        }

        // Q3.1 — called by the form to trigger an attack.
        public void TriggerAttack(Direction direction)
        {
            bool success = HeroAttack(direction);

            // Q3.2 will add enemy counter-attacks here:
            // if (success) { EnemiesAttack(); }
        }

        // Tries to move the hero in the requested direction.
        // Returns true if the move succeeded (empty target), false otherwise.
        private bool MoveHero(Direction direction)
        {
            if (direction == Direction.None)
                return false;

            var hero = CurrentLevel.Hero;

            // pick target using vision index (0=Up,1=Right,2=Down,3=Left)
            int idx = (int)direction;
            Tile target = hero.Vision[idx];

            // NEW: if target is exit, either complete the game or go to next level
            if (target is ExitTile)
            {
                bool isLastLevel = (_currentIndex >= _numberOfLevels - 1);

                if (isLastLevel)
                {
                    _state = GameState.Complete;  // mark game complete
                    return false;                 // per brief: return false on last level
                }

                NextLevel();                      // go to next level
                return true;                      // successful "move"
            }

            // original empty-check
            if (target is not EmptyTile)
                return false;

            // do the swap and refresh vision
            CurrentLevel.SwapTiles(hero, target);
            hero.UpdateVision(CurrentLevel);
            return true;
        }

        private void MoveEnemies()
        {
            // Loop over each enemy in the current level
            foreach (var enemy in CurrentLevel.Enemies)
            {
                if (enemy.IsDead)
                    continue; // skip dead enemies

                // Ask the enemy for its move
                if (enemy.GetMove(out Tile? target) && target is EmptyTile)
                {
                    // swap the enemy with the target tile
                    CurrentLevel.SwapTiles(enemy, target);
                }
            }

            // refresh everyone’s vision after all movement
            CurrentLevel.UpdateVision();
        }


        // Public entry point that the Form will call.
        public void TriggerMovement(Direction direction)
        {
            // Move the hero first
            bool heroMoved = MoveHero(direction);

            // After the hero moves, move each enemy
            MoveEnemies();
        }


        private int GetEnemiesForLevel(int levelIndex)
        {
            // Level 1 → 1 enemy, Level 2 → 2 enemies, caps at 3
            int n = 1 + levelIndex;
            return n > 3 ? 3 : n;
        }

        public override string ToString()
        {
            if (_state == GameState.Complete)
                return "🎉 You reached the exit on the last level.\n Game complete! 🎉";

            // GameOver will be handled later; for now treat as in progress
            return CurrentLevel.ToString();
        }
    }
}
