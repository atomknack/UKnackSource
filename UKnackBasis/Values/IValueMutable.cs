//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from IValueGenerators
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UKnack.Common;

namespace UKnack.Values;

public partial interface IValueMutable<T> : IValue<T>, IValueSetter<T>
{
    public new static IValueMutable<T> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IValueMutable<T>>(obj);
}

