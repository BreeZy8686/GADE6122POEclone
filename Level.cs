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
            Empty,
            Wall,
        }

        public Level(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            InitialiseTiles(width, height);
        }

        public Tile CreateTile(TileType type, Position position)
        {
            switch (type)
            {
                case TileType.Wall:
                    return new WallTile(position);
                case TileType.Empty:
                    return new EmptyTile(position);
                default:
                    throw new ArgumentException("Unknown tile type");
            }
        }

        private Tile CreateTile(TileType type, int x, int y)
        {
            return CreateTile(type, new Position(x, y));
        }

        public void InitialiseTiles(int width, int height)
        {
            _tiles = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Boundary tiles are walls, inner tiles are empty
                    TileType type = (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        ? TileType.Wall
                        : TileType.Empty;
                    _tiles[x, y] = CreateTile(type, x, y);
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