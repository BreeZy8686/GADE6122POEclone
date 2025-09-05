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
    }
}
