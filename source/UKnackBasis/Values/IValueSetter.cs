
namespace UKnack.Values;
public interface IValueSetter<T>
{
    public void SetValue (T value);

    public static IValueSetter<T> Validate(UnityEngine.Object obj) => 
        CommonStatic.ValidCast<IValueSetter<T>>(obj);

}
