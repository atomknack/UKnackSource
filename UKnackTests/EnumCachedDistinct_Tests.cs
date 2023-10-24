using FluentAssertions;
using UKnack;
using UKnack.Common;

namespace UKnackTests;

public class EnumCachedDistinct_Tests
{
    enum Seasons
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }
    [Fact]
    public void Names_Test()
    {
        var expected = Enum.GetNames<Seasons>();
        expected.Length.Should().Be(4);
        var actual = EnumCachedDistinct<Seasons>.Names.ToArray();
        actual.Should().Equal(expected);
    }
    [Fact]
    public void Spring_Test()
    {
        var expected = Enum.GetName(Seasons.Spring);
        var actual = EnumCachedDistinct<Seasons>.GetName(Seasons.Spring);
        actual.Should().Be(expected);
    }
    [Fact]
    public void SummerTest()
    {
        var expected = Enum.GetName(Seasons.Summer);
        var actual = EnumCachedDistinct<Seasons>.GetName(Seasons.Summer);
        actual.Should().Be(expected);
    }
    [Fact]
    public void AutumnTest()
    {
        var expected = Enum.GetName(Seasons.Autumn);
        var actual = EnumCachedDistinct<Seasons>.GetName(Seasons.Autumn);
        actual.Should().Be(expected);
    }
    [Fact]
    public void Winter_Test()
    {
        var expected = Enum.GetName<Seasons>(Seasons.Winter);
        var actual = EnumCachedDistinct<Seasons>.GetName(Seasons.Winter);
        actual.Should().Be(expected);
    }
}
