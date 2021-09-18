using UnityEngine;

namespace Common.PathFinding
{
    public class PathFinder
    {
        private readonly Tile[,] _tiles;
        private bool _populated;

        public PathFinder(int width, int height)
        {
            _tiles = new Tile[width, height];
            for (int x = 0; x < _tiles.GetLength(0); x++)
            for (int y = 0; y < _tiles.GetLength(1); y++)
                _tiles[x, y] = new Tile(x, y);
        }
        
        public void CalculatePathsData(int xStart, int yStart, int distance, bool[,] occupationData)
        {
            Clear();
            if (!TryGetSuitableTile(xStart, yStart, 0, out var startTile))
                return;
            
            Debug.Assert(occupationData.GetLength(0) == _tiles.GetLength(0) 
                         && occupationData.GetLength(1) == _tiles.GetLength(1));
            
            for (int x = 0; x < occupationData.GetLength(0); x++)
            for (int y = 0; y < occupationData.GetLength(1); y++)
            {
                _tiles[x, y].Occupied = occupationData[x, y];
            }
            
            startTile.TotalCost = 0;
            CalculateNeighbours(xStart, yStart, 1, distance);
            _populated = true;
        }

        public bool TryGetPathsData(out Tile[,] data)
        {
            data = _tiles;
            return _populated;
        }

        public void Clear()
        {
            for (int x = 0; x < _tiles.GetLength(0); x++)
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                var tile = _tiles[x, y];
                tile.Occupied = false;
                tile.TotalCost = int.MaxValue;
            }

            _populated = false;
        }

        private void CalculateNeighbours(int x, int y, int currentCost, int distance)
        {
            if (currentCost > distance)
                return;
            
            if (TryGetSuitableTile(x + 1, y, currentCost, out var right))
                right.TotalCost = currentCost;
            if (TryGetSuitableTile(x, y + 1, currentCost, out var top))
                top.TotalCost = currentCost;
            if (TryGetSuitableTile(x - 1, y, currentCost, out var left))
                left.TotalCost = currentCost;
            if (TryGetSuitableTile(x, y - 1, currentCost, out var bottom))
                bottom.TotalCost = currentCost;

            currentCost++;
            if (right != null)
                CalculateNeighbours(right.X, right.Y, currentCost, distance);
            if (top != null)
                CalculateNeighbours(top.X, top.Y, currentCost, distance);
            if (left != null)
                CalculateNeighbours(left.X, left.Y, currentCost, distance);
            if (bottom != null)
                CalculateNeighbours(bottom.X, bottom.Y, currentCost, distance);
        }

        private bool TryGetSuitableTile(int x, int y, int currentCost, out Tile tile)
        {
            tile = null;
            if (x < 0 || y < 0 || x >= _tiles.GetLength(0) || y >= _tiles.GetLength(1))
            {
                return false;
            }

            tile = _tiles[x, y];
            if (tile.Occupied || tile.TotalCost <= currentCost)
            {
                tile = null;
                return false;
            }
            
            return true;
        }
        
        public class Tile
        {
            public readonly int X;
            public readonly int Y;
            public bool Occupied;
            public int TotalCost = int.MaxValue;

            public Tile(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}