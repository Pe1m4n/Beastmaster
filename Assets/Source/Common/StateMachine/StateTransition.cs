using System;
using System.Collections.Generic;

namespace Common.StateMachine
{
    public class StateTransition<TStates, TActions>
        where TActions : Enum
        where TStates: Enum
    {
        private readonly IEqualityComparer<TStates> _statesEqualityComparer;
        private readonly IEqualityComparer<TActions> _actionsEqualityComparer;
        private readonly TStates _fromState;
        private readonly TActions _onAction;

        public StateTransition(TStates fromState, TActions onAction,
            IEqualityComparer<TStates> statesEqualityComparer,
            IEqualityComparer<TActions> actionsEqualityComparer)
        {
            _statesEqualityComparer = statesEqualityComparer;
            _actionsEqualityComparer = actionsEqualityComparer;
            _fromState = fromState;
            _onAction = onAction;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * _statesEqualityComparer.GetHashCode(_fromState) 
                      + 31 * _actionsEqualityComparer.GetHashCode(_onAction);
        }

        public override bool Equals(object obj)
        {
            return obj is StateTransition<TStates, TActions> other
                   && _statesEqualityComparer.Equals(_fromState, other._fromState)
                   && _actionsEqualityComparer.Equals(_onAction, other._onAction);
        }
    }
}