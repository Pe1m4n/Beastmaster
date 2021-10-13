using Beastmaster.Core.Configs;

namespace Beastmaster.Core.State.Fight
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