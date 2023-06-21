using System;
using UKnack.Attributes;
using UKnack.Preconcrete.Values;
using UKnack.Values;
using UnityEngine;

namespace UKnack.Concrete.Values
{
    [CreateAssetMenu(fileName = "PrefsAudio", menuName = "UKnack/SOPrefs/audio float DependsOnOther", order = 110)]
    public partial class SOPrefsAudioVolume : SOValueMutable<float>
    {
        [SerializeField]
        [MarkNullAsColor(0.2f, 0.5f, 0.5f, "if null, then this value is independent")]
        [DisableEditingInPlaymode]
        private SOValue<float> _dependUpon;

        [SerializeField]
        //[Range(0f, 1f)]
        private USetOrPrefsOrDefault<float> _rawValue;

        [NonSerialized]
        private bool _subscribedTo_dependUpon = false;

        [SerializeField]
        private ValueDebugInfo<float> _DebugLastResultedValue;


        public override float RawValue 
        { 
            get => Mathf.Clamp01(_rawValue.Value); 
            protected set => _rawValue.Value = Mathf.Clamp01(value); 
        }

        private float GetValue(float parent, float value)
        {
            //Debug.Log($"get value requested {parent} {value}");
            return _DebugLastResultedValue.SetAndReturn(Mathf.Clamp01(parent) * Mathf.Clamp01(value));
        }

        public override float GetValue()
        {
            return _DebugLastResultedValue.SetAndReturn(CalcGetValue());

            float CalcGetValue()
            {
                float value = RawValue;
                if (_dependUpon == null)
                    return value;

                return GetValue(_dependUpon.GetValue(), value);
            }
        }
    }

}

