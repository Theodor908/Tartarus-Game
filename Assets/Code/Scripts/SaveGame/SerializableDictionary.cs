using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [System.Serializable]
    public class SerializableDictionary<Tkey,Tvalue> : Dictionary<Tkey,Tvalue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Tkey> keys = new List<Tkey>();
        [SerializeField] private List<Tvalue> values = new List<Tvalue>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach(KeyValuePair<Tkey,Tvalue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }

        }

        public void OnAfterDeserialize()
        {
            Clear();

            if(keys.Count != values.Count)
            {
                throw new System.Exception(string.Format("There are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));
            }

            for(int i = 0; i < keys.Count; i++)
            {
                Add(keys[i], values[i]);
            }

        }
        

    }
}