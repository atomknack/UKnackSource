using UnityEngine;
using UnityEngine.Events;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Commands;
using System;

namespace UKnack.Concrete.Events
{
    [Obsolete("Not tested")]
    [AddComponentMenu("UKnack/SOEventBoolSplitter")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventBoolSplitter : AbstractCommandSubscribedToSOEvent<bool>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<bool>), nameof(IEvent<bool>.Validate))]
        private SOEvent<bool> _subscribedTo;

        [SerializeField]
        private UnityEvent<bool> _eventOnTrue;
        [SerializeField]
        private UnityEvent<bool> _eventOnFalse;

        protected override IEvent<bool> SubscribedTo =>
            IEvent<bool>.Validate(_subscribedTo);

        public override void Execute(bool b)
        {
            if (b)
            {
                _eventOnTrue?.Invoke(this);
                return;
            }

            _eventOnFalse?.Invoke(this);
        }
    }
}