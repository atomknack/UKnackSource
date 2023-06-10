//temp until there is will be same in UKnackBasis

using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Input
{
    [AddComponentMenu("UKnack/CallbackContext/ValueFloatTo_IPublisher<bool>")]
    internal sealed class CallbackContextValueFloatToPublisherBool : MonoBehaviour, IPublisher<CallbackContext>
    {
        [SerializeField]
        [Tooltip("This value is set to true, so there won't be duplicate call to event")]
        private bool _ignoreStarted = true;

        [SerializeField]
        private bool _inversed = false;

        [SerializeField]
        [Tooltip("Set this value if you do not need value of cancelation")]
        private bool _ignoreCanceled = false;

        [SerializeField]
        [ValidReference(typeof(IPublisher<bool>), nameof(IPublisher<bool>.Validate), typeof(IPublisher<bool>))]
        [DisableEditingInPlaymode]
        private UnityEngine.Object _iPublisher;

        private IPublisher<bool> _iPublisherAsInterface;

        public void Publish(CallbackContext ctx)
        {
            if (_ignoreStarted && ctx.started)//ctx.phase == UnityEngine.InputSystem.InputActionPhase.Started)
                return;
            if (_ignoreCanceled && ctx.canceled)
                return;
            float rawvalue = ctx.ReadValue<float>();
            //Debug.Log($"{rawvalue} {ctx.phase} {ctx.started}");
            bool value = (1 - rawvalue) < 0.999f;
            if (_inversed)
                value = !value;
            _iPublisherAsInterface.Publish(value);
        }

        private void Awake()
        {
            _iPublisherAsInterface = IPublisher<bool>.Validate(_iPublisher);
        }
    }
}