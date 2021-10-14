using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.View.Units;
using Common.UniRxExtensions;
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
            var viewState = state.UnitViewStates[data.UnitId].AnimationState;
            viewState.CurrentAnimation = AnimationNames.WALK;

            using (var disposable = new CompositeDisposable())
            {
                for (int i = 1; i < data.Path.Length; i++)
                {
                    var desiredDirection = data.Path[i - 1].GetLookAtDirection(data.Path[i]);
                    var desiredRotation = ViewConstants.Directions[desiredDirection];
                    if (targetView.transform.rotation != desiredRotation)
                        await RotateUnit(targetView, desiredRotation, disposable);
                   
                    await MoveUnitToPosition(targetView, _tilesView.GetViewPosition(state.FightState, data.Path[i]), disposable);
                }    
            }
            
            
            viewState.CurrentAnimation = AnimationNames.IDLE;
        }

        private Task MoveUnitToPosition(UnitView target, Vector3 destination, ICollection<IDisposable> disposables)
        {
            var origin = target.transform.position;
            const float timeToTravel = 1f;
            var tcs = new TaskCompletionSource<bool>();
            UniRxExtensions.TimerTween(timeToTravel).Subscribe(p =>
            {
                var newPos = Vector3.Lerp(origin, destination, p);
                target.transform.position = newPos;
            }, () => tcs.SetResult(true)).AddTo(disposables);
            
            return tcs.Task;
        }
        
        public static Task RotateUnit(UnitView target, Quaternion rotation, ICollection<IDisposable> disposables)
        {
            var origin = target.transform.rotation;
            const float timeToTurn = 0.5f;
            var tcs = new TaskCompletionSource<bool>();
            UniRxExtensions.TimerTween(timeToTurn).Subscribe(p =>
            {
                var newRotation = Quaternion.Lerp(origin, rotation, p);
                target.transform.rotation = newRotation;
            }, () =>
            {
                target.transform.rotation = rotation;
                tcs.SetResult(true);
            }).AddTo(disposables);
            
            return tcs.Task;
        }
    }
}