using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.StateMachine
{
    public class StateMachine<TStates, TActions, TContext>
        where TStates: Enum
        where TActions : Enum
    {
        private readonly IEqualityComparer<TStates> _statesEqualityComparer;
        private readonly IEqualityComparer<TActions> _actionsEqualityComparer;

        private readonly Dictionary<TStates, StateBehaviour<TStates, TActions, TContext>> _behaviourStates;
        private readonly Dictionary<StateTransition<TStates, TActions>, TStates> _transitions 
            = new Dictionary<StateTransition<TStates, TActions>, TStates>();
        private TStates _currentState;

        public StateMachine(IEqualityComparer<TStates> statesEqualityComparer,
            IEqualityComparer<TActions> actionsEqualityComparer)
        {
            _statesEqualityComparer = statesEqualityComparer;
            _actionsEqualityComparer = actionsEqualityComparer;
            _behaviourStates = new Dictionary<TStates, StateBehaviour<TStates, TActions, TContext>>(_statesEqualityComparer);
        }

        public void NextState(TActions action)
        {
            var transition = new StateTransition<TStates, TActions>(_currentState, action,
                _statesEqualityComparer, _actionsEqualityComparer);

            if (!_transitions.TryGetValue(transition, out var nextState))
            {
                Debug.LogErrorFormat($"There's no transition from {_currentState} by {action} action.");
                return;
            }
            
            SetState(nextState);
        }

        public void Start(TStates startState)
        {
            ValidateBehaviourStates();
            _currentState = startState;
        }

        public void Tick(TContext context)
        {
            _behaviourStates[_currentState].Tick(context);
        }

        private void SetState(TStates state)
        {
            _currentState = state;
        }

        public void AddTransition(TStates from, TActions by, TStates to)
        {
            var transition = new StateTransition<TStates, TActions>(from, by, _statesEqualityComparer, _actionsEqualityComparer);
            _transitions.Add(transition, to);
        }

        public TransitionSettingProviderFrom<TStates, TActions, TContext> BindTransitions()
        {
            return new TransitionSettingProviderFrom<TStates, TActions, TContext>(this);
        }

        private void ValidateBehaviourStates()
        {
            //TODO: find a better solution
            if (_behaviourStates.Count < Enum.GetNames(typeof(TStates)).Length) //ugly but dunno how to check enum count
            {
                Debug.LogError($"There's a state(s) without binded behaviour");
            }
        }

        public BehaviourBindingChain<TStates, TActions, TContext> BindBehaviourToState(TStates state, StateBehaviour<TStates, TActions, TContext> behaviour)
        {
            _behaviourStates.Add(state, behaviour);
            behaviour.Init(this);
            return new BehaviourBindingChain<TStates, TActions, TContext>(this);
        }
    }

    public class BehaviourBindingChain<TStates, TActions, TContext>
        where TStates: Enum
        where TActions : Enum
    {
        private readonly StateMachine<TStates, TActions, TContext> _stateMachine;

        public BehaviourBindingChain(StateMachine<TStates, TActions, TContext> stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public BehaviourBindingChain<TStates, TActions, TContext> BindBehaviourToState(TStates state, StateBehaviour<TStates, TActions, TContext> behaviour)
        {
            return _stateMachine.BindBehaviourToState(state, behaviour);
        }
    }

    public class TransitionSettingProviderFrom<TStates, TActions, TContext>
        where TStates: Enum
        where TActions : Enum
    {
        private readonly StateMachine<TStates, TActions, TContext> _stateMachine;

        public TransitionSettingProviderFrom(StateMachine<TStates, TActions, TContext> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public TransitionSettingProviderBy<TStates, TActions, TContext> From(TStates state)
        {
            return new TransitionSettingProviderBy<TStates, TActions, TContext>(_stateMachine, state);
        }
    }
    
    public class TransitionSettingProviderBy<TStates, TActions, TContext>
        where TStates: Enum
        where TActions : Enum
    {
        private readonly StateMachine<TStates, TActions, TContext> _stateMachine;
        private readonly TStates _from;

        public TransitionSettingProviderBy(StateMachine<TStates, TActions, TContext> stateMachine, TStates from)
        {
            _stateMachine = stateMachine;
            _from = @from;
        }

        public TransitionSettingProviderTo<TStates, TActions, TContext> By(TActions action)
        {
            return new TransitionSettingProviderTo<TStates, TActions, TContext>(_stateMachine, _from, action);
        }
    }
    
    public class TransitionSettingProviderTo<TStates, TActions, TContext>
        where TStates: Enum
        where TActions : Enum
    {
        private readonly StateMachine<TStates, TActions, TContext> _stateMachine;
        private readonly TStates _from;
        private readonly TActions _by;

        public TransitionSettingProviderTo(StateMachine<TStates, TActions, TContext> stateMachine, TStates from, TActions by)
        {
            _stateMachine = stateMachine;
            _from = @from;
            _by = @by;
        }

        public TransitionSettingProviderFrom<TStates, TActions, TContext> To(TStates toState)
        {
            _stateMachine.AddTransition(_from, _by, toState);
            return new TransitionSettingProviderFrom<TStates, TActions, TContext>(_stateMachine);
        }
    }
}