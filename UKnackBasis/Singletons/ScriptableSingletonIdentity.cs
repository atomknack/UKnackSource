using System;
using System.Collections.Concurrent;
using UKnack.Common;

namespace UKnack.Singletons;

public abstract partial class ScriptableSingletonIdentity: ScriptableObjectWithReadOnlyName, IHaveDescription
{
    public abstract string Description { get; }

    [NonSerialized] 
    protected static readonly ConcurrentDictionary<ScriptableSingletonIdentity, object> s_globalRegistry = new();
    [NonSerialized]
    protected int _version = 0;

    public virtual bool IsAlreadyRegistered() =>
        s_globalRegistry.ContainsKey(this);

    public virtual bool TryRegister(out SuccessfullyRegisteredTag registeredReference, object? value = null)
    {
        if(s_globalRegistry.TryAdd(this, value!))
        {
            _version++;
            registeredReference = new SuccessfullyRegisteredTag(this, _version);
            return true;
        }
        registeredReference = default(SuccessfullyRegisteredTag);
        return false;
    }

    public virtual bool Unregister(SuccessfullyRegisteredTag registeredReference, object? value = null)
    {
        if (registeredReference.version != _version)
            return false;

        if (s_globalRegistry.TryRemove(registeredReference.reference, out _))
        {
            _version++;
            return true;
        }
        return false;
    }
}
