using System.Collections.Generic;

namespace Beastmaster.Core.State
{
    public interface IStateActionMediator
    {
        void PerformAction(ActionData data);

        void PerformActions(IEnumerable<ActionData> actions);
    }
}