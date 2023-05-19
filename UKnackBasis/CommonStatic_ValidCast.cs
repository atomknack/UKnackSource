using UnityEngine;

namespace UKnack;

public static partial class CommonStatic
{
    public static TCast ValidCast<TCast>(UnityEngine.Object obj)
    {
        if (obj == null)
            throw new System.ArgumentNullException("Object should not be null");
        if (obj is TCast cast)
            return cast;
        throw new System.Exception($"Cannot cast {nameof(UnityEngine.Object)} to {typeof(TCast)}");
    }

}
