using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State
{
    public class TileState
    {
        public int OccupantId { get; set; } = -1;
        public Coordinates Coordinates;

        public TileState(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }
    }
}