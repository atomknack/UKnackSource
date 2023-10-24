using FluentAssertions.Execution;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKnack;

namespace UKnackTests;

public interface IHaveDefaultMethod
{
    static int GetInt(IHaveDefaultMethod t)
    {
        if (t == null)
            return -1;
        return t.GetIntValue();
    }
    internal int GetIntValue();
}
public static class IHaveDefaultMethodExtension
{
    public static int Get(this IHaveDefaultMethod t)
    {
        if (t == null)
            return -10;
        return t.GetIntValue();
    }

}

public class InterfaceDefaultForNull_Tests
{
    [Fact]
    public void Null_Test()
    {
        IHaveDefaultMethod n = null;
        IHaveDefaultMethod.GetInt(n).Should().Be(-1);
        n.Get().Should().Be(-10);
    }
}

