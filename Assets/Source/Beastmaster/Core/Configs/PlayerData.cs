using System;

namespace Beastmaster.Core.Configs
{
    [Serializable]
    public struct PlayerData
    {
        public readonly byte PlayerId;
        public readonly string Nickname;
        public readonly UnitConfig[] Units;

        public PlayerData(byte playerId, string nickname, UnitConfig[] units)
        {
            PlayerId = playerId;
            Nickname = nickname;
            Units = units;
        }
    }
}