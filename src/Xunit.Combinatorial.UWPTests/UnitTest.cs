using System;
using Xunit;

public class UnitTest1
{
    [Fact]
    public void OrdinaryFact()
    {
    }

    [Theory, CombinatorialData]
    public void TestMethod1(bool hi)
    {
    }
}
