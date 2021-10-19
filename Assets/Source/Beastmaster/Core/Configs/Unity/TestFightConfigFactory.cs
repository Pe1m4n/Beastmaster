using System.Linq;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Beastmaster.Core.Installers
{
    public class TestFightConfigFactory : SerializedScriptableObject
    {
        [OdinSerialize] private int _locationWidth;
        [OdinSerialize] private int _locationHeight;

        [OdinSerialize] private Coordinates[] _leftPlayerSpawnPoints;
        [OdinSerialize] private Coordinates[] _rightPlayerSpawnPoints;

        [OdinSerialize] private UnitConfigHolder[] _leftPlayerUnits;
        [OdinSerialize] private UnitConfigHolder[] _rightPlayerUnits;
        
        public FightConfig Create()
        {
            return new FightConfig(_locationWidth,
                _locationHeight,
                _leftPlayerSpawnPoints,
                _rightPlayerSpawnPoints,
                _leftPlayerUnits.Select(h => h.UnitConfig).ToArray(),
                _rightPlayerUnits.Select(h => h.UnitConfig).ToArray());
        }
    }
}