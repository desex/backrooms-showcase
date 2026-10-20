using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.SerializeCollection
{
    public abstract class UnitySerializedDictionaryBase<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField] private List<TKey> _keyData = new();

        [HideInInspector]
        [SerializeField] private List<TValue> _valueData = new();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // Более показать не могу
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Более показать не могу
        }
    }
}