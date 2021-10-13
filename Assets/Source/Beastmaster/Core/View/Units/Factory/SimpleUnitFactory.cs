using System;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Common.UnityExtensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Beastmaster.Core.View.Units
{
    public class SimpleUnitFactory : IUnitViewFactory
    {
        private readonly IUnitPrefabProvider _unitPrefabProvider;

        public SimpleUnitFactory(IUnitPrefabProvider unitPrefabProvider)
        {
            _unitPrefabProvider = unitPrefabProvider;
        }
        
        public UnitView CreateUnitView(UnitState state, Transform parent)
        {
            var go = Object.Instantiate(_unitPrefabProvider.GetPrefabForUnitTypeId(state.UnitTypeId), parent);
            if (!go.TryGetComponentInChildren<UnitView>(out var unitView))
            {
                throw new InvalidOperationException(
                    $"Prefab for unit {state.UnitTypeId} doesn't have UnitView component attached to it's children");
            }
            unitView.Init(state);

            return unitView;
        }
    }
}