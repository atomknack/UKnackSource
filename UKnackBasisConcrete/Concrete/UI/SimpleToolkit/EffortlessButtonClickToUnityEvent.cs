using UnityEngine;
using UnityEngine.Events;
using UKnack.Preconcrete.UI.SimpleToolkit;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [AddComponentMenu("UKnack/UI.SimpleToolkit/EffortlessButtonClickToUnityEvent")]
    public sealed class EffortlessButtonClickToUnityEvent : EffortlessUIElement_Button
    {
        [SerializeField]
        private UnityEvent _buttonClickEvent;

        private void ButtonClicked()
        {
            _buttonClickEvent?.Invoke();
        }

        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            _button.clicked += ButtonClicked;
        }
        protected override void LayoutCleanupBeforeDestruction()
        {
            _button.clicked -= ButtonClicked;
        }
    }
}