using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.KeyValues;
using UnityEngine;

namespace UKnack.Preconcrete.KeyValues
{
    public class SOKeyValueMutableDictionaryWithDefaultValue<TKey, TValue> : SOKeyValueMutable<TKey, TValue>
    {
        [SerializeField]
        [Tooltip("Value that will be default (returned if no key in dictionary) on every game start(Serialization)")]
        [DisableEditingInPlaymode]
        private TValue _defaultValue;

        private Dictionary<TKey, TValue> _dictionary = new(256);

        public override bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public override TValue GetValue(TKey key)
        {
            if (_dictionary.TryGetValue(key, out TValue value)) 
                return value;
            return _defaultValue;
        }

        public virtual void Clear() => _dictionary.Clear();
        public override bool Remove(TKey key) => _dictionary.Remove(key);

        public override void SetValueWithoutNotify(TKey key, TValue value) => _dictionary[key] = value;
    }
}
