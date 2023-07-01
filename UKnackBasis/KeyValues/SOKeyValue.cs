using UKnack.Events;
using UKnack.Values;
using UnityEngine.InputSystem;

namespace UKnack.KeyValues;

public abstract class SOKeyValue<TKey, TValue> : SOEvent<TKey, TValue>, IKeyValue<TKey, TValue>
{
    public TValue this[TKey key]
    {
        get => GetValue(key);
    }

    public abstract bool ContainsKey(TKey key);

    public abstract TValue GetValue(TKey key);
}