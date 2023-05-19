using System;
using System.Diagnostics;
using UnityEngine;

namespace UKnack.Attributes;

[Conditional("UNITY_EDITOR")]
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class ProvidedComponentAttribute : PropertyAttribute
{
    public static T Provide<T>(GameObject owner, T currentProperty) where T : Component
    {
        var result = currentProperty == null ? owner.GetComponentInParent<T>(includeInactive: true) : currentProperty;
        ThrowIfNull(result, typeof(T));
        return result;
    }

    public static Component Provide(Type componentType, GameObject owner, Component currentProperty)
    {
        var result = currentProperty == null ? owner.GetComponentInParent(componentType, includeInactive: true) : currentProperty;
        ThrowIfNull(result, componentType);
        return result;
    }

    private static void ThrowIfNull(Component result, Type componentType)
    {
        if (result == null)
            throw new ArgumentNullException($"Property value is null, and there no suitable {componentType} to provide");
    }
}