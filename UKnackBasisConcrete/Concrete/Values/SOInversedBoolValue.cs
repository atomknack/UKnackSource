using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UKnack.Concrete.Values
{
    [Obsolete("new version (04.24) not tested")]
    [CreateAssetMenu(fileName = "SOInversedBoolValue", menuName = "UKnack/SOValueDependsOnOther/Inversed bool value", order = 110)]
    public partial class SOInversedBoolValue : SOValue<bool>
    {
        [SerializeField]
        [ValidReference]
        private SOValue<bool> _dependUpon;

        [NonSerialized]
        private bool _subscribedTo_dependUpon = false;

        public bool RawValue { get => !_dependUpon.GetValue(); }
        private static bool GetValue(bool parent, bool _) => !parent;

        public override bool GetValue() => RawValue;


    }
}