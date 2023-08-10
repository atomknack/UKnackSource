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
        T result = ProvideOrNull(owner, currentProperty);
        ThrowIfNull(result, typeof(T));
        return result;
    }

    public static T ProvideOrNull<T>(GameObject owner, T currentProperty) where T : Component =>
         currentProperty == null ? owner.GetComponentInParent<T>(includeInactive: true) : currentProperty;

    public static Component Provide(Type componentType, GameObject owner, Component currentProperty)
    {
        Component result = ProvideOrNull(componentType, owner, currentProperty);
        ThrowIfNull(result, componentType);
        return result;
    }

    public static Component ProvideOrNull(Type componentType, GameObject owner, Component currentProperty) =>
        currentProperty == null ? owner.GetComponentInParent(componentType, includeInactive: true) : currentProperty;

    private static void ThrowIfNull(Component result, Type componentType)
    {
        if (result == null)
            throw new ArgumentNullException($"Property value is null, and there no suitable {componentType} to provide");
    }
}