//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EventInterfaces
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UKnack.Common;

namespace UKnack.Events;

public interface ISubscriberToEvent<T1,T2,T3> : IHaveDescription
{
    public void OnEventNotification(T1 t1,T2 t2,T3 t3);
    // public string Description { get; }
}
