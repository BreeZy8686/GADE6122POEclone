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
            int pickups = 1;
            return new Level(width, height, enemies, pickups);
        }



        // NEW: pick a random inner size within limits
        private (int w, int h) RandomSize()
        {
            int w = _random.Next(MIN_SIZE, MAX_SIZE + 1);
            int h = _random.Next(MIN_SIZE, MAX_SIZE + 1);
            return (w, h);
        }

        // Q3.3 — "current/max" HP for the UI
        public string HeroStats
        {
            get
            {
            var h = CurrentLevel.Hero;
            return $"{h.HitPoints}/{h.MaxHitPoints}";
            }
        }



        // NEW: create a level with random size; optionally reuse the same hero
        // NEW: create a level with random size and correct enemy count;
        // optionally reuse the same hero object
        private Level GenerateLevel(HeroTile? hero = null)
        {
            var (w, h) = RandomSize();
            int enemies = GetEnemiesForLevel(_currentIndex);
            int pickups = 1; // Q4.3 requirement

            return hero == null
                ? new Level(w, h, enemies, pickups)
                : new Level(w, h, enemies, pickups, hero); // reuse hero; HP preserved
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

        // Hero attacks; if attack happened, enemies retaliate.
        // Then check for hero death and set GameOver.
        public void TriggerAttack(Direction direction)
        {
            if (_state == GameState.GameOver || _state == GameState.Complete)
                return;

            bool success = HeroAttack(direction);

            if (success)
            {
                EnemiesAttack();

                // Q3.3 — check if hero has died after counters
                if (CurrentLevel.Hero.IsDead)
                {
                    _state = GameState.GameOver;
                }
            }
        }

        

       



        // Tries to move the hero in the requested direction.
        // Returns true if the move succeeded (empty target), false otherwise.
        private bool MoveHero(Direction direction)
        {
            if (direction == Direction.None)
                return false;

            var hero = CurrentLevel.Hero;

            // 0=Up,1=Right,2=Down,3=Left
            int idx = (int)direction;
            Tile target = hero.Vision[idx];

            // Exit: advance or complete
            if (target is ExitTile)
            {
                bool isLastLevel = (_currentIndex >= _numberOfLevels - 1);
                if (isLastLevel)
                {
                    _state = GameState.Complete;
                    return false; // per brief: return false on last level
                }

                NextLevel();
                return true;
            }

            // Pickup — apply, swap, then clear the old hero cell so it doesn't respawn
            if (target is PickupTile pickup)
            {
                var from = hero.Position;          // remember where hero came from
                pickup.ApplyEffect(hero);          // apply effect (e.g., heal 10)
                CurrentLevel.SwapTiles(hero, pickup);   // puts pickup into 'from' cell
                CurrentLevel.ReplaceWithEmpty(from);    // remove pickup left behind
                return true;
            }

            // Normal move only allowed into Empty
            if (target is EmptyTile)
            {
                CurrentLevel.SwapTiles(hero, target);
                hero.UpdateVision(CurrentLevel);
                return true;
            }

            // Blocked by wall/enemy/etc.
            return false;
        }




        // Q3.2 — Enemies attack the hero (and any valid targets) after a successful hero attack.
        private void EnemiesAttack()
        {
            foreach (var enemy in CurrentLevel.Enemies)
            {
                if (enemy == null || enemy.IsDead)
                    continue;

                // Ask the enemy who it can attack (per your GruntTile.GetTargets implementation)
                var targets = enemy.GetTargets();
                if (targets == null || targets.Length == 0)
                    continue;

                // Attack all available targets (brief says: loop over each target and invoke Attack)
                foreach (var target in targets)
                {
                    if (target != null && !enemy.IsDead && !target.IsDead)
                        enemy.Attack(target);
                }
            }

            // Optional: keep vision in sync if your HUD relies on it
            CurrentLevel.UpdateVision();
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


        // Guard: stop if game is already over
        public void TriggerMovement(Direction direction)
        {
            if (_state == GameState.GameOver || _state == GameState.Complete)
                return;

            bool moved = MoveHero(direction);

            // keep enemies moving every time the hero moves
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
                return "🎉 You reached the exit on the last level.\nGame complete! 🎉";

            if (_state == GameState.GameOver)
                return "Game Over! The hero has fallen.";

            return CurrentLevel.ToString();
        }

    }
}
