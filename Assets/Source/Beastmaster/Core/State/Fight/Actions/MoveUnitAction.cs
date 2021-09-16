using Beastmaster.Core.Primitives;
using UnityEngine;

namespace Beastmaster.Core.State
{
    public class MoveUnitAction : AbstractStateAction<FightState, MoveUnitAction.Data>
    {
        public class Data : ActionData
        {
            public readonly int UnitId;
            public readonly Coordinates MoveTo;
            public readonly Coordinates MoveFrom;
                
            public Data(int unitId, Coordinates moveTo, Coordinates moveFrom, bool immutable = false) : base(immutable)
            {
                UnitId = unitId;
                MoveTo = moveTo;
                MoveFrom = moveFrom;
            }
        }

        protected override void Execute(FightState state, Data data)
        {
            if (!state.TryGetUnit(data.UnitId, out var unit))
            {
                Debug.LogError($"{nameof(MoveUnitAction)} Error: Couldn't get unit with id {data.UnitId}");
            }
            var origin = state.GetTile(data.MoveFrom);
            var destination = state.GetTile(data.MoveTo);
            
            Debug.Assert(unit.Coordinates.Equals(data.MoveFrom));
            Debug.Assert(destination.OccupantId == FightStateConstants.TILE_NOT_OCCUPIED);

            origin.OccupantId = FightStateConstants.TILE_NOT_OCCUPIED;
            destination.OccupantId = unit.Id;
            unit.Coordinates = data.MoveTo;
        }
    }
}