using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] List<TKey> Keys = new List<TKey>();
    [SerializeField] List<TValue> Values = new List<TValue>();


    public void OnBeforeSerialize()
    {
        Keys.Clear();
        Values.Clear();

        foreach (KeyValuePair<TKey,TValue> pair in this)
        {
            Keys.Add(pair.Key);
            Values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        if(Keys.Count  != Values.Count)
        {
            Debug.Log("Keys count is not equal to values count");
        }

        for (int i = 0; i < Keys.Count; i++)
        {
            this.Add(Keys[i], Values[i]);
        }
    }
}
