using System.Collections.Generic;
using Beastmaster.Core.State;
using Common.StateMachine;

namespace Beastmaster.Core.Controllers
{
    public class PlayerController
    {
        private readonly StateMachine<EStateType, EActionType, (PlayerState, List<ActionData>)> _stateMachine; //How does tuple affect performance?

        public PlayerController(IFightInputProvider fightInputProvider)
        {
            _stateMachine = new StateMachine<EStateType, EActionType, (PlayerState, List<ActionData>)>(new StateTypeEqualityComparer(),
                new ActionsTypeEqualityComparer());

            _stateMachine.BindTransitions().From(EStateType.Default).By(EActionType.UnitSelected)
                .To(EStateType.UnitSelected);
            _stateMachine.BindTransitions().From(EStateType.UnitSelected).By(EActionType.UnitDeselected)
                .To(EStateType.Default);

            _stateMachine.BindBehaviourToState(EStateType.Default, new DefaultBehaviour(fightInputProvider))
                .BindBehaviourToState(EStateType.UnitSelected, new UnitSelectedBehaviour(fightInputProvider));
            
            _stateMachine.Start(EStateType.Default);
        }
        
        public void Tick(PlayerState state, List<ActionData> actions)
        {
            _stateMachine.Tick((state, actions));
        }
    }
}