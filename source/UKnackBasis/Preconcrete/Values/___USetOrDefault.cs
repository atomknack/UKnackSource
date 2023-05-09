/* now is generated
using System;
using UKnack.Values;
using UnityEngine;

namespace UKnackBasis.Preconcrete.Values;

/// <summary>
/// This struct intended to be used with scriptable objects
/// </summary>
[Serializable]
public struct USetOrDefault<T> : IValueGetter<T>, IValueSetter<T>//, ISerializationCallbackReceiver
{
    [SerializeField]
    [Tooltip("Value that will be default on every game start(Serialization)")]
    private T _defaultValue;
    [NonSerialized] private bool _isSet;
    [NonSerialized] private T _value;

    [SerializeField]
    [Tooltip("This value is for debug only purposes, to watch last value that was get")]
    private ValueDebugInfo<T> _debugLastValueGet;

    [SerializeField]
    [Tooltip("This value is for debug only purposes, to watch last value that was set")]
    private ValueDebugInfo<T> _debugLastValueSet;

    public T Value { get => GetValue(); set => SetValue(value); }
    public T GetValue() =>
        _debugLastValueGet.SetAndReturn( 
            _isSet ? _value : _defaultValue
            );

    public void SetValue(T value)
    {
        _isSet = true;
        _value = _debugLastValueSet.SetAndReturn( value);

        //SetLastValue();
    }

    
//    public void OnAfterDeserialize() =>
//        SetLastValue();

//    public void OnBeforeSerialize() =>
//        SetLastValue();

//    private void SetLastValue()
//    {
//#if UNITY_EDITOR
//        _lastValue_Readonly = GetValue();
//#endif
//    }
    
}
*/