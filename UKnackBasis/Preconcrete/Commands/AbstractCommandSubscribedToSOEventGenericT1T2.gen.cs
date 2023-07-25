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

public abstract class AbstractCommandSubscribedToSOEvent<T1,T2> : CommandMonoBehaviour<T1,T2>, ISubscriberToEvent<T1,T2>
{
    //[SerializeField]
    //[ValidReference(typeof(IEvent<T1,T2>))] //commented because this is not allowed
    //private SOEvent<T1,T2> _subscribedTo;

    public abstract IEvent<T1,T2> SubscribedTo { get; }

    public virtual void OnEventNotification(T1 t1,T2 t2) => 
        Execute(t1,t2);

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

