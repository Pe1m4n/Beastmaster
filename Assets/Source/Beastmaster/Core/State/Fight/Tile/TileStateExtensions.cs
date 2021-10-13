using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public static class TileStateExtensions
    {
        public static TileState GetTile(this FightState state, Coordinates coordinates)
        {
            return state.Tiles[state.FightConfig.LocationWidth * coordinates.Y + coordinates.X];
        }

        public static bool TryGetTile(this FightState state, Coordinates coordinates, out TileState tileState)
        {
            tileState = null;
            if (coordinates.X < 0 || coordinates.Y < 0 || coordinates.X >= state.FightConfig.LocationWidth ||
                coordinates.Y >= state.FightConfig.LocationHeight)
            {
                return false;
            }

            tileState = GetTile(state, coordinates);
            return true;
        }
    }
}