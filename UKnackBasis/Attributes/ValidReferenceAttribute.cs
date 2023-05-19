using System;
using System.Diagnostics;
using UnityEngine;

namespace UKnack.Attributes;

[Conditional("UNITY_EDITOR")]
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class ValidReferenceAttribute : PropertyAttribute //ValidReferenceAttribute
{
    public readonly Type containerOfValidationMethod;
    public readonly string prefabValidationMethodName;

    public readonly Type subType1;
    public readonly Type subType2;
    public readonly Type[] moreSubtypes;

    public ValidReferenceAttribute(Type subclass1 = null, Type subclass2 = null, params Type[] moreSubclasses) :
        this(typeof(ValidReferenceAttribute), nameof(NotNull), subclass1, subclass2, moreSubclasses)
    {
    }

    public ValidReferenceAttribute(string prefabValidationMethodName,
    Type subclass1 = null, Type subclass2 = null, params Type[] moreSubclasses) :
        this(null, prefabValidationMethodName, subclass1, subclass2, moreSubclasses)
    { 
    }

    public ValidReferenceAttribute(Type containerOfValidationMethod, string prefabValidationMethodName,
        Type subclass1 = null, Type subclass2 = null, params Type[] moreSubclasses)
    {
        this.containerOfValidationMethod = containerOfValidationMethod;
        this.prefabValidationMethodName = prefabValidationMethodName;

        this.subType1 = subclass1;
        this.subType2 = subclass2;
        if (moreSubclasses == null || moreSubclasses.Length == 0)
            this.moreSubtypes = null;
        else
            this.moreSubtypes = moreSubclasses;
    }
    private static void NotNull(UnityEngine.Object obj)
    {
        if (obj == null)
            throw new ArgumentNullException("Object should not be null");
    }

    
    public static T WilyCastFromUnityObject<T>(UnityEngine.Object obj) where T : class
    {
        if (obj == null)
            return null;
        if (obj is T t)
            return t;
        if (obj is GameObject go)
            return go.GetComponent<T>();
        return null;
    }
    /*
    public static bool TryWilyCastFromUnityObject<T>(UnityEngine.Object obj, out T @out)
    {
        if (obj == null)
        {
            @out = default(T);
            return false;
        }
        if (obj is T t)
        {
            @out = t;
            return true;
        }
        if (obj is GameObject go)
        {
            @out = go.GetComponent<T>();
            if (@out != null)
                return true;
            else
                return false;
        }

        @out = default(T);
        return false;
    }

    public static UnityEngine.Object WilyExtractTypeFromUnityObject(UnityEngine.Object obj, Type subclass)
    {
        if (obj == null)
            return obj;
        if (obj is GameObject go)
        {
            Type goType = go.GetType();
            if (goType == subclass || subclass.IsSubclassOf(goType))
                return obj;
            var component =  go.GetComponent(subclass);
            if (component != null)
            {
                //UnityEngine.Debug.Log(component.GetType().ToString());
                return component;
            }

        }
        return obj;
    }*/
}
