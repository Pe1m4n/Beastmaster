using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public class AnimationComponent
    {
        private readonly Animator _animator;

        private string _currentAnimation;

        public AnimationComponent(Animator animator)
        {
            _animator = animator;
        }

        public void ApplyState(AnimationState state)
        {
            if (_currentAnimation != state.CurrentAnimation)
            {
                _animator.SetTrigger(state.CurrentAnimation);
                _currentAnimation = state.CurrentAnimation;
            }
        }
    }
}