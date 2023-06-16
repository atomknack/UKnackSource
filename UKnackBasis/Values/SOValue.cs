using UKnack.Events;
using UKnack.Values;

namespace UKnack.Values;

public abstract class SOValue<T> : SOEvent<T>, IValue<T>
{
    //public virtual T Value { get { return GetValue(); }}
    public abstract T GetValue();
}