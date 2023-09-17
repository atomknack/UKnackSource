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
[CreateAssetMenu(fileName = "PublisherToSOEvent_nongeneric", menuName = "UKnack/Publishers/To nongeneric")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class SOPublisher_Concrete_nongeneric : SOPublisher
{
    [SerializeField]
    [ValidReference(typeof(IEvent), nameof(IEvent.Validate),
        typeof(SOEvent),
        typeof(SOEvent_Concrete_nongeneric)
    )] private SOEvent where;

    public override void Publish()
    {
        ValidateWhere();
        where.InternalInvoke();
    }

    internal void ValidateWhere() =>
        IEvent.Validate(where);

}

}

