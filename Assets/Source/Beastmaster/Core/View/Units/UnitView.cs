using System;
using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Animator _animator;
        
        private AnimationComponent _animationComponent;
        
        public int UnitId { get; private set; }

        private void Awake()
        {
            _animationComponent = new AnimationComponent(_animator);
        }

        public void Init(int unitId)
        {
            UnitId = unitId;
        }
        
        public void ApplyState(ViewState state)
        {
            if (state.PlayerState.HoveredUnit == UnitId)
            {
                _renderer.material.color = Color.yellow;
            }
            else if (state.PlayerState.SelectedUnit == UnitId)
            {
                _renderer.material.color = Color.green;
            }
            else
            {
                _renderer.material.color = Color.white;
            }
            
            _animationComponent.ApplyState(state.UnitViewStates[UnitId].AnimationState);
        }
    }
}