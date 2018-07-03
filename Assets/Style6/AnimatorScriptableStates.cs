using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アニメーターのステートを設定するScriptableObject
public class AnimatorScriptableStates : ScriptableObject
{
    public RuntimeAnimatorController _controller = null;

    public AnimStateTable animState;

    // Key は別所で定義する
    [System.Serializable]
    public class AnimStateTable : SerializeDictionaryBase<string, AnimData, AnimPair> { }

    [System.Serializable]
    public class AnimPair : SerializePairBase<string, AnimData> { public AnimPair(string key, AnimData value) : base(key, value) { } }
}


// 保持データ
[System.Serializable]
public class AnimData
{
    public AnimationClip clip = null;

    public float fadeTime = 0.5f;

    public AnimData(AnimationClip clip = null, float time = 0.5f)
    {
        this.clip = clip;
        this.fadeTime = time;
    }
}
