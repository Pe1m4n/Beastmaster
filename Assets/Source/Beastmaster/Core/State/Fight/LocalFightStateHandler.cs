using Zenject;

namespace Beastmaster.Core.State.Fight
{
    public class LocalFightStateHandler : ITickable
    {
        private readonly FightStateContainer _stateContainer;

        public LocalFightStateHandler(FightStateContainer stateContainer)
        {
            _stateContainer = stateContainer;
        }

        public void Tick()
        {
            
        }
    }
}