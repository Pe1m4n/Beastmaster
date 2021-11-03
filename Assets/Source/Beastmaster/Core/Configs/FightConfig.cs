using System;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.Configs
{
    [Serializable]
    public struct FightConfig
    {
        public short LocationWidth;
        public short LocationHeight;

        public short TurnTimeSeconds;

        public Coordinates[] LeftPlayerSpawnPoints;
        public Coordinates[] RightPlayerSpawnPoints;

        public PlayerData LeftPlayerData;
        public PlayerData RightPlayerData;
        
        public FightConfig(short locationWidth,
            short locationHeight,
            short turnTimeSeconds,
            Coordinates[] leftPlayerSpawnPoints,
            Coordinates[] rightPlayerSpawnPoints,
            PlayerData leftPlayerData,
            PlayerData rightPlayerData)
        {
            LocationWidth = locationWidth;
            LocationHeight = locationHeight;
            TurnTimeSeconds = turnTimeSeconds;
            LeftPlayerSpawnPoints = leftPlayerSpawnPoints;
            RightPlayerSpawnPoints = rightPlayerSpawnPoints;
            LeftPlayerData = leftPlayerData;
            RightPlayerData = rightPlayerData;
            
            
            System.Diagnostics.Debug.Assert(LeftPlayerData.Units.Length == LeftPlayerSpawnPoints.Length);
            System.Diagnostics.Debug.Assert(RightPlayerData.Units.Length == RightPlayerSpawnPoints.Length);
        }
    }
}