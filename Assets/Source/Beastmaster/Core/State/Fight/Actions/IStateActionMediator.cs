using System.Collections.Generic;

namespace Beastmaster.Core.State.Fight
{
    public interface IStateActionMediator
    {
        void PerformAction(ActionData data);

        void PerformActions(IEnumerable<ActionData> actions);
    }
}