using UnityEngine;

namespace UKnack.Common;

public class ScriptableObjectWithReadOnlyName : ScriptableObject
{
    // to protect scriptable object from errorous asset name change from accidental UnityEvent pick
    public new string name
    {
        //at current version get only sufficiently hides name from edit, if in future versions of UnityEditor it not enough, you can try to uncomment setter.
        //set { Debug.LogError($"Are you trying to change asset name to '{value}'? If it was intentional, use cast to base class"); }
        get { return base.name; }
    }
}