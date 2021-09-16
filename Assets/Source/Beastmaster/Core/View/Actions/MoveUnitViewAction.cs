using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beastmaster.Core.State;
using Beastmaster.Core.View.Units;
using UniRx;
using UnityEngine;

namespace Beastmaster.Core.View
{
    public class MoveUnitViewAction : AbstractViewAction<ViewState, MoveUnitAction.Data>
    {
        private readonly UnitsView _unitsView;
        private readonly TilesView _tilesView;

        public MoveUnitViewAction(UnitsView unitsView, TilesView tilesView)
        {
            _unitsView = unitsView;
            _tilesView = tilesView;
        }
        
        protected override async Task Execute(ViewState state, MoveUnitAction.Data data)
        {
            var targetView = _unitsView.GetUnitView(data.UnitId);
            var disposable = new CompositeDisposable(); //TODO: don't forget to rework that creepy bastard
            var viewState = state.UnitViewStates[data.UnitId].AnimationState;
            viewState.CurrentAnimation = AnimationNames.WALK;
            await MoveUnitToPosition(targetView, _tilesView.GetViewPosition(state.FightState, data.MoveTo), disposable);
            disposable.Dispose();
            viewState.CurrentAnimation = AnimationNames.IDLE;
        }

        private Task MoveUnitToPosition(UnitView target, Vector3 destination, ICollection<IDisposable> disposables)
        {
            var origin = target.transform.position;
            const float timeToTravel = 1.5f;
            var currentTravelTime = 0f;
            var tcs = new TaskCompletionSource<bool>();
            Observable.EveryUpdate().Subscribe(u =>
            {
                currentTravelTime += Time.deltaTime;
                var newPos = Vector3.Lerp(origin, destination, currentTravelTime / timeToTravel);
                target.transform.position = newPos;
                if (currentTravelTime >= timeToTravel)
                {
                    tcs.SetResult(true);
                }
            }).AddTo(disposables);
            return tcs.Task;
        }
    }
}