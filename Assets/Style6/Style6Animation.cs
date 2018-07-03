using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

namespace AnimatorLikeAnimation.Style6
{
    [RequireComponent(typeof(Animator))]
    public class Style6Animation : MonoBehaviour
    {
        Animator _animator;
        AnimatorOverrideController _overrideCon;

        [SerializeField]
        List<AnimatorState> _stats;

        private bool _curent = true;

        public void SetAnimation(AnimationClip animationClips)
        {
            _curent = !_curent;
            var currentIndex = System.Convert.ToInt32(_curent);

            _stats[currentIndex].motion = animationClips;
            var hash = _stats[currentIndex].nameHash;
            Debug.Log(hash);
            _animator.CrossFadeInFixedTime(hash, 0.6f);

        }

        public void Setup(AnimationClip animationClips)
        {
            _animator = GetComponent<Animator>();

            _overrideCon = new AnimatorOverrideController();
            _overrideCon.runtimeAnimatorController = _animator.runtimeAnimatorController;
            _animator.runtimeAnimatorController = _overrideCon;

            _stats = new List<AnimatorState>();

            var animCon = _overrideCon.runtimeAnimatorController as AnimatorController;
            foreach (var layer in animCon.layers)
            {
                var stateMachine = layer.stateMachine;
                foreach (var state in stateMachine.states)
                {
                    _stats.Add(state.state);
                }
            }

            SetAnimation(animationClips);
        }
    }
}