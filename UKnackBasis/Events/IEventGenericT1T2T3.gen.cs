//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EventInterfaces
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using System;
using UnityEngine.Events;

namespace UKnack.Events;

public static partial class IEventUnSubscribeNullSafeExtension
{
    public static void UnsubscribeNullSafe<T1,T2,T3>(this IEvent<T1,T2,T3> ev, Action<T1,T2,T3> subscriber)
        { if (subscriber != null) ev.Unsubscribe(subscriber); }
    public static void UnsubscribeNullSafe<T1,T2,T3>(this IEvent<T1,T2,T3> ev, UnityEvent<T1,T2,T3> subscriber)
        { if (subscriber != null) ev.Unsubscribe(subscriber); }
    public static void UnsubscribeNullSafe<T1,T2,T3>(this IEvent<T1,T2,T3> ev, ISubscriberToEvent<T1,T2,T3> subscriber)
        { if (subscriber != null) ev.Unsubscribe(subscriber); }
}
public interface IEvent<T1,T2,T3>
{
    public static IEvent<T1,T2,T3> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IEvent<T1,T2,T3>>(obj);

    /// <summary>
    /// Subscribes reciever of event
    /// </summary>
    /// <param name="subscriber">To unsubscribe use UnsubscribeNullSafe extension method.
    /// subscriber will be called when something is published to event</param>
    public void Subscribe(Action<T1,T2,T3> subscriber);
    internal void Unsubscribe(Action<T1,T2,T3> subscriber);

    /// <summary>
    /// Subscribes reciever of event
    /// </summary>
    /// <param name="subscriber">To unsubscribe use UnsubscribeNullSafe extension method.
    /// subscriber will be called when something is published to event</param>
    public void Subscribe(UnityEvent<T1,T2,T3> subscriber);
    internal void Unsubscribe(UnityEvent<T1,T2,T3> subscriber);

    /// <summary>
    /// Subscribes reciever of event
    /// </summary>
    /// <param name="subscriber">To unsubscribe use UnsubscribeNullSafe extension method.
    /// subscriber will be called when something is published to event</param>
    public void Subscribe(ISubscriberToEvent<T1,T2,T3> subscriber);
    internal void Unsubscribe(ISubscriberToEvent<T1,T2,T3> subscriber);

}

