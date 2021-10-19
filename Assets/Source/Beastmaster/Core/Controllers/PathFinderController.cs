using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;

namespace Beastmaster.Core.Controllers
{
    public class PathFinderController
    {
        private int _lastSelectedUnit = FightStateConstants.NO_UNIT;
        private Coordinates _lastSelectedUnitCoordinates = Coordinates.None;

        public void Tick(PlayerState state)
        {
            state.FightState.TryGetUnit(state.SelectedUnit, out var unitState);

            var shouldCalculatePath = unitState != null 
                                      && (_lastSelectedUnit != unitState.Id || !_lastSelectedUnitCoordinates.Equals(unitState.Coordinates))
                                      && unitState.OwnerId == state.PlayerId;

            if (shouldCalculatePath)
            {
                state.CurrentPathData.Populate(state.FightState);
                PathFinder.CalculatePathsData(ref state.CurrentPathData, unitState.Coordinates.X,
                    unitState.Coordinates.Y, (sbyte)unitState.Attributes.Values[AttributeType.MovePoints]);
            }

            _lastSelectedUnit = unitState?.Id ?? FightStateConstants.NO_UNIT;
            _lastSelectedUnitCoordinates = unitState?.Coordinates ?? Coordinates.None;
        }
    }
}