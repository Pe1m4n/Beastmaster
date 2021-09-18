using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Common.PathFinding;

namespace Beastmaster.Core.Controllers
{
    public class PathFinderController
    {
        private readonly PathFinder _pathFinder;
        private readonly bool[,] _occupationData;
        
        private int _lastSelectedUnit = -1;
        private Coordinates _lastSelectedUnitCoordinates = Coordinates.None;

        public PathFinderController(PathFinder pathFinder, FightConfig fightConfig)
        {
            _pathFinder = pathFinder;
            _occupationData = new bool[fightConfig.LocationWidth, fightConfig.LocationHeight];
        }

        public void Tick(PlayerState state)
        {
            for (int x = 0; x < _occupationData.GetLength(0); x++)
            for (int y = 0; y < _occupationData.GetLength(1); y++)
            {
                _occupationData[x, y] = state.FightState.GetTile(new Coordinates{X = x, Y = y}).OccupantId != FightStateConstants.TILE_NOT_OCCUPIED;
            }
            
            state.FightState.TryGetUnit(state.SelectedUnit, out var unitState);
            if (_lastSelectedUnit != state.SelectedUnit)
            {
                if (unitState != null)
                    _pathFinder.CalculatePathsData(unitState.Coordinates.X, unitState.Coordinates.Y, FightStateConstants.TEMP_UNIT_SPEED, _occupationData);
                else
                    _pathFinder.Clear();
                _lastSelectedUnit = state.SelectedUnit;
                _lastSelectedUnitCoordinates = unitState?.Coordinates?? Coordinates.None;
            }

            if (unitState == null || _lastSelectedUnitCoordinates.Equals(unitState.Coordinates)) 
                return;
            
            _pathFinder.CalculatePathsData(unitState.Coordinates.X, unitState.Coordinates.Y, FightStateConstants.TEMP_UNIT_SPEED, _occupationData);
            _lastSelectedUnitCoordinates = unitState.Coordinates;
        }
    }
}