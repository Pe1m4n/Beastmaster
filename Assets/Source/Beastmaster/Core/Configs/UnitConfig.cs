using System;

namespace Beastmaster.Core.Configs
{
    [Serializable]
    public struct UnitConfig
    {
        public int UnitTypeId;
        
        public UnitConfig(int unitTypeId)
        {
            UnitTypeId = unitTypeId;
        }
    }
}