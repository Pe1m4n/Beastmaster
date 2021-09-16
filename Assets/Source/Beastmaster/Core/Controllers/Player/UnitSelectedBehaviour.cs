using System.Collections.Generic;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Common.StateMachine;

namespace Beastmaster.Core.Controllers
{
    public class UnitSelectedBehaviour : StateBehaviour<EStateType, EActionType, (PlayerState PlayerState, List<ActionData> Actions)>
    {
        private readonly IFightInputProvider _fightInputProvider;

        public UnitSelectedBehaviour(IFightInputProvider fightInputProvider)
        {
            _fightInputProvider = fightInputProvider;
        }

        public override void Tick((PlayerState PlayerState, List<ActionData> Actions) context)
        {
            var playerState = context.PlayerState;
            playerState.HoveredTile = _fightInputProvider.GetTileUnderCursor();
            playerState.HoveredUnit = _fightInputProvider.GetUnitIdUnderCursor();

            if (_fightInputProvider.LMBClicked())
            {
                playerState.SelectedTile = playerState.HoveredTile;
                playerState.SelectedUnit = playerState.HoveredUnit;
            }

            if (_fightInputProvider.RMBClicked() 
                && !playerState.HoveredTile.Equals(Coordinates.None)
                && playerState.FightState.GetTile(playerState.HoveredTile).OccupantId == FightStateConstants.TILE_NOT_OCCUPIED)
            {
                var unitState = playerState.FightState.Units[playerState.SelectedUnit];
                context.Actions.Add(new MoveUnitAction.Data(
                    playerState.SelectedUnit,
                    playerState.HoveredTile,
                    unitState.Coordinates));
            }

            if (playerState.SelectedUnit == FightStateConstants.NO_UNIT)
            {
                ActionPerformed(EActionType.UnitDeselected);
            }
        }
    }
}