using UnityEngine;
using System.Collections;

namespace AnimatorLikeAnimation.Style6
{
    public class Style6ControllerPlus : MonoBehaviour
    {
        int currentAnimation;
        [SerializeField] Style6Animation target;
        [SerializeField] AnimationClip[] animations;

        void Awake()
        {
            target.Setup(animations);
        }

        public void PlayNextAnimation()
        {
            currentAnimation++;
            if (currentAnimation >= animations.Length)
            {
                currentAnimation = 0;
            }

            target.CrossFadeInFixedTime(animations[currentAnimation].name);
        }
    }
}