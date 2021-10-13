using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;

namespace Beastmaster.Core.State
{
    public class PlayerState
    {
        public FightState FightState { get; }

        public Coordinates HoveredTile = Coordinates.None;
        public Coordinates SelectedTile = Coordinates.None;

        public int HoveredUnit = FightStateConstants.NO_UNIT;
        public int SelectedUnit = FightStateConstants.NO_UNIT;

        public PathFinder.TilesData CurrentPathData;

        public PlayerState(FightState fightState)
        {
            FightState = fightState;
            CurrentPathData = new PathFinder.TilesData(FightState.FightConfig.LocationWidth,
                FightState.FightConfig.LocationHeight);
        }
    }
}