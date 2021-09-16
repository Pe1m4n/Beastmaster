using Beastmaster.Core.Configs;
using Beastmaster.Core.State;

namespace Beastmaster.Core
{
    public class FightStateContainer
    {
        public FightState State { get; }

        public FightStateContainer(FightConfig config)
        {
            State = new FightState(config);
        }
    }
}