using Beastmaster.Core.State.Fight;
using Zenject;

namespace Beastmaster.Core.Unity.State
{
    public class LocalStateHandlerTicker : ITickable
    {
        private readonly LocalFightStateHandler _stateHandler;

        public LocalStateHandlerTicker(LocalFightStateHandler stateHandler)
        {
            _stateHandler = stateHandler;
        }
        
        public void Tick()
        {
            _stateHandler.Tick();
        }
    }
}