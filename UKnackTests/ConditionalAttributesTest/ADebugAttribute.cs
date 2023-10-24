
namespace UKnackTests.ConditionalAttributesTest;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
[System.Diagnostics.Conditional("Release")]
internal class ADebugAttribute : Attribute
{
    internal static int Add100(int value) => value + 100;
}
