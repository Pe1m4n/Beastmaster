using System.Linq;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Beastmaster.Core.Installers
{
    public class TestFightConfigFactory : SerializedScriptableObject
    {
        [OdinSerialize] private short _locationWidth;
        [OdinSerialize] private short _locationHeight;
        [OdinSerialize] private short _turnTimeSeconds;

        [OdinSerialize] private Coordinates[] _leftPlayerSpawnPoints;
        [OdinSerialize] private Coordinates[] _rightPlayerSpawnPoints;

        [OdinSerialize] private UnitConfigHolder[] _leftPlayerUnits;
        [OdinSerialize] private UnitConfigHolder[] _rightPlayerUnits;

        [OdinSerialize] private string _leftPlayerNickname;
        [OdinSerialize] private string _rightPlayerNickname;
        
        public FightConfig Create()
        {
            return new FightConfig(_locationWidth,
                _locationHeight,
                _turnTimeSeconds,
                _leftPlayerSpawnPoints,
                _rightPlayerSpawnPoints,
                new PlayerData(0, _leftPlayerNickname, _leftPlayerUnits.Select(h => h.UnitConfig).ToArray()),
                new PlayerData(1, _rightPlayerNickname, _rightPlayerUnits.Select(h => h.UnitConfig).ToArray()));

        }
    }
}