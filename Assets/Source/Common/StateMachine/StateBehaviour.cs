using System;

namespace Common.StateMachine
{
    public abstract class StateBehaviour<TStates, TActions, TContext> 
        where TActions: Enum
        where TStates: Enum
    {
        private StateMachine<TStates, TActions, TContext> _stateMachine;

        public void Init(StateMachine<TStates, TActions, TContext> stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public abstract void Tick(TContext context);
        
        protected void ActionPerformed(TActions action)
        {
            _stateMachine.NextState(action);
        }
    }
}