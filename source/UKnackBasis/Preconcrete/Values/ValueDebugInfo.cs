using System;
using UnityEngine;

namespace UKnack.Preconcrete.Values;

/// <summary>
/// This struct purpose is to view values on Editor with low overhead
/// </summary>
[Serializable]
public struct ValueDebugInfo<T>
{
    [SerializeField]
    [Tooltip("This value is for debug only purposes, to watch last value that was set")]
    private T _value;
    [SerializeField]
    [Tooltip("System time when value was set")]
    private long _timeStamp;

    public T SetAndReturn(T value)
    {
#if UNITY_EDITOR
        Set(value);
#endif
        return value;
    }

    public void Set(T value)
    {
#if UNITY_EDITOR
        _value = value;
        _timeStamp = System.DateTime.Now.Ticks;
#endif
    }

    public static implicit operator ValueDebugInfo<T>(T value)
    {
        ValueDebugInfo<T> result = default;
        result.Set(value);
        return result;
    }
}
