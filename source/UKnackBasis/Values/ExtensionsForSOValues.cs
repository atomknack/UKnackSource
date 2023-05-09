using UKnack;

namespace UKnack.Values;

public static class ExtensionsForSOValues
{
    public static void Flip(this SOValueMutable<bool> v)
    {
        v.SetValue(!v.GetValue());
    }
}