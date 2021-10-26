// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

using Xunit;

public class CombinatorialValuesAttributeTests
{
    [Fact]
    public void Ctor_SetsProperty()
    {
        object[] values = new object[1];
        var attribute = new CombinatorialValuesAttribute(values);
        Assert.Same(values, attribute.Values);
    }

    [Fact]
    public void NullArg()
    {
        var attribute = new CombinatorialValuesAttribute(null);
        Assert.Equal(new object?[] { null }, attribute.Values);
    }

    [Fact]
    public void NullArgInArray()
    {
        var attribute = new CombinatorialValuesAttribute(new object?[] { null });
        Assert.Equal(new object?[] { null }, attribute.Values);
    }
}
