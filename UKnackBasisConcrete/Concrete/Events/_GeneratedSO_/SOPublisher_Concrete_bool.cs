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
[CreateAssetMenu(fileName = "PublisherToSOEvent_bool", menuName = "UKnack/Publishers/To bool")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
internal class SOPublisher_Concrete_bool : SOPublisher<bool>
{
    [SerializeField]
    [ValidReference(typeof(IEvent<bool>), nameof(IEvent<bool>.Validate),
        typeof(SOEvent<bool>),
        typeof(SOEvent_Concrete_bool)
        , typeof(SOValueMutable_Concrete_bool)
    )] private SOEvent<bool> where;

    public override void Publish(bool b)
    {
        ValidateWhere();
        where.InternalInvoke(b);
    }

    internal void ValidateWhere() =>
        IEvent<bool>.Validate(where);

}

}

