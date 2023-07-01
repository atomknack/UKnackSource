using UKnack.Common;

namespace UKnack.KeyValues;

public partial interface IKeyValue<TKey, TValue> : IKeyValueGetter<TKey, TValue>
{
    bool ContainsKey(TKey key);
    TValue this[TKey key]
    {
        get;
    }
}

