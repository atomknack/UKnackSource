using FluentAssertions;
using FluentAssertions.Execution;
using UKnack;
using UKnack.Common;

namespace UKnackTests;

public class EnumCachedDistinctShort_Tests
{
    enum SeasonsShort: short
    {
        Spring = 100,
        Summer = -10,
        Autumn = 20,
        Winter = 1000,
        // NoSeason = 1,
        // UnknownSeason = 1 // Two same values will throw
    }
    [Fact]
    public void Names_Test()
    {
        var expected = Enum.GetNames<SeasonsShort>();
        expected.Length.Should().Be(4);
        var actual = EnumCachedDistinct<SeasonsShort>.Names.ToArray();
        actual.Should().Equal(expected);
    }
    [Fact]
    public void Spring_Test()
    {
        var expected = Enum.GetName(SeasonsShort.Spring);
        var actual = EnumCachedDistinct<SeasonsShort>.GetName(SeasonsShort.Spring);
        actual.Should().Be(expected);
    }
    [Fact]
    public void SummerTest()
    {
        var expected = Enum.GetName(SeasonsShort.Summer);
        var actual = EnumCachedDistinct<SeasonsShort>.GetName(SeasonsShort.Summer);
        actual.Should().Be(expected);
    }
    [Fact]
    public void AutumnTest()
    {
        var expected = Enum.GetName(SeasonsShort.Autumn);
        var actual = EnumCachedDistinct<SeasonsShort>.GetName(SeasonsShort.Autumn);
        actual.Should().Be(expected);
    }
    [Fact]
    public void Winter_Test()
    {
        var expected = Enum.GetName<SeasonsShort>(SeasonsShort.Winter);
        var actual = EnumCachedDistinct<SeasonsShort>.GetName(SeasonsShort.Winter);
        actual.Should().Be(expected);
    }
}
