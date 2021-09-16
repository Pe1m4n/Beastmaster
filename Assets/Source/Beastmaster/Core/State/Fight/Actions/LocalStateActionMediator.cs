using System.Collections.Generic;

namespace Beastmaster.Core.State
{
    public class LocalStateActionMediator : IStateActionMediator
    {
        private readonly FightStateContainer _fightStateContainer;
        private readonly ActionsBindingContainer _actionsBindingContainer;

        public LocalStateActionMediator(FightStateContainer fightStateContainer, ActionsBindingContainer actionsBindingContainer)
        {
            _fightStateContainer = fightStateContainer;
            _actionsBindingContainer = actionsBindingContainer;
        }

        public void PerformAction(ActionData data)
        {
            _actionsBindingContainer.GetActionForData(data).Invoke(_fightStateContainer.State, data);
            _fightStateContainer.State.Actions.Add(data);
        }

        public void PerformActions(IEnumerable<ActionData> actions)
        {
            foreach (var action in actions)
            {
                PerformAction(action);
            }
        }
    }
}