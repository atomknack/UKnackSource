using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{

    [CreateAssetMenu(fileName = "RepublishOnlyPerformed_CallbackContext", menuName = "UKnack/RepublishOnlyPerformed_CallbackContext")]
    public sealed class SOPublisher_RepublishOnlyPerformed_CallbackContext : SOPublisher<CallbackContext>
    {
        [SerializeField][ValidReference] private SOPublisher _publisher;

        public override void Publish(CallbackContext t)
        {
            IPublisher<CallbackContext>.Validate(_publisher);
            if (t.performed)
                _publisher.Publish();
        }
    }

}