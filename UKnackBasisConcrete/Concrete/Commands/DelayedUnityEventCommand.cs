using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UKnack.Commands;

namespace UKnack.Concrete.Commands
{
    [AddComponentMenu("UKnack/Commands/RunDelayedEventCommand")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class DelayedUnityEventCommand: CommandMonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        private float _delay = 0.1f;

        [SerializeField]
        private UnityEvent _delayedEvent;

        [SerializeField]
        private bool _executeOnEnable = false;

        private void OnEnable()
        {
            if (_delayedEvent == null)
                throw new System.ArgumentNullException(nameof(_delayedEvent));
            if (_executeOnEnable)
                Execute();
        }

        public override void Execute()
        {
            StartCoroutine(DoDelayedWork());
        }

        private IEnumerator DoDelayedWork()
        {
            if (_delay <=0)
                yield return null;
            else
                yield return new WaitForSeconds(_delay);

            _delayedEvent?.Invoke();
        }


    }
}
