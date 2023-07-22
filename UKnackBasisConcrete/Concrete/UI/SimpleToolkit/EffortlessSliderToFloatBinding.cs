
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack.Values;
using UKnack.Preconcrete.UI.SimpleToolkit;
using UKnack.Events;
using System;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [Obsolete("pending deletion in Major 3 version, use EffortlessSliderToRawSOValueFloatBinding instead")]
    public class EffortlessSliderToFloatBinding : EffortlessUIElement_Slider, ISubscriberToEvent<float>
    {
        private string _description = string.Empty;
        public string Description => _description;

        [SerializeField]
        [ValidReference(typeof(SOValueMutable<float>), typeof(Values.SOPrefsAudioVolume))]
        private SOValueMutable<float> _valueProvider;

        [SerializeField] 
        private UnityEvent<float> _onSliderUIChanged;

        private void OnValueChanged(ChangeEvent<float> ev)
        {
            if (ev.newValue == _valueProvider.RawValue)
                return;
            if (ev.previousValue == ev.newValue)
                return;
            _valueProvider.SetValue(ev.newValue);
            _onSliderUIChanged.Invoke(ev.newValue);
        }

        public void OnEventNotification(float _)
        {
            float value = _valueProvider.RawValue;
            if (value == _slider.value)
                return;
            _slider.SetValueWithoutNotify(value);
        }

        protected override void LayoutReady(VisualElement layout)
        {
            _description = $"{nameof(EffortlessSliderToFloatBinding)} of {gameObject.name}";

            if (_valueProvider == null)
                throw new System.ArgumentNullException(nameof(_valueProvider));

            if (_onSliderUIChanged == null)
                throw new System.ArgumentNullException(nameof(_onSliderUIChanged));

            OnEventNotification(_valueProvider.RawValue);
            _slider.RegisterCallback<ChangeEvent<float>>(OnValueChanged);
            _valueProvider.Subscribe(this);
        }

        protected override void LayoutGonnaBeDestroyedNow()
        {
            _slider.UnregisterCallback<ChangeEvent<float>>(OnValueChanged);
            _valueProvider.Unsubscribe(this);
        }
    }

}