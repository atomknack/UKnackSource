namespace UKnack.Singletons;

public abstract partial class ScriptableSingletonIdentity
{
    public readonly struct SuccessfullyRegisteredTag
    {
        internal readonly ScriptableSingletonIdentity reference;
        internal readonly long version;

        internal SuccessfullyRegisteredTag(ScriptableSingletonIdentity reference, long version)
        {
            this.reference = reference;
            this.version = version;
        }
    }
}
