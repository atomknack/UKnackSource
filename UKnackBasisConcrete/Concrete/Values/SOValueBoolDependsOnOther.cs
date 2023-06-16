using System;
using UKnack.Attributes;
using UKnack.Preconcrete.Values;
using UKnack.Values;
using UnityEngine;

namespace UKnack.Concrete.Values
{
    [Obsolete("new version (04.24) not tested")]
    [CreateAssetMenu(fileName = "SOValueBoolDependsOnOther", menuName = "UKnack/SOValueDependsOnOther/bool DependsOnOther", order = 110)]
    public partial class SOValueBoolDependsOnOther : SOValueMutable<bool>
    {
        [SerializeField]
        [MarkNullAsColor(0.2f, 0.5f, 0.5f, "if null, then this value is independent")]
        [DisableEditingInPlaymode]
        private SOValue<bool> _dependUpon;

        [SerializeField]
        private USetOrDefault<bool> _rawValue;

        [NonSerialized]
        private bool _subscribedTo_dependUpon = false;

        [SerializeField]
        private ValueDebugInfo<bool> _DebugLastResultedValue;



        public override bool RawValue 
        { 
            get => _rawValue.Value; 
            protected set => _rawValue.Value = value; 
        }

        public void Flip() => ExtensionsForSOValues.Flip(this);

        private bool GetValue(bool parent, bool value) => 
            _DebugLastResultedValue.SetAndReturn(parent && value);
        public override bool GetValue()
        {
            return _DebugLastResultedValue.SetAndReturn(CalcGetValue());

            bool CalcGetValue()
            {
                bool value = RawValue;
                if (value == false)
                    return false;
                if (_dependUpon == null)
                    return value;

                return GetValue(_dependUpon.GetValue(), value);
            }
        }
    }

}

