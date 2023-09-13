#nullable disable
using System;
using UKnack.Values;

namespace UKnack.Preconcrete.Values;

/// <summary>
/// UMemoizedValue not contains any serializable fields
/// In this library it used in base classes to get values only once from child
/// </summary>
/// <typeparam name="T"></typeparam>
[Obsolete("Not tested, never used yet")]
public struct UMemoizedValue<T> : IValueGetter<T>
{
    private readonly Func<T> _lazyGetter;
    private bool _initialized;
    private T _value;

    public T Value { get => GetValue(); }

    public T GetValue() => 
        _initialized ? _value : InitAndReturn();

    public void Deconstruct(out T value)
    {
        value = GetValue();
    }

    public UMemoizedValue(Func<T> valueGetterForLazyInitialization)
    {
        _lazyGetter = valueGetterForLazyInitialization;
        _initialized = false;
    }

    public void ForceReinit()
    {
        InitAndReturn();
    }

    private T InitAndReturn()
    {
        _value = _lazyGetter(); 
        _initialized = true;
        return _value;
    }
}

