using UKnack.Common;

namespace UKnack.KeyValues;


public partial interface IKeyValueMutable<TKey, TValue> : IKeyValue<TKey, TValue>, IKeyValueSetter<TKey, TValue>
{
    bool Remove(TKey key);
    new TValue this[TKey key]
    {
        get;
        set;
    }
}