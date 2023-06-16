//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_SOValueDependsOn_PartialGenerated
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

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
    public partial class SOInversedBoolValue
    {
        // [SerializeField][PlaymodeDisabled] private SOValue<bool> _dependUpon;
        // [NonSerialized]  bool _subscribedTo_dependUpon = false;
        // private partial bool GetValue(bool parent, bool _value);
        
        protected virtual void DependUponValueChanged(bool newDependUponValue)
        {
            InvokeSubscribers(this, GetValue(newDependUponValue, RawValue));
        }

        protected virtual void TrySubscribeToProvider()
        {
            if (_subscribedTo_dependUpon == true)
                return;
            if (_dependUpon == null)
                return;
            _dependUpon.Subscribe(DependUponValueChanged);
            _subscribedTo_dependUpon = true;
        }


        protected virtual void TryUnSubscribeFromProvider()
        {
            if (_subscribedTo_dependUpon == false)
                return;
            if (_dependUpon == null)
                return;
            if (SubscribersCount() == 0 ) 
            {
                _dependUpon.UnsubscribeNullSafe(DependUponValueChanged);
                _subscribedTo_dependUpon = false;
            }
        }

        public override void Subscribe(Action<bool> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }
        public override void Subscribe(UnityEvent<bool> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }
        public override void Subscribe(ISubscriberToEvent<bool> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }

        internal override void Unsubscribe(Action<bool> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }
        internal override void Unsubscribe(UnityEvent<bool> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }
        internal override void Unsubscribe(ISubscriberToEvent<bool> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }

    }
}
