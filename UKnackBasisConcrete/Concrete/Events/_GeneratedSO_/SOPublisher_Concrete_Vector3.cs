//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_SOEventS
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Concrete.Values;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{

/// This class not intended to be used in code, but only made for ease of creation scriptable object in Unity Editor
[CreateAssetMenu(fileName = "PublisherToSOEvent_Vector3", menuName = "UKnack/Publishers/To Vector3")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class SOPublisher_Concrete_Vector3 : SOPublisher<Vector3>
{
    [SerializeField]
    [ValidReference(typeof(IEvent<Vector3>), nameof(IEvent<Vector3>.Validate),
        typeof(SOEvent<Vector3>),
        typeof(SOEvent_Concrete_Vector3)
        , typeof(SOValueMutable_Concrete_Vector3)
    )] private SOEvent<Vector3> where;

    public override void Publish(Vector3 v)
    {
        ValidateWhere();
        where.InternalInvoke(v);
    }

    internal void ValidateWhere() =>
        IEvent<Vector3>.Validate(where);

}

}

