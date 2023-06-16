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
    public partial class SOPrefsAudioVolume
    {
        // [SerializeField][PlaymodeDisabled] private SOValue<float> _dependUpon;
        // [NonSerialized]  bool _subscribedTo_dependUpon = false;
        // private partial float GetValue(float parent, float _value);
        
        protected virtual void DependUponValueChanged(float newDependUponValue)
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

        public override void Subscribe(Action<float> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }
        public override void Subscribe(UnityEvent<float> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }
        public override void Subscribe(ISubscriberToEvent<float> subscriber)
        {
            TrySubscribeToProvider();
            base.Subscribe(subscriber);
        }

        internal override void Unsubscribe(Action<float> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }
        internal override void Unsubscribe(UnityEvent<float> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }
        internal override void Unsubscribe(ISubscriberToEvent<float> subscriber)
        {
            base.Unsubscribe(subscriber);
            TryUnSubscribeFromProvider();
        }

    }
}
