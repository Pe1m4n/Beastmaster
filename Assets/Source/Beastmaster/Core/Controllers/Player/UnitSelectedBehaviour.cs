using System.Collections.Generic;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;
using Common.StateMachine;
using UnityEngine;

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
                && playerState.FightState.TryGetUnit(playerState.SelectedUnit, out var unitState)
                && playerState.FightState.TryGetTile(playerState.HoveredTile, out var tileState)
                && tileState.OccupantId == FightStateConstants.TILE_NOT_OCCUPIED
                && playerState.CurrentPathData.AvailableForPathing(tileState.Coordinates.X, tileState.Coordinates.Y))
            {
                var path = new Path();
                Debug.Assert(playerState.CurrentPathData.TryFillPath(path, unitState.Coordinates, tileState.Coordinates));
                
                context.Actions.Add(new MoveUnitAction.Data(
                    playerState.SelectedUnit,
                    path));
            }

            if (playerState.SelectedUnit == FightStateConstants.NO_UNIT)
            {
                ActionPerformed(EActionType.UnitDeselected);
            }
        }
    }
}