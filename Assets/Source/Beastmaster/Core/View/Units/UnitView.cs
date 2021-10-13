using System;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private AnimationComponent _animationComponent;
        private Direction _currentDirection;
        
        public int UnitId { get; private set; }

        private void Awake()
        {
            _animationComponent = new AnimationComponent(_animator);
        }

        public void Init(UnitState state)
        {
            UnitId = state.Id;
        }
        
        public void ApplyState(ViewState state)
        {
            var unitViewState = state.UnitViewStates[UnitId];
            _animationComponent.ApplyState(unitViewState.AnimationState);
        }
    }
}