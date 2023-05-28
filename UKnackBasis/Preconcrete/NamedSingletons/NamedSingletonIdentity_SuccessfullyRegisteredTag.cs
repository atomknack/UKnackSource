namespace UKnack.Preconcrete.NamedSingletons;

public abstract partial class NamedSingletonIdentity
{
    public readonly struct SuccessfullyRegisteredTag
    {
        internal readonly NamedSingletonIdentity reference;
        internal readonly long version;

        internal SuccessfullyRegisteredTag(NamedSingletonIdentity reference, long version)
        {
            this.reference = reference;
            this.version = version;
        }
    }
}
