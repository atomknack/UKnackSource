/* WIP

using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Events;
using UKnack.Preconcrete.Commands;

namespace UKnack.Concrete.Events
{
    /// <summary>
    /// Subscribes UnityEvent to SOEvent. 
    /// </summary>
    [AddComponentMenu("UKnack/SOEventToUnityEventAdapters/SOEvent_bool_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventBoolSplitter : AbstractCommandSubscribedToSOEvent<bool>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<bool>), nameof(IEvent<bool>.Validate))] 
        private SOEvent<bool> _subscribedTo;

        protected override IEvent<bool> _iEvent => 
            IEvent<bool>.Validate(_subscribedTo);
    }
}
*/