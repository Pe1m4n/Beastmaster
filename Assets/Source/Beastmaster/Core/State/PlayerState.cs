using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;

namespace Beastmaster.Core.Controllers
{
    public class PlayerState
    {
        public FightState FightState { get; }

        public Coordinates HoveredTile = Coordinates.None;
        public Coordinates SelectedTile = Coordinates.None;

        public int HoveredUnit = FightStateConstants.NO_UNIT;
        public int SelectedUnit = FightStateConstants.NO_UNIT;

        public PlayerState(FightState fightState)
        {
            FightState = fightState;
        }
    }
}