using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public interface IUnitViewFactory
    {
        UnitView CreateUnitView(UnitState state, Transform parent);
    }
}