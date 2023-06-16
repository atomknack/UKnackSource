using UKnack.Common;
namespace UKnack.Values;

public interface IValueMutable<T> : IValue<T>, IValueSetter<T>
{
    public new static IValueMutable<T> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IValueMutable<T>>(obj);
}

