using System;
using System.Collections.Generic;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.Configs
{
    [Serializable]
    public class UnitConfig
    {
        public int UnitTypeId;
        public Dictionary<AttributeType, int> Attributes;
    }
}