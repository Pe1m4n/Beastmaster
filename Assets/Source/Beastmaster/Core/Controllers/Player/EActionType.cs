using System.Collections.Generic;

namespace Beastmaster.Core.Controllers
{
    public enum EActionType
    {
        UnitSelected,
        UnitDeselected
    }

    public class ActionsTypeEqualityComparer : IEqualityComparer<EActionType>
    {
        public bool Equals(EActionType x, EActionType y)
        {
            return x == y;
        }

        public int GetHashCode(EActionType obj)
        {
            return (int)obj;
        }
    }
}