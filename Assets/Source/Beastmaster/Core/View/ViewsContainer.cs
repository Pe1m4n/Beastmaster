using System;
using Beastmaster.Core.View.Units;

namespace Beastmaster.Core.View
{
    public class ViewsContainer : IDisposable
    {
        private readonly TilesView _tilesView;
        private readonly UnitsView _unitsView;
        private readonly ViewActionsQueue _viewActionsQueue;

        private int _lastPerformedAction;

        public ViewsContainer(TilesView tilesView,
            UnitsView unitsView,
            ViewActionsQueue viewActionsQueue)
        {
            _tilesView = tilesView;
            _unitsView = unitsView;
            _viewActionsQueue = viewActionsQueue;
        }
        
        public void ApplyState(ViewState state)
        {
            PerformActions(state);
            _tilesView.ApplyState(state);
            _unitsView.ApplyState(state);
        }

        private void PerformActions(ViewState state)
        {
            for (int i = _lastPerformedAction; i < state.FightState.Actions.Count; i++)
            {
                _viewActionsQueue.Add(state, state.FightState.Actions[i]);
            }

            _lastPerformedAction = state.FightState.Actions.Count;
        }

        public void Dispose()
        {
            _tilesView.Dispose();
            _unitsView.Dispose();
        }
    }
}