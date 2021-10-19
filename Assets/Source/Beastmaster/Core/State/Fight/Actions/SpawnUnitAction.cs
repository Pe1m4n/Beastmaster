using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public sealed class SpawnUnitAction : AbstractStateAction<FightState, SpawnUnitAction.Data>
    {
        public class Data : ActionData
        {
            public readonly int PlayerId;
            public readonly UnitConfig Config;
            public readonly Coordinates Coordinates;

            public Data(int playerId, UnitConfig config, Coordinates coordinates, bool immutable = false) : base(immutable)
            {
                PlayerId = playerId;
                Config = config;
                Coordinates = coordinates;
            }
        }

        protected override void Execute(FightState state, Data data)
        {
            state.AddUnit(data.PlayerId, data.Config, data.Coordinates);   
        }
    }
}