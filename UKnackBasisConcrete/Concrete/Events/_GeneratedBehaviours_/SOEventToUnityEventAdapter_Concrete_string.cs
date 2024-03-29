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
    [AddComponentMenu("UKnack/SOEventToUnityEventAdapters/SOEvent_string_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventToUnityEventAdapter_Concrete_string : AbstractCommandSubscribedToSOEvent<string>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<string>), nameof(IEvent<string>.Validate))] 
        private SOEvent<string> _subscribedTo;

        [SerializeField]
        private UnityEvent<string> _unityEvent;

        protected override IEvent<string> SubscribedTo => 
            IEvent<string>.Validate(_subscribedTo);

        public override void Execute(string s) => 
            _unityEvent?.Invoke(s);

    }
}

