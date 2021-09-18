using System.Collections.Generic;
using Beastmaster.Core.State;
using Common.PathFinding;
using Common.StateMachine;
using UnityEngine;

namespace Beastmaster.Core.Controllers
{
    public class DefaultBehaviour : StateBehaviour<EStateType, EActionType, (PlayerState PlayerState, List<ActionData> Actions)>
    {
        private readonly IFightInputProvider _fightInputProvider;

        public DefaultBehaviour(IFightInputProvider fightInputProvider)
        {
            _fightInputProvider = fightInputProvider;
        }

        public override void Tick((PlayerState PlayerState, List<ActionData> Actions) context)
        {
            var state = context.PlayerState;
            state.HoveredTile = _fightInputProvider.GetTileUnderCursor();
            state.HoveredUnit = _fightInputProvider.GetUnitIdUnderCursor();

            if (_fightInputProvider.LMBClicked())
            {
                state.SelectedTile = state.HoveredTile;
                state.SelectedUnit = state.HoveredUnit;
            }

            if (state.SelectedUnit != FightStateConstants.NO_UNIT)
            {
                ActionPerformed(EActionType.UnitSelected);
            }
        }
    }
}