using System.Collections.Generic;
using UKnack.Events;
using UKnack.Values;
using UnityEngine.InputSystem;

namespace UKnack.KeyValues;

public abstract class SOKeyValueMutable<TKey, TValue> : 
    SOKeyValue<TKey, TValue>, 
    IKeyValueMutable<TKey, TValue>, 
    IPublisher<TKey, TValue>, 
    IKeyValueSetterWithoutNotification<TKey, TValue>
{
    public abstract bool Remove(TKey key);

    new public TValue this[TKey key] 
    { 
        get => GetValue(key); 
        set => SetValue(key, value);
    }

    public virtual void SetValue(TKey key, TValue value)
    {
        SetValueWithoutNotify(key, value);
        InternalInvoke(key, GetValue(key));//new version, replaces: InternalInvoke(value);
    }

    internal override void InternalInvoke(TKey key, TValue value)
    {
        SetValueWithoutNotify(key,value);
        SOEvent<TKey, TValue>.InvokeSubscribers(this, key, value);
    }

    public abstract void SetValueWithoutNotify(TKey key, TValue value);
    // {
    //     T prevValue = GetValue(key);
    //     if (EqualityComparer<TValue>.Default.Equals(value, prevValue))
    //         return;
    //     RawValue = value;
    // }

    public virtual void Publish(TKey key, TValue value)
    {
        SetValue(key, value);
    }
}
