//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from AbstractCommandSubscribedToSOEvent
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using System;
using UKnack.Attributes;
using UKnack.Commands;
using UKnack.Events;
using UnityEngine;

namespace UKnack.Preconcrete.Commands;

public abstract class AbstractCommandSubscribedToSOEvent : CommandMonoBehaviour, ISubscriberToEvent
{
    //[SerializeField]
    //[ValidReference(typeof(IEvent))] //commented because this is not allowed
    //private SOEvent _subscribedTo;

    public abstract IEvent SubscribedTo { get; }

    public virtual void OnEventNotification() => 
        Execute();

    protected virtual void OnEnable()
    {
        if (SubscribedTo == null)
            throw new ArgumentNullException(nameof(SubscribedTo));
        SubscribedTo.Subscribe(this);
    }
    protected virtual void OnDisable()
    {
         SubscribedTo.UnsubscribeNullSafe(this);
    }
}

