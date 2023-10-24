
namespace UKnackTests.ConditionalAttributesTest;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
[System.Diagnostics.Conditional("Release")]
internal class AReleaseAttribute : Attribute
{
    internal static int Add500(int value) => value + 500;
}
