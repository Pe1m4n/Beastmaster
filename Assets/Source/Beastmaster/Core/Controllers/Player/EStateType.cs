using System.Collections.Generic;

namespace Beastmaster.Core.Controllers
{
    public enum EStateType
    {
        Default,
        UnitSelected
    }

    public class StateTypeEqualityComparer : IEqualityComparer<EStateType>
    {
        public bool Equals(EStateType x, EStateType y)
        {
            return x == y;
        }

        public int GetHashCode(EStateType obj)
        {
            return (int)obj;
        }
    }
}