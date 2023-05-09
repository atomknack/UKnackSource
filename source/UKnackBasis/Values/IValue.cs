using UKnack.Events;

namespace UKnack.Values;

public interface IValue<T> : IEvent<T>, IValueGetter<T>
{
    public new static IValue<T> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IValue<T>>(obj);
}

