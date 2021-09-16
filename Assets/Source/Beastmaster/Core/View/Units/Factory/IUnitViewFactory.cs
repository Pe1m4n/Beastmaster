using Beastmaster.Core.State;
using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public interface IUnitViewFactory
    {
        UnitView CreateUnitView(UnitState state, Transform parent);
    }
}