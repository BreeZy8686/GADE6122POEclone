using System;
using System.Text;

namespace GADE6122
{
    public class Level
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Tile[,] _tiles;
        private readonly Random _random;

        public int Width => _width;
        public int Height => _height;
        public Tile[,] Tiles => _tiles;

        public Level(int width, int height, Random? random = null)
        {
            if (width < 3 || height < 3)
                throw new ArgumentException("Level must be at least 3x3 to have walls.");
            _width = width;
            _height = height;
            _tiles = new Tile[_width, _height];
            _random = random ?? new Random();

            InitialiseTiles();
        }

        // Builds the map: walls on the outer border, empty tiles inside.
        private void InitialiseTiles()
        {
            for (int y = 0; y < _height; y++)           // go through each row
            {
                for (int x = 0; x < _width; x++)        // go through each column
                {
                    // true when the cell is on any outer edge of the grid
                    bool isBorder = (x == 0) || (y == 0) || (x == _width - 1) || (y == _height - 1);

                    // place a WallTile at borders, otherwise an EmptyTile
                    _tiles[x, y] = isBorder
                        ? new WallTile(new Position(x, y))
                        : new EmptyTile(new Position(x, y));
                }
            }
        }

        // Converts the grid to a text map for display in a label/console.
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            for (int y = 0; y < _height; y++)           // include the final row (y < _height)
            {
                for (int x = 0; x < _width; x++)        // include the final column
                {
                    sb.Append(_tiles[x, y].Display);    // write each tile's display char
                }
                sb.AppendLine();                         // new line after every row
            }

            return sb.ToString();                        // do NOT TrimEnd() here
        }
    }
}
