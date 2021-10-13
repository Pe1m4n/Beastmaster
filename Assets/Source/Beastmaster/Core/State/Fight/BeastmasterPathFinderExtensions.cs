using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;
using UnityEngine;

namespace Beastmaster.Core.Controllers
{
    public static class BeastmasterPathFinderExtensions
    {
        public static void Populate(this ref PathFinder.TilesData tiles, FightState fightState)
        {
            foreach (var tileState in fightState.Tiles)
            {
                ref var tile = ref tiles.GetTileRef(tileState.Coordinates.X, tileState.Coordinates.Y);
                tile.Occupied = tileState.OccupantId != FightStateConstants.TILE_NOT_OCCUPIED;
                tile.TotalCost = -1;
            }
        }
        
        public static bool TryFillPath(this PathFinder.TilesData tiles, Path path,
            Coordinates from, Coordinates to)
        {
            if (!tiles.AvailableForPathing(to.X, to.Y))
                return false;
            
            ref readonly var destinationTile = ref tiles.GetTileRefReadonly(to.X, to.Y);
            ref readonly var lastTile = ref destinationTile;
            
            path.Add(to);
            while (lastTile.TotalCost > 1)
            {
                lastTile = ref tiles.GetNextNeighbour(lastTile);
                if (lastTile.Equals(PathFinder.Tile.InvalidTile))
                    return false;
                path.Add( new Coordinates() { X = lastTile.X, Y = lastTile.Y });
            }
            path.Add(from);

            return true;
        }

        private static unsafe ref readonly PathFinder.Tile GetNextNeighbour(this PathFinder.TilesData tiles, PathFinder.Tile target)
        {
            const byte count = 4;
            var neighbours = new PathFinder.Tile[count];//TODO: fix stackalloc magic

            neighbours[0] = tiles.GetTileOrInvalidTile(target.X + 1, target.Y);
            neighbours[1] = tiles.GetTileOrInvalidTile(target.X, target.Y + 1);
            neighbours[2] = tiles.GetTileOrInvalidTile(target.X - 1, target.Y);
            neighbours[3] = tiles.GetTileOrInvalidTile(target.X, target.Y - 1);

            for (byte i = 0; i < count; i++)
            {
                if (neighbours[i].TotalCost == target.TotalCost - 1)
                    return ref neighbours[i];
            }
            Debug.LogAssertion("Couldn't find any walkable neighbour");
            return ref PathFinder.Tile.InvalidTile;
        }

        private static ref readonly PathFinder.Tile GetTileOrInvalidTile(this PathFinder.TilesData tiles, int x, int y)
        {
            if (tiles.AvailableForPathing(x, y))
                return ref tiles.GetTileRefReadonly(x, y);

            return ref PathFinder.Tile.InvalidTile;
        }
    }
}