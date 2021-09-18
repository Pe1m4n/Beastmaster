using System.Collections.Generic;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State
{
    public class FightState
    {
        public FightConfig FightConfig { get; }
        public TileState[] Tiles { get; }
        public List<UnitState> Units { get; }
        public List<ActionData> Actions { get; }
        public int UnitsSpawned { get; internal set; }
        
        public FightState(FightConfig config)
        {
            FightConfig = config;
            Tiles = new TileState[config.LocationHeight * config.LocationWidth];
            Units = new List<UnitState>();
            Actions = new List<ActionData>();
            
            CreateDefaultTileStates();
            CreateDefaultUnitStates();
        }

        private void CreateDefaultTileStates()
        {
            for (var index = 0; index < Tiles.Length; index++)
            {
                Tiles[index] = new TileState(new Coordinates()
                {
                    X = index % FightConfig.LocationWidth,
                    Y = (index - index % FightConfig.LocationWidth) / FightConfig.LocationWidth
                });
            }
        }

        private void CreateDefaultUnitStates()
        {
            for (var i = 0; i < FightConfig.LeftPlayerUnits.Length; i++)
            {
                var unitConfig = FightConfig.LeftPlayerUnits[i];
                var spawnPoint = FightConfig.LeftPlayerSpawnPoints[i];

                this.AddUnit(FightStateConstants.LEFT_PLAYER_ID, unitConfig.UnitTypeId, spawnPoint);
            }
            
            for (var i = 0; i < FightConfig.RightPlayerUnits.Length; i++)
            {
                var unitConfig = FightConfig.RightPlayerUnits[i];
                var spawnPoint = FightConfig.RightPlayerSpawnPoints[i];

                this.AddUnit(FightStateConstants.RIGHT_PLAYER_ID, unitConfig.UnitTypeId, spawnPoint);
            }
        }
    }
}