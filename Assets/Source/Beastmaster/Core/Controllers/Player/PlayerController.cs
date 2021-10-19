using System.Collections.Generic;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Common.PathFinding;
using Common.StateMachine;

namespace Beastmaster.Core.Controllers
{
    public class PlayerController
    {
        private readonly StateMachine<EStateType, EActionType, (PlayerState, List<ActionData>)> _stateMachine; //How does tuple affect performance?

        private readonly PathFinderController _pathFinderController;
        
        public PlayerController(IFightInputProvider fightInputProvider)
        {
            _pathFinderController = new PathFinderController();
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
            
            _pathFinderController.Tick(state);
        }
    }
}