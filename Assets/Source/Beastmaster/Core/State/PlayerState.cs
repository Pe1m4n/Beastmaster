using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;

namespace Beastmaster.Core.State
{
    public class PlayerState
    {
        public byte PlayerId => FightState.Meta.TurnForPlayer; //TODO: May cause bugs. For debug purposes only. Remove later. 
        public FightState FightState;

        public Coordinates HoveredTile = Coordinates.None;
        public Coordinates SelectedTile = Coordinates.None;

        public int HoveredUnit = FightStateConstants.NO_UNIT;
        public int SelectedUnit = FightStateConstants.NO_UNIT;

        public PathFinder.TilesData CurrentPathData;

        public PlayerState(byte playerId, FightState fightState) 
        {
            //PlayerId = playerId;
            FightState = fightState;
            CurrentPathData = new PathFinder.TilesData(FightState.FightConfig.LocationWidth,
                FightState.FightConfig.LocationHeight);
        }
    }
}