using UnityEngine;

namespace Common.PathFinding
{
    public static class PathFinderExtensions
    {
        public static bool AvailableForPathing(this PathFinder.Tile[,] pathData, int x, int y)
        {
            Debug.Assert(x >= 0 && y >= 0 && x < pathData.GetLength(0) && y < pathData.GetLength(1));
            return pathData[x, y].TotalCost < int.MaxValue && !pathData[x, y].Occupied;
        }
    }
}