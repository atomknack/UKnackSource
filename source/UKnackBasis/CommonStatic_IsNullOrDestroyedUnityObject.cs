using System;

namespace UKnack;

public static partial class CommonStatic
{
    //IsNullOrDestroyedUnityObject
    //IsNullOrDestroyed
    /*
    public static bool IsNull(this UnityEngine.Object obj)//System.Object
    {
        if (object.ReferenceEquals(obj, null)) 
            return true;
        //if (obj is UnityEngine.Object)
        //    return (obj as UnityEngine.Object) == null;
        //return false;
        return obj == null;
    }
    public static bool IsNotNull(this UnityEngine.Object obj) => !obj.IsNull();
    */
    /*
    private const string STRINGEMPTY = "";
    public static void DoOrThrow(this UnityEngine.Object obj, Action act, string paramName = STRINGEMPTY, string message = STRINGEMPTY)
    {
        if (obj.IsNull())
            throw new ArgumentNullException(paramName, message);
        act();
    }
    public static void DoOrThrow(this UnityEngine.Object obj, Action<UnityEngine.Object> act, string paramName = STRINGEMPTY, string message = STRINGEMPTY)
    {
        if (obj.IsNull())
            throw new ArgumentNullException(paramName, message);
        act(obj);
    }
    */
}
