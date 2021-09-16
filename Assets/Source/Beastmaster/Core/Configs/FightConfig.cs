using System;
using Beastmaster.Core.Primitives;
using UnityEngine;

namespace Beastmaster.Core.Configs
{
    [Serializable]
    public struct FightConfig
    {
        public int LocationWidth;
        public int LocationHeight;

        public Coordinates[] LeftPlayerSpawnPoints;
        public Coordinates[] RightPlayerSpawnPoints;

        public UnitConfig[] LeftPlayerUnits;
        public UnitConfig[] RightPlayerUnits;
        
        public FightConfig(int locationWidth,
            int locationHeight,
            Coordinates[] leftPlayerSpawnPoints,
            Coordinates[] rightPlayerSpawnPoints,
            UnitConfig[] leftPlayerUnits,
            UnitConfig[] rightPlayerUnits)
        {
            LocationWidth = locationWidth;
            LocationHeight = locationHeight;
            LeftPlayerSpawnPoints = leftPlayerSpawnPoints;
            RightPlayerSpawnPoints = rightPlayerSpawnPoints;
            LeftPlayerUnits = leftPlayerUnits;
            RightPlayerUnits = rightPlayerUnits;
            
            Debug.Assert(LeftPlayerUnits.Length == LeftPlayerSpawnPoints.Length);
            Debug.Assert(RightPlayerUnits.Length == RightPlayerSpawnPoints.Length);
        }
    }
}