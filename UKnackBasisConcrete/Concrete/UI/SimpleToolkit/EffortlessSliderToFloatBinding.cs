
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack.Values;
using UKnack.Preconcrete.UI.SimpleToolkit;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    public class EffortlessSliderToFloatBinding : EffortlessUIElement_Slider
    {
        [SerializeField]
        [ValidReference(typeof(SOValueMutable<float>), typeof(Values.SOPrefsAudioVolume))]
        private SOValueMutable<float> valueProvider;

        [SerializeField] 
        private UnityEvent<float> _onSliderUIChanged;

        private void OnValueChanged(ChangeEvent<float> ev)
        {
            if (ev.newValue == valueProvider.RawValue)
                return;
            if (ev.previousValue == ev.newValue)
                return;
            valueProvider.SetValue(ev.newValue);
            _onSliderUIChanged.Invoke(ev.newValue);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (valueProvider == null)
                throw new System.ArgumentNullException(nameof(valueProvider));

            if (_onSliderUIChanged == null)
                throw new System.ArgumentNullException(nameof(_onSliderUIChanged));

            _slider.SetValueWithoutNotify(valueProvider.RawValue);
            _slider.RegisterCallback<ChangeEvent<float>>(OnValueChanged);
        }

        protected override void OnDisable()
        {
            _slider.UnregisterCallback<ChangeEvent<float>>(OnValueChanged);
            base.OnDisable();
        }

    }

}