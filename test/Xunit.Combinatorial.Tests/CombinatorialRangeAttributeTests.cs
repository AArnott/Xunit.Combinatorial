// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit;

public class CombinatorialRangeAttributeTests
{
    [Theory]
    [InlineData(0, 5)]
    public void CountOfIntegers_HappyPath_SetsAttributeWithRange(int from, int count)
    {
        object[] values = Enumerable.Range(from, count).Cast<object>().ToArray();
        var attribute = new CombinatorialRangeAttribute(from, count);
        Assert.Equal(values, attribute.GetValues(null!));
    }

    [Theory]
    [InlineData(0, -2)]
    public void CountOfIntegers_NegativeCount_ArgOutOfRange(int from, int count)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRangeAttribute(from, count));
    }

    [Theory]
    [InlineData(0, 7, 2)]
    [InlineData(0, 8, 2)]
    [InlineData(7, 0, -2)]
    [InlineData(0, -8, -2)]
    public void IntegerStep_HappyPath_SetsAttributeWithRange(int from, int to, int step)
    {
        object[] expectedValues = Sequence(from, to, step).Cast<object>().ToArray();

        var attribute = new CombinatorialRangeAttribute(from, to, step);
        Assert.Equal(expectedValues, attribute.GetValues(null!));
    }

    [Theory]
    [InlineData(4, 2, 1)]
    [InlineData(1, 5, -1)]
    public void IntegerStep_InvalidIntervalAndStep_ArgOutOfRange(int from, int to, int step)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRangeAttribute(from, to, step));
    }

    [Theory]
    [InlineData(0u, 5u)]
    public void CountOfUnsignedIntegers_HappyPath_SetsAttributeWithRange(uint from, uint count)
    {
        object[] values = UnsignedSequence(from, from + count - 1u, 1u).Cast<object>().ToArray();
        var attribute = new CombinatorialRangeAttribute(from, count);
        Assert.Equal(values, attribute.GetValues(null!));
    }

    [Theory]
    [InlineData(0u, 0u)]
    public void CountOfUnsignedIntegers_ZeroCount_ArgOutOfRange(uint from, uint count)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRangeAttribute(from, count));
    }

    [Theory]
    [InlineData(0u, 7u, 2u)]
    [InlineData(0u, 8u, 2u)]
    [InlineData(7u, 0u, 2u)]
    public void UnsignedIntegerStep_HappyPath_SetsAttributeWithRange(uint from, uint to, uint step)
    {
        object[] expectedValues = UnsignedSequence(from, to, step).Cast<object>().ToArray();

        var attribute = new CombinatorialRangeAttribute(from, to, step);
        Assert.Equal(expectedValues, attribute.GetValues(null!));
    }

    internal static IEnumerable<int> Sequence(int from, int to, int step)
        => step >= 0 ? SequenceIterator(from, to, step) : SequenceReverseIterator(from, to, step);

    internal static IEnumerable<uint> UnsignedSequence(uint from, uint to, uint step)
        => from < to ? UnsignedSequenceIterator(from, to, step) : UnsignedSequenceReverseIterator(from, to, step);

    private static IEnumerable<int> SequenceIterator(int from, int to, int step)
    {
        int value = from;
        while (value <= to)
        {
            yield return value;
            unchecked
            {
                value += step;
            }
        }
    }

    private static IEnumerable<int> SequenceReverseIterator(int from, int to, int step)
    {
        int value = from;
        while (value >= to)
        {
            yield return value;
            unchecked
            {
                value += step;
            }
        }
    }

    private static IEnumerable<uint> UnsignedSequenceIterator(uint from, uint to, uint step)
    {
        uint value = from;
        while (value <= to)
        {
            yield return value;
            unchecked
            {
                value += step;
            }
        }
    }

    private static IEnumerable<uint> UnsignedSequenceReverseIterator(uint from, uint to, uint step)
    {
        uint value = from;
        while (value >= to && value <= from)
        {
            yield return value;
            unchecked
            {
                value -= step;
            }
        }
    }
}
