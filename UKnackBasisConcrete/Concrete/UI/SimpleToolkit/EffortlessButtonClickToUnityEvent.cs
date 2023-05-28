using UnityEngine;
using UnityEngine.Events;
using UKnack.Preconcrete.UI.SimpleToolkit;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [AddComponentMenu("UKnack/UI.SimpleToolkit/EffortlessButtonClickToUnityEvent")]
    public sealed class EffortlessButtonClickToUnityEvent : EffortlessButtonClick
    {
        [SerializeField]
        private UnityEvent _buttonClickEvent;

        protected override void ButtonClicked()
        {
            _buttonClickEvent?.Invoke();
        }
    }
}