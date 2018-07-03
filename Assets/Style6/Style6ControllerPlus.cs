using UnityEngine;
using System.Collections;

namespace AnimatorLikeAnimation.Style6
{
    public class Style6ControllerPlus : MonoBehaviour
    {
        int currentAnimation = 0;
        [SerializeField] Style6Animation target;
        [SerializeField] AnimationClip[] animations;

        void Start()
        {
            target.Setup(animations[currentAnimation]);
        }

        public void PlayNextAnimation()
        {
            currentAnimation++;
            if (currentAnimation >= animations.Length)
            {
                currentAnimation = 0;
            }

            target.SetAnimation(animations[currentAnimation]);
        }
    }
}