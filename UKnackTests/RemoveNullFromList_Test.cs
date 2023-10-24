using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKnackTests;

public class RemoveNullFromList_Test
{

    [Fact]
    public void ListOfStrings()
    {
        var list = new List<string>();
        list.Add("a");
        list.Add("b");
        list.Add(null);
        list.Add("c");
        list.Add(null);
        list.Count.Should().Be(5);
        list.Remove(null).Should().Be(true);
        list.Count.Should().Be(4);

    }
}
