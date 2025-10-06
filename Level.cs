using System;
using System.Text;

namespace GADE6122
{
    // Make TileType visible project-wide (used by Level + GameEngine)
    public enum TileType
    {
        Empty,
        Wall,
        Exit,
        Hero,
        Enemy
    }

    public class Level
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Tile[,] _tiles;
        private readonly Random _random = new Random();

        private HeroTile _hero;
        private ExitTile _exit;
        private EnemyTile[] _enemies = Array.Empty<EnemyTile>();

        // === Public API expected by the rest of the project ===
        public HeroTile Hero => _hero;
        public EnemyTile[] Enemies => _enemies;

        // Some code references Level.Tiles — expose it read-only
        public Tile[,] Tiles => _tiles;

        // Constructor: width, height, number of enemies
        public Level(int width, int height, int numberOfEnemies)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[_width, _height];

            // Build border walls + empty interior
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    bool border = (x == 0 || y == 0 || x == _width - 1 || y == _height - 1);
                    _tiles[x, y] = CreateTile(border ? TileType.Wall : TileType.Empty, new Position(x, y));
                }
            }

            // Place hero and exit
            _hero = new HeroTile(GetRandomEmptyPosition());
            SetTile(_hero);

            _exit = new ExitTile(GetRandomEmptyPosition());
            SetTile(_exit);

            // Place enemies
            _enemies = new EnemyTile[numberOfEnemies];
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var pos = GetRandomEmptyPosition();
                var enemy = (EnemyTile)CreateTile(TileType.Enemy, pos); // GruntTile
                _enemies[i] = enemy;
                SetTile(enemy);
            }

            UpdateVision();
        }

        public Level(int width, int height, int numberOfEnemies, HeroTile existingHero)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[_width, _height];

            // Build border walls + empty interior
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    bool border = (x == 0 || y == 0 || x == _width - 1 || y == _height - 1);
                    _tiles[x, y] = CreateTile(border ? TileType.Wall : TileType.Empty, new Position(x, y));
                }
            }

            // Reuse the SAME hero object (HP and all), just give it a new empty position
            _hero = existingHero;
            var heroPos = GetRandomEmptyPosition();
            _hero.MoveTo(heroPos);
            SetTile(_hero);

            // Place exit
            _exit = new ExitTile(GetRandomEmptyPosition());
            SetTile(_exit);

            // Place enemies
            _enemies = new EnemyTile[numberOfEnemies];
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var pos = GetRandomEmptyPosition();
                var enemy = (EnemyTile)CreateTile(TileType.Enemy, pos); // Grunt
                _enemies[i] = enemy;
                SetTile(enemy);
            }

            UpdateVision();
        }


        // Get a tile from the grid
        public Tile GetTile(Position p) => _tiles[p.X, p.Y];

        // Put a tile in the grid (uses its Position)
        private void SetTile(Tile tile) => _tiles[tile.Position.X, tile.Position.Y] = tile;

        // Update hero + enemy vision arrays
        public void UpdateVision()
        {
            _hero.UpdateVision(this);
            foreach (var e in _enemies)
                e.UpdateVision(this);
        }

        // Factory for tiles (includes Enemy -> GruntTile)
        public Tile CreateTile(TileType type, Position pos)
        {
            return type switch
            {
                TileType.Empty => new EmptyTile(pos),
                TileType.Wall => new WallTile(pos),
                TileType.Exit => new ExitTile(pos),
                TileType.Hero => new HeroTile(pos),
                TileType.Enemy => new GruntTile(pos),   // Q2.2 Grunt
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        // Swap two tiles (your GameEngine calls SwapTiles with an 'a' and 'target')
        public void SwapTiles(Tile a, Tile b)
        {
            var posA = a.Position;
            var posB = b.Position;

            // swap in grid
            _tiles[posA.X, posA.Y] = b;
            _tiles[posB.X, posB.Y] = a;

            // update objects' positions
            SetTilePosition(a, posB);
            SetTilePosition(b, posA);

            // keep vision fresh after movement
            UpdateVision();
        }

        private void SetTilePosition(Tile t, Position p)
        {
            if (t is CharacterTile ct)
                ct.MoveTo(p);
            else
                t.Position = p;
        }

        // Find a random empty location (avoids the border walls)
        public Position GetRandomEmptyPosition()
        {
            while (true)
            {
                int x = _random.Next(1, _width - 1);
                int y = _random.Next(1, _height - 1);
                if (_tiles[x, y] is EmptyTile)
                    return new Position(x, y);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                    sb.Append(_tiles[x, y].Display);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}