
using FluentAssertions;

namespace UKnackTests.ConditionalAttributesTest;

public class ConditionalAttribute_Tests
{
    [Fact]
    public void ConditionalDebugStaticMethodCall_Test()
    {
        int i = 0; 
        i = ADebugAttribute.Add100(7);
        i.Should().Be(107);
    }
    [Fact]
    public void ConditionalReleasetaticMethodCall_Test()
    {
        int i = 0;
        i = AReleaseAttribute.Add500(4);
        i.Should().Be(504);
    }
}
