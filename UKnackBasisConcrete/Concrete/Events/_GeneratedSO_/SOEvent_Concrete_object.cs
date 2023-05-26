//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_SOEventS
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Events;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{

    /// This class not intended to be used in code, but only made for ease of creation scriptable object in Unity Editor
    [CreateAssetMenu(fileName = "SOEvent_object", menuName = "UKnack/SOEvents/SOEvent_object")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEvent_Concrete_object : SOEvent<object>
    {
        [SerializeField] private UnityEvent<object> _beforeSubscribers;
        [SerializeField] private UnityEvent<object> _afterSubscribers;

        internal override void InternalInvoke(object obj)
        {
            _beforeSubscribers.Invoke(obj);
            base.InternalInvoke(obj);
            _afterSubscribers.Invoke(obj);
        }
    }
}

