using System;
using System.Collections.Generic;
using UKnack.Events;
using UKnack.Values;
using UnityEngine;

namespace UKnack.Values;

public abstract class SOValueMutable<T> : SOValue<T>, IValueMutable<T>, IPublisher<T>
{
    //[SerializeField] private USetOrDefault<T> _value;
    public abstract T RawValue { get; protected set; }

    public override T GetValue() => RawValue;

    /// <summary>
    /// !!!Danger, Do NOT set value in subscriber because it could lead to infinite loop!!!
    /// Value won't be set if it same as previous value;
    /// if value will be set, then it will be set only after all subscribers notification of new value.
    /// </summary>
    /// <param name="value"></param>
    [Obsolete("new version not tested")]
    public virtual void SetValue(T value)
    {
        SetValueWithoutNotify(value);
        InternalInvoke(GetValue());//new version, replaces: InternalInvoke(value);
    }

    internal override void InternalInvoke(T value)
    {
        SetValueWithoutNotify(value);
        SOEvent<T>.InvokeSubscribers(this, value);
    }

    public virtual void SetValueWithoutNotify(T value)
    {
        T prevValue = GetValue();
        if (EqualityComparer<T>.Default.Equals(value, prevValue))
            return;
        RawValue = value;
    }

    public virtual void Publish(T t)
    {
        SetValue(t);
    }
}