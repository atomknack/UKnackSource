using UKnack.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Input
{
    [AddComponentMenu("UKnack/CallbackContext/PhaseDecomposerToUnityEvents")]
    internal class CallbackContextPhaseDecomposerToUnityEvents: MonoBehaviour, IPublisher<CallbackContext>
    {
        [SerializeField] private UnityEvent<CallbackContext> _0_disabled;
        [SerializeField] private UnityEvent<CallbackContext> _1_waiting;
        [SerializeField] private UnityEvent<CallbackContext> _2_started;
        [SerializeField] private UnityEvent<CallbackContext> _3_performed;
        [SerializeField] private UnityEvent<CallbackContext> _4_canceled;

        //You can use ? operator with UnityEvents, but not with Monobehaviours 
        public void Publish(CallbackContext t)
        {
            switch (t.phase)
            {
                case InputActionPhase.Disabled:
                    _0_disabled?.Invoke(t); 
                    return;
                case InputActionPhase.Waiting:
                    _1_waiting?.Invoke(t);
                    return;
                case InputActionPhase.Started:
                    _2_started?.Invoke(t);
                    return;
                case InputActionPhase.Performed:
                    _3_performed?.Invoke(t);
                    return;
                case InputActionPhase.Canceled:
                    _4_canceled?.Invoke(t);
                    return;
            }
            throw new System.ArgumentException($"Unknown input Action phase: {t.phase}");
        }
    }
}
