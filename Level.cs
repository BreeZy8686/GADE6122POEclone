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

        // store the hero reference
        private HeroTile _hero;

        // store the exit reference
        private ExitTile _exit;

        public int Width => _width;
        public int Height => _height;
        public Tile[,] Tiles => _tiles;

        // expose hero as read-only
        public HeroTile Hero => _hero;

        // expose exit as read-only
        public ExitTile Exit => _exit;

        // <<< added: enum with Hero value
        public enum TileType { Empty, Wall, Hero,
            Exit
        }

        // <<< CHANGED SIGNATURE: optional HeroTile parameter with default null
        public Level(int width, int height, Random? random = null, HeroTile? hero = null)
        {
            if (width < 3 || height < 3)
                throw new ArgumentException("Level must be at least 3x3 to have walls.");
            _width = width;
            _height = height;
            _tiles = new Tile[_width, _height];
            _random = random ?? new Random();

            InitialiseTiles();      // builds walls + empty interior

            // After building the grid, pick a random empty cell for the hero
            Position pos = GetRandomEmptyPosition();

            if (hero == null)
            {
                // No hero passed in -> create a new hero at the random position
                _hero = (HeroTile)CreateTile(TileType.Hero, pos);
            }
            else
            {
                // Reuse existing hero -> move it to the random position
                hero.MoveTo(pos);          // Position must be settable in Tile
                _hero = hero;
            }

            // Place the hero into the tiles array so it renders on the map
            _tiles[pos.X, pos.Y] = _hero;

            // Optional: update the hero's vision based on current map
            _hero.UpdateVision(this);

            // find another random empty cell and place the exit there
            Position exitPos = GetRandomEmptyPosition();
            _exit = (ExitTile)CreateTile(TileType.Exit, exitPos);
            _tiles[exitPos.X, exitPos.Y] = _exit;
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

        // <<< added: small factory to create tiles by type
        private Tile CreateTile(TileType type, Position pos)
        {
            return type switch
            {
                TileType.Wall => new WallTile(pos),
                TileType.Hero => new HeroTile(pos),
                TileType.Exit => new ExitTile(pos), 
                _ => new EmptyTile(pos),
            };
        }

        // <<< added: find a random empty position inside the map (not a wall)
        private Position GetRandomEmptyPosition()
        {
            while (true)
            {
                int x = _random.Next(1, _width - 1);   // avoid border walls
                int y = _random.Next(1, _height - 1);

                if (_tiles[x, y] is EmptyTile)
                    return new Position(x, y);
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
        // Swaps the positions of two tiles on the grid.
        // Also updates their Position values to match the swap.
        public void SwapTiles(Tile a, Tile b)
        {
            // read current positions
            Position pa = a.Position;
            Position pb = b.Position;

            // swap them in the 2D array
            _tiles[pa.X, pa.Y] = b;
            _tiles[pb.X, pb.Y] = a;

            // update the tiles' Position values
            // (Position setter is protected in Tile, but CharacterTile exposes MoveTo;
            // for non-character tiles, we assign via a small helper below.)
            SetTilePosition(a, pb);
            SetTilePosition(b, pa);
        }

        // Helper to update a tile's Position (works for any Tile).
        private void SetTilePosition(Tile t, Position p)
        {
            // Characters have MoveTo, empty/wall do not — so we handle both cases.
            if (t is CharacterTile ct)
                ct.MoveTo(p);
            else
                // For basic tiles, we need a small setter path. If your Tile already
                // has a public/protected set, this will compile. If not, tell me and
                // I’ll give you a tiny internal setter method.
                t.Position = p;
        }


    }
}