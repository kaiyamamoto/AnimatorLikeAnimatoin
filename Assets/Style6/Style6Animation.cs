using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

namespace AnimatorLikeAnimation.Style6
{
    [RequireComponent(typeof(Animator))]
    public class Style6Animation : MonoBehaviour
    {
        // アニメーターの設定データ
        [SerializeField]
        private AnimatorScriptableStates _scriptableStates = null;

        // アニメーションの設定内容
        private Dictionary<string, int> _clipNameToHash = new Dictionary<string, int>();

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
        private Dictionary<string, int> CreateAnimatorState(AnimatorOverrideController con, AnimatorScriptableStates states)
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
            var list = states.animState.List;
            for (int i = 0; i < list.Count; i++)
            {
                var clip = list[i].Value.clip;
                var name = list[i].Key;
                if (clipNameToHash.ContainsKey(name) == false)
                {
                    var state = stateMachine.AddState(name);
                    state.motion = clip;
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
        public void Setup()
        {
            // TODO : いずれ別の設定方法
            var runtimeController = _scriptableStates._controller;
            // 上書き用のコントローラー作成
            var overrideAnimatorController = new AnimatorOverrideController(runtimeController);

            // ステートの作成
            _clipNameToHash.Clear();
            _clipNameToHash = CreateAnimatorState(overrideAnimatorController, _scriptableStates);

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