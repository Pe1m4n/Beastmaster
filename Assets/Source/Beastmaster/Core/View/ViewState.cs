using System.Collections.Generic;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.View.Units;

namespace Beastmaster.Core.View
{
    public class ViewState
    {
        public FightState FightState;
        public PlayerState PlayerState { get; }
        public List<UnitViewState> UnitViewStates { get; } = new List<UnitViewState>();

        public ViewState(FightState fightState, PlayerState playerState)
        {
            FightState = fightState;
            PlayerState = playerState;
        }
    }
}