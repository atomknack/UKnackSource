//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EventInterfaces
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
    
namespace UKnack.Events;

public interface IPublisher<T1,T2> 
{
    void Publish(T1 t1,T2 t2);

    public static IPublisher<T1,T2> Validate(UnityEngine.Object obj) => 
        CommonStatic.ValidCast<IPublisher<T1,T2>>(obj);
}

