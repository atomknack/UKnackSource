//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from SOEventToUnityEventAdapter_FromGeneric
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Commands;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{
    /// <summary>
    /// Subscribes UnityEvent to SOEvent. 
    /// </summary>
    [AddComponentMenu("UKnack/SOEventToUnityEventAdapters/SOEvent_float_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventToUnityEventAdapter_Concrete_float : AbstractCommandSubscribedToSOEvent<float>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<float>), nameof(IEvent<float>.Validate))] 
        private SOEvent<float> _subscribedTo;

        [SerializeField]
        private UnityEvent<float> _unityEvent;

        protected override IEvent<float> SubscribedTo => 
            IEvent<float>.Validate(_subscribedTo);

        public override void Execute(float t) => 
            _unityEvent?.Invoke(t);

    }
}

