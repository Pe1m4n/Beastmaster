using System.Collections.Generic;
using Beastmaster.Core.State;
using Common.StateMachine;

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
            context.PlayerState.HoveredTile = _fightInputProvider.GetTileUnderCursor();
            context.PlayerState.HoveredUnit = _fightInputProvider.GetUnitIdUnderCursor();

            if (_fightInputProvider.LMBClicked())
            {
                context.PlayerState.SelectedTile = context.PlayerState.HoveredTile;
                context.PlayerState.SelectedUnit = context.PlayerState.HoveredUnit;
            }

            if (context.PlayerState.SelectedUnit != FightStateConstants.NO_UNIT)
            {
                ActionPerformed(EActionType.UnitSelected);
            }
        }
    }
}