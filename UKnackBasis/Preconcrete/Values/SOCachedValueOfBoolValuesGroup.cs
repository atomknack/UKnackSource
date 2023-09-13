using System;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Preconcrete.Values;

public abstract class SOCachedValueOfBoolValuesGroup : SOValue<bool>
{
    [SerializeField]
    [ValidReference]
    [DisableEditingInPlaymode]
    protected SOValue<bool>[] _dependUpon;

    [NonSerialized]
    protected bool _subscribedTo_dependUpon = false;

    [NonSerialized]
    private bool _isInitialized = false;
    [NonSerialized]
    private bool _cached;

    protected abstract void RefreshCache(); // The only method that need to be changed for other condition types

    public bool RawValue
    {
        get
        {
            if (_isInitialized)
                return _cached;
            RefreshCache();
            _isInitialized = true;
            return _cached;
        }
        protected set
        { 
            _cached = value;
        }
    }

    public override bool GetValue() => RawValue;


    protected virtual void DependUponValueChanged(bool _)
    {
        bool prev = RawValue;
        RefreshCache();
        bool current = RawValue;
        if (prev != current) 
            InvokeSubscribers(this, GetValue());
    }

    protected virtual void TrySubscribeToProvider()
    {
        if (_subscribedTo_dependUpon == true)
            return;
        foreach (var provider in _dependUpon)
        {
            if (provider == null)
                throw new System.Exception("all providers should be not null");
            provider.Subscribe(DependUponValueChanged);
        }


        _subscribedTo_dependUpon = true;
    }


    protected virtual void TryUnSubscribeFromProvider()
    {
        if (_subscribedTo_dependUpon == false)
            return;
        if (SubscribersCount() == 0)
        {
            foreach (var provider in _dependUpon)
            {
                provider.UnsubscribeNullSafe(DependUponValueChanged);
            }

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