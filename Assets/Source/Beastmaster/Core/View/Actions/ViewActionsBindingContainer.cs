using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beastmaster.Core.State;

namespace Beastmaster.Core.View
{
    public class ViewActionsBindingContainer
    {
        private readonly Dictionary<Type, Func<ViewState, ActionData, Task>> _actionsDict;

        public ViewActionsBindingContainer(MoveUnitViewAction moveUnitViewAction)
        {
            _actionsDict = new Dictionary<Type, Func<ViewState, ActionData, Task>>();
            
            AddBinding(moveUnitViewAction);
        }

        public Func<ViewState, ActionData, Task> GetActionForData(ActionData data)
        {
            if (!_actionsDict.TryGetValue(data.GetType(), out var action))
            {
                throw new InvalidOperationException($"There's no view action for data of type {data.GetType()}");
            }

            return action;
        }

        private void AddBinding<TData>(AbstractViewAction<ViewState, TData> actionInstance) where TData : ActionData
        {
            _actionsDict.Add(typeof(TData), actionInstance.Execute);
        }
    }
}