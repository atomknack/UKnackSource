//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from SOValueToUnityEventAdapter_FromGeneric
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Preconcrete.Commands;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Values
{
    /// <summary>
    /// Subscribes UnityEvent to SOValue. 
    /// And in OnEnable invokes UnityEvent with SOValue (Unlike SOEvent version of such script, that only waits for event).
    /// </summary>
    [AddComponentMenu("UKnack/SOValueToUnityEventAdapters/SOValue_float_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOValueToUnityEventAdapter_Concrete_float : AbstractCommandSubscribedToSOEvent<float>
    {
        [SerializeField]
        private UnityEvent<float> _unityEvent;

        [SerializeField]
        [Tooltip("Subscribes UnityEvent to SOValue, OnEnable invokes UnityEvent with value of SOValue")]
        [ValidReference(typeof(IValue<float>), nameof(IValue<float>.Validate))] 
        private SOValue<float> _value;

        private new void OnEnable()
        {
            base.OnEnable();
            _unityEvent?.Invoke(_value.GetValue());
        }

        protected override IEvent<float> SubscribedTo => 
            IEvent<float>.Validate(_value);

        public override void Execute(float t) => 
            _unityEvent?.Invoke(t);
    }
}

