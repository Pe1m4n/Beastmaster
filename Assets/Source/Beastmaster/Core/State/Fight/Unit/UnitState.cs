using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State
{
    public class UnitState
    {
        public int OwnerId { get; set; }
        public int UnitTypeId { get; set; }
        public int Id { get; set; }
        public Coordinates Coordinates { get; set; }
        public Direction Direction { get; set; }
    }
}