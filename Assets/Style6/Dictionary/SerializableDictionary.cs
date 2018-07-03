using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// シリアライズ可能なDictionaryのクラス
/// </summary>
[System.Serializable]
public class SerializeDictionaryBase<TKey, TValue, Type> where Type : SerializePairBase<TKey, TValue>
{
    [SerializeField]
    private List<Type> _list;
    private Dictionary<TKey, TValue> _table;

    public List<Type> List
    {
        get { return _list; }
    }

    public Dictionary<TKey, TValue> Table
    {
        get { return _table == null ? ConvertListToDictionary(List) : _table; }
        set { _table = value; }
    }
    static Dictionary<TKey, TValue> ConvertListToDictionary(List<Type> list)
    {
        var dic = new Dictionary<TKey, TValue>();
        foreach (var pair in list)
        {
            dic.Add(pair.Key, pair.Value);
        }
        return dic;
    }
}

/// <summary>
/// シリアル化できる、KeyValuePair
/// </summary>
[System.Serializable]
public class SerializePairBase<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public SerializePairBase(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public SerializePairBase(KeyValuePair<TKey, TValue> pair)
    {
        Key = pair.Key;
        Value = pair.Value;
    }
}