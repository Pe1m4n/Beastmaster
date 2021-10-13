namespace Common.PathFinding
{
    public static class PathFinder
    {
        public static void CalculatePathsData(ref TilesData tilesData, int xStart, int yStart, sbyte distance)
        {
            ref var startTile = ref tilesData.GetTileRef(xStart, yStart);
            startTile.TotalCost = 0;
            
            CalculateNeighbours(ref tilesData, xStart, yStart, 1, distance);
        }

        private static void CalculateNeighbours(ref TilesData tiles, int x, int y, int currentCost, sbyte distance)
        {
            if (currentCost > distance)
                return;
            
            if (IsSuitableTile(ref tiles, x + 1, y, currentCost))
            {
                ref var right = ref tiles.GetTileRef(x + 1, y);
                right.TotalCost = currentCost;
                CalculateNeighbours(ref tiles, right.X, right.Y, currentCost + 1, distance);
            }
            if (IsSuitableTile(ref tiles, x, y + 1, currentCost))
            {
                ref var top = ref tiles.GetTileRef(x, y + 1);
                top.TotalCost = currentCost;
                CalculateNeighbours(ref tiles, top.X, top.Y, currentCost + 1, distance);
            }
            if (IsSuitableTile(ref tiles, x - 1, y, currentCost))
            {
                ref var left = ref tiles.GetTileRef(x - 1, y);
                left.TotalCost = currentCost;
                CalculateNeighbours(ref tiles, left.X, left.Y, currentCost + 1, distance);
            }
            if (IsSuitableTile(ref tiles, x, y - 1, currentCost))
            {
                ref var bottom = ref tiles.GetTileRef(x, y - 1);
                bottom.TotalCost = currentCost;
                CalculateNeighbours(ref tiles, bottom.X, bottom.Y, currentCost + 1, distance);
            }
        }

        private static bool IsSuitableTile(ref TilesData tiles, int x, int y, int currentCost)
        {
            if (x < 0 || y < 0 || x >= tiles.Width || y >= tiles.Height)
            {
                return false;
            }

            ref readonly var tile = ref tiles.GetTileRefReadonly(x, y);
            return !tile.Occupied && (tile.TotalCost > currentCost || tile.TotalCost < 0);
        }
        
        public struct Tile
        {
            public static Tile InvalidTile = new Tile(-1, -1);
            public readonly sbyte X;
            public readonly sbyte Y;
            public bool Occupied;
            public int TotalCost;

            public Tile(sbyte x, sbyte y)
            {
                X = x;
                Y = y;
                Occupied = false;
                TotalCost = -1;
            }
        }

        public struct TilesData
        {
            private readonly Tile[,] _tiles;
            
            public readonly int Width;
            public readonly int Height;
            
            public TilesData(int width, int height)
            {
                Width = width;
                Height = height;
                _tiles = new Tile[width, height];

                for (sbyte x = 0; x < width; x++)
                for (sbyte y = 0; y < height; y++)
                    _tiles[x, y] = new Tile(x, y);
            }

            public ref Tile GetTileRef(int x, int y)
            {
                return ref _tiles[x, y];
            }
            
            public ref readonly Tile GetTileRefReadonly(int x, int y)
            {
                return ref _tiles[x, y];
            }
        }
    }
}