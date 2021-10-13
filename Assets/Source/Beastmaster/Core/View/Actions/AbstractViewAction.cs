using System.Threading.Tasks;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;

namespace Beastmaster.Core.View
{
    public abstract class AbstractViewAction<TState, TData> where TData : ActionData
    {
        public Task Execute(TState state, ActionData data)
        {
            return Execute(state, data as TData);
        }
        
        protected abstract Task Execute(TState state, TData data);
    }
}