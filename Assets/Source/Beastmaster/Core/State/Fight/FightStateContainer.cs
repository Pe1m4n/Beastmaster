using Beastmaster.Core.Configs;

namespace Beastmaster.Core.State.Fight
{
    public class FightStateContainer
    {
        public FightState State { get; private set; }

        public FightStateContainer(FightConfig config)
        {
            State = new FightState(config);
        }

        public void SetNewState(FightState state)
        {
            State = state;
        }

        public FightState GetLatestState()
        {
            return State;
        }
    }
}