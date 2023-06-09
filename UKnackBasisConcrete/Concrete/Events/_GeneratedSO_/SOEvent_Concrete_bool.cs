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
    [CreateAssetMenu(fileName = "SOEvent_bool", menuName = "UKnack/SOEvents/SOEvent_bool")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEvent_Concrete_bool : SOEvent<bool>
    {
        [SerializeField] private UnityEvent<bool> _beforeSubscribers;
        [SerializeField] private UnityEvent<bool> _afterSubscribers;

        internal override void InternalInvoke(bool b)
        {
            _beforeSubscribers.Invoke(b);
            base.InternalInvoke(b);
            _afterSubscribers.Invoke(b);
        }
    }
}


