using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public sealed class SpawnUnitAction : AbstractStateAction<FightState, SpawnUnitAction.Data>
    {
        public class Data : ActionData
        {
            public readonly int PlayerId;
            public readonly int UnitTypeId;
            public readonly Coordinates Coordinates;

            public Data(int playerId, int unitTypeId, Coordinates coordinates, bool immutable = false) : base(immutable)
            {
                PlayerId = playerId;
                UnitTypeId = unitTypeId;
                Coordinates = coordinates;
            }
        }

        protected override void Execute(FightState state, Data data)
        {
            state.AddUnit(data.UnitTypeId, data.PlayerId, data.Coordinates);   
        }
    }
}