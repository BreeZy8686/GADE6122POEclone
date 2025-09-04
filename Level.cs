using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    public class Level
    {
        private Tile[,] _tiles;
        private int _width;
        private int _height;

        public int Width => _width;
        public int Height => _height;

        public enum TileType
        {
            Empty
        }

        public Level(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            InitialiseTiles();
        }

        private Tile CreateTile(TileType type, Position position)
        {
            Tile tile = type switch
            {
                TileType.Empty => new EmptyTile(position),
                 => throw new ArgumentException("Invalid TileType")
            };
            _tiles[position.X, position.Y] = tile;
            return tile;
        }

        // Overloaded convenience method
        private Tile CreateTile(TileType type, int x, int y)
        {
            return CreateTile(type, new Position(x, y));
        }

        private void InitialiseTiles()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    CreateTile(TileType.Empty, x, y);
                }
            }
        }

        public override string ToString()
        {
            var result = new System.Text.StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    result.Append(_tiles[x, y].Display);
                }
                result.Append('\n');
            }
            return result.ToString();
        }
    }
}