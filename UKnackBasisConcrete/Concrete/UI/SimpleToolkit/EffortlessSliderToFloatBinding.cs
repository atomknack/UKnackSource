
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
    public class EffortlessSliderToFloatBinding : EffortlessUIElement_Slider
    {
        private string _description = string.Empty;
        public string Description => _description;

        [SerializeField]
        [ValidReference(typeof(SOValueMutable<float>), typeof(Values.SOPrefsAudioVolume))]
        private SOValueMutable<float> _valueProvider;

        [SerializeField] 
        private UnityEvent<float> _onSliderUIChanged;

        [Obsolete("pending deletion in Major 3 version, use EffortlessSliderToRawSOValueFloatBinding instead")]
        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            throw new NotImplementedException();
        }

        [Obsolete("pending deletion in Major 3 version, use EffortlessSliderToRawSOValueFloatBinding instead")]
        protected override void LayoutCleanupBeforeDestruction()
        {
            throw new NotImplementedException();
        }
    }

}