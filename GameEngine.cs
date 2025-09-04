using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    public class GameEngine
    {
        // Fields
        private Level currentLevel;   // stores the current level being played
        private int numberOfLevels;   // stores the number of levels in the game
        private Random random;        // used for rolling random numbers

        // Constants
        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 20;

        // Constructor
        public GameEngine(int numberOfLevels)
        {
            this.numberOfLevels = numberOfLevels;
            random = new Random();

            // Create a new level with random width and height
            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);

            currentLevel = new Level(width, height);
        }

        // Property to access the current level
        public Level CurrentLevel
        {
            get { return currentLevel; }
        }

        // Override ToString() to return the current level's string form
        public override string ToString()
        {
            return currentLevel.ToString();
        }

        
    }
}