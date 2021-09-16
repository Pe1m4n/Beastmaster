using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Beastmaster.Core.View.Units
{
    public class UnitsView : IDisposable
    {
        private readonly List<UnitView> _unitViews = new List<UnitView>();
        private readonly IUnitViewFactory _unitViewFactory;
        private readonly TilesView _tilesView;

        private Transform _unitsRoot;

        public UnitsView(Transform rootTransform,
            IUnitViewFactory unitViewFactory,
            TilesView tilesView)
        {
            _unitViewFactory = unitViewFactory;
            _tilesView = tilesView;
            var unitsRoot = new GameObject("UnitsView");
            unitsRoot.transform.parent = rootTransform;
            _unitsRoot = unitsRoot.transform;
        }

        public void ApplyState(ViewState state)
        {
            SyncUnitsToState(state);
            foreach (var unitView in _unitViews)
            {
                unitView.ApplyState(state);
            }
        }

        public void SyncUnitsToState(ViewState state)
        {
            for (int i = _unitViews.Count; i < state.FightState.Units.Count; i++)
            {
                var unitState = state.FightState.Units[i];
                var unitView = _unitViewFactory.CreateUnitView(unitState, _unitsRoot);
                unitView.transform.position = _tilesView.GetViewPosition(state.FightState, unitState.Coordinates);
                _unitViews.Add(unitView);
                
                state.UnitViewStates.Add(new UnitViewState());
            }
        }

        public UnitView GetUnitView(int id)
        {
            return _unitViews[id];
        }

        public void Dispose()
        {
            foreach (var unitView in _unitViews)
            {
                Object.Destroy(unitView);
            }
        }
    }
}