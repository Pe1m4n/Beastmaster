using System.Collections.Generic;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;

namespace Beastmaster.Core.View
{
    public class ViewActionsQueue
    {
        private readonly Queue<ActionData> _actionsData = new Queue<ActionData>();
        private readonly ViewActionsBindingContainer _actionsContainer;
        
        private bool _busy;
        
        public ViewActionsQueue(ViewActionsBindingContainer actionsContainer)
        {
            _actionsContainer = actionsContainer;
        }

        public async void Add(ViewState state, ActionData data)
        {
            if (_busy)
            {
                _actionsData.Enqueue(data);
                return;
            }

            _busy = true;
            _actionsData.Enqueue(data);

            while (_actionsData.Count > 0)
            {
                var currentActionData = _actionsData.Dequeue();
                var action = _actionsContainer.GetActionForData(currentActionData);
                if (action == null)
                {
                    continue;
                }
                
                await action.Invoke(state, currentActionData);   
            }
            _busy = false;
        }
    }
}