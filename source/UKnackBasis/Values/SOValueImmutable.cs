using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKnack.Values;

public abstract class SOValueImmutable<T> : SOValue<T>
{
    internal override void InternalInvoke(T value)
    {
        throw new System.InvalidOperationException("Immutable value cannot be changed");
    }
}