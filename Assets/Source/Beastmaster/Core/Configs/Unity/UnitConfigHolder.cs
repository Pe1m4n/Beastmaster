using System;
using Beastmaster.Core.Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Beastmaster.Core.Installers
{
    public class UnitConfigHolder : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize] public UnitConfig UnitConfig = new UnitConfig();
    }
}