//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from IValueGenerators
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UKnack.Common;
using UKnack.Events;

namespace UKnack.Values;

public partial interface IValue<T> : IEvent<T>, IValueGetter<T>
{
    public new static IValue<T> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IValue<T>>(obj);
}


