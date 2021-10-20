using System;
using Zenject;

namespace Beastmaster.Core.State.Fight
{
    public class LocalFightStateHandler : ITickable
    {
        private readonly FightStateContainer _stateContainer;

        private readonly UnitsSystem _unitsSystem = new UnitsSystem();

        public LocalFightStateHandler(FightStateContainer stateContainer)
        {
            _stateContainer = stateContainer;
        }

        public void Tick()
        {
            var state = _stateContainer.State;
            
            if (state.Meta.TurnEnd > DateTime.Now)
            {
                return;
            }
            
            _unitsSystem.Turn(state);
            
            var leftPlayerId = state.FightConfig.LeftPlayerData.PlayerId;
            var rightPlayerId = state.FightConfig.RightPlayerData.PlayerId;
            state.Meta.TurnForPlayer = state.Meta.TurnForPlayer == leftPlayerId ? rightPlayerId : leftPlayerId;
            state.Meta.TurnEnd = state.Meta.TurnEnd.AddSeconds(state.FightConfig.TurnTimeSeconds);
        }
    }
}