using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public class UnitState
    {
        public byte OwnerId;
        public int UnitTypeId;
        public int Id;
        public Coordinates Coordinates;
        public Direction Direction;
        public UnitConfig UnitConfig;
        public AttributesState Attributes;
    }
}