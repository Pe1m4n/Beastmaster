using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State
{
    public static class TileStateExtensions
    {
        public static TileState GetTile(this FightState state, Coordinates coordinates)
        {
            return state.Tiles[state.FightConfig.LocationWidth * coordinates.X + coordinates.Y];
        }
    }
}