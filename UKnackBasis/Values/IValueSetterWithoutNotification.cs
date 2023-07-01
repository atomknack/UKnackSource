
using UKnack.Common;

namespace UKnack.Values;
public interface IValueSetterWithoutNotification<T>
{
    void SetValueWithoutNotify(T value);
}
