//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EventInterfaces
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
    
using UKnack.Common;

namespace UKnack.Events;

public interface IPublisher<T> 
{
    void Publish(T t);

    public static IPublisher<T> Validate(UnityEngine.Object obj) => 
        CommonStatic.ValidCast<IPublisher<T>>(obj);
}
