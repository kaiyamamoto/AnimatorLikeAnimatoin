using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

namespace AnimatorLikeAnimation.Style6
{
    [RequireComponent(typeof(Animator))]
    public class Style6Animation : MonoBehaviour
    {
        // アニメーションの設定内容
        Dictionary<string, int> _clipNameToHash = new Dictionary<string, int>();

        // アニメーター
        private Animator _animator = null;
        public Animator Animator
        {
            get { return _animator ? _animator : GetComponent<Animator>(); }
        }

        /// <summary>
        /// OverrideControllerにアニメーションクリップを設定 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="animationClips"></param>
        private Dictionary<string, int> CreateAnimatorState(AnimatorOverrideController con, AnimationClip[] animationClips)
        {
            // cast
            var animCon = con.runtimeAnimatorController as AnimatorController;
            // 初期レイヤーからステートマシーンを取得
            var stateMachine = animCon.layers[0].stateMachine;
            // ステートをリセット
            foreach (var state in stateMachine.states)
            {
                stateMachine.RemoveState(state.state);
            }

            // Stateの設定
            Dictionary<string, int> clipNameToHash = new Dictionary<string, int>();
            for (int i = 0; i < animationClips.Length; i++)
            {
                var name = animationClips[i].name;
                if (clipNameToHash.ContainsKey(name) == false)
                {
                    var state = stateMachine.AddState(name);
                    state.motion = animationClips[i];
                    var hash = state.nameHash;

                    clipNameToHash.Add(name, hash);
                }
            }

            // 設定した内容のDictionary返還
            return clipNameToHash;
        }

        /// <summary>
        /// Animatorの設定
        /// </summary>
        /// <param name="animationClips"></param>
        public void Setup(AnimationClip[] animationClips)
        {
            // TODO : いずれ別の設定方法
            var runtimeController = Resources.Load<RuntimeAnimatorController>(string.Format("ChangeController"));
            // 上書き用のコントローラー作成
            var overrideAnimatorController = new AnimatorOverrideController(runtimeController);

            // ステートの作成
            _clipNameToHash.Clear();
            _clipNameToHash = CreateAnimatorState(overrideAnimatorController, animationClips);

            // 現在のコントローラーに設定内容を反映
            Animator.runtimeAnimatorController = overrideAnimatorController;
        }

        /// <summary>
        /// フェードありのアニメーションPlay
        /// </summary>
        /// <param name="name"></param>
        public void CrossFadeInFixedTime(string name)
        {
            var hash = _clipNameToHash[name];
            Animator.CrossFadeInFixedTime(hash, 0.6f);
        }

        /// <summary>
        /// 通常のアニメーションPlay
        /// </summary>
        /// <param name="name"></param>
        public void Play(string name)
        {
            var hash = _clipNameToHash[name];
            Animator.Play(hash);
        }
    }
}