using Beastmaster.Core.State.Fight;

namespace Beastmaster.Core.State
{
    public static class PlayerStateExtensions
    {
        public static bool IsPlayerTurn(this PlayerState state)
        {
            return state.PlayerId == state.FightState.Meta.TurnForPlayer;
        }

        public static bool PlayerControlledUnitSelected(this PlayerState state)
        {
            if (!state.FightState.TryGetUnit(state.SelectedUnit, out var unit))
                return false;

            return unit.OwnerId == state.PlayerId;
        }
    }
}