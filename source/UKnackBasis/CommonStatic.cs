using UnityEngine;

namespace UKnack;

public static partial class CommonStatic
{
    public static string GetFullPath_Recursive(GameObject go) =>
        go.transform.parent == null ? go.name : GetTransformPath_Recursive(go.transform.parent) + "/" + go.name;
    public static string GetTransformPath_Recursive(Transform trans) =>
        trans.parent == null ? trans.name : GetTransformPath_Recursive(trans.parent) + "/" + trans.name;

    public static string BelongingGameobjectName(MonoBehaviour behaviour)
    {
        if(behaviour == null)
            return "null, reason: behaviour is null";
        if (behaviour.gameObject == null)
            return "null, reason: gameObject is null";
        return behaviour.gameObject.name;
    }


}
