
namespace UKnack.Values;
public interface IValueGetter<T>
{
    public T GetValue();
    public static IValueGetter<T> Validate(UnityEngine.Object obj) =>
        CommonStatic.ValidCast<IValueGetter<T>>(obj);
}
