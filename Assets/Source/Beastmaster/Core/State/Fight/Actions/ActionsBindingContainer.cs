using System;
using System.Collections.Generic;

namespace Beastmaster.Core.State
{
    public class ActionsBindingContainer
    {
        private readonly Dictionary<Type, Action<FightState, ActionData>> _actionsDict;

        public ActionsBindingContainer()
        {
            _actionsDict = new Dictionary<Type, Action<FightState, ActionData>>();
            BindActions();
        }
        
        private void BindActions()
        {
            AddBinding(new SpawnUnitAction());
            AddBinding(new MoveUnitAction());
        }

        public Action<FightState, ActionData> GetActionForData(ActionData data)
        {
            if (!_actionsDict.TryGetValue(data.GetType(), out var action))
            {
                throw new InvalidOperationException($"There's no state action for data of type {data.GetType()}");
            }

            return action;
        }

        private void AddBinding<TData>(AbstractStateAction<FightState, TData> actionInstance) where TData : ActionData
        {
            _actionsDict.Add(typeof(TData), actionInstance.Execute);
        }
    }
}