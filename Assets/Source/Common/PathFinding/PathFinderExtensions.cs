namespace Common.PathFinding
{
    public static class PathFinderExtensions
    {
        public static bool AvailableForPathing(this PathFinder.TilesData pathData, int x, int y)
        {
            if (x < 0 || y < 0 || x >= pathData.Width || y >= pathData.Height)
                return false;
            
            ref readonly var tile = ref pathData.GetTileRefReadonly(x, y);
            return tile.TotalCost > 0 && !tile.Occupied;
        }
    }
}