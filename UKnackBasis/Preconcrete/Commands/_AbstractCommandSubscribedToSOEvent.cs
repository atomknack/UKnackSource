//now is generated
/*
using System;
using UKnack.Attributes;
using UKnack.Commands;
using UKnack.Events;
using UnityEngine;

namespace UKnack.Preconcrete.Commands;

public abstract class AbstractCommandSubscribedToSOEvent : CommandMonoBehaviour, ISubscriberToEvent
{
    [SerializeField]
    [ValidReference(typeof(IEvent))]
    private SOEvent _subscribedTo;

    public virtual void OnEventNotification() => 
        Execute();
    protected virtual void OnEnable()
    {
        Subscribe(_subscribedTo, this);
    }
    protected virtual void OnDisable()
    {
        UnSubscribe(_subscribedTo, this);
    }
    public static void Subscribe(SOEvent soevent, ISubscriberToEvent subsriber)
    {
        if (soevent == null)
            throw new ArgumentNullException(nameof(soevent));
        soevent.Subscribe(subsriber);
    }

    public static void UnSubscribe(SOEvent soevent, ISubscriberToEvent subsriber)
    {
        soevent.UnsubscribeNullSafe(subsriber);
    }
}
*/