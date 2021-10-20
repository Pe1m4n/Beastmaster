using System;
using System.Collections.Generic;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight.Meta;

namespace Beastmaster.Core.State.Fight
{
    public class FightState
    {
        public readonly FightConfig FightConfig;
        public readonly TileState[] Tiles;
        public readonly List<UnitState> Units;
        public readonly List<ActionData> Actions;
        public FightMetaState Meta;
        
        public int UnitsSpawned { get; internal set; }
        
        public FightState(FightConfig config)
        {
            FightConfig = config;
            Tiles = new TileState[config.LocationHeight * config.LocationWidth];
            Units = new List<UnitState>();
            Actions = new List<ActionData>();

            Meta.TurnEnd = DateTime.Now.AddSeconds(config.TurnTimeSeconds);
            
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
            AddPlayerUnits(FightConfig.LeftPlayerData, FightConfig.LeftPlayerSpawnPoints);
            AddPlayerUnits(FightConfig.RightPlayerData, FightConfig.RightPlayerSpawnPoints);
        }

        private void AddPlayerUnits(in PlayerData playerData, Coordinates[] spawnPoints)
        {
            for (var i = 0; i < playerData.Units.Length; i++)
            {
                var unitConfig = playerData.Units[i];
                var spawnPoint = spawnPoints[i];

                this.AddUnit(playerData.PlayerId, unitConfig, spawnPoint);
            }   
        }
    }
}