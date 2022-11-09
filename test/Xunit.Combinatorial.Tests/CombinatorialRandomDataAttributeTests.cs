// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit;

public class CombinatorialRandomDataAttributeTests
{
    [Fact]
    public void ConstructorNoParams()
        => Check(new CombinatorialRandomDataAttribute());

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(25)]
    public void ConstructorCount(int count)
        => Check(new CombinatorialRandomDataAttribute { Count = count });

    [Theory]
    [InlineData(1, 25)]
    [InlineData(3, 100)]
    [InlineData(24, 1000)]
    public void ConstructorCountMaxValue(int count, int maxValue)
        => Check(new CombinatorialRandomDataAttribute { Count = count, Maximum = maxValue });

    [Theory]
    [InlineData(1, 25, 35)]
    [InlineData(3, 100, 200)]
    public void ConstructorCountMinMaxValues(int count, int minValue, int maxValue)
        => Check(new CombinatorialRandomDataAttribute { Count = count, Minimum = minValue, Maximum = maxValue });

    [Theory]
    [InlineData(1, 25, 35, 234)]
    [InlineData(3, 100, 200, 1343)]
    [InlineData(24, 1000, 3000, 56732)]
    [InlineData(12, 576, 765, 0)]
    public void ConstructorCountMinMaxValuesSeed(int count, int minValue, int maxValue, int seed)
        => Check(new CombinatorialRandomDataAttribute { Count = count, Minimum = minValue, Maximum = maxValue, Seed = seed });

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void ConstructorOutOfRangeCount(int count)
    {
        Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomDataAttribute { Count = count }.Values);
    }

    [Fact]
    public void CountHigherThanRangeSize()
    {
        Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomDataAttribute { Count = 6, Minimum = 1, Maximum = 5 }.Values);
        Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomDataAttribute { Count = 4, Minimum = -3, Maximum = -1 }.Values);
        _ = new CombinatorialRandomDataAttribute { Count = 3, Minimum = 1, Maximum = 3 }.Values;
        _ = new CombinatorialRandomDataAttribute { Count = 3, Minimum = -3, Maximum = -1 }.Values;
    }

    internal static void Check(CombinatorialRandomDataAttribute attribute)
    {
        Assert.NotNull(attribute.Values);
        Assert.Equal(attribute.Count, attribute.Values.Length);

        Assert.All(attribute.Values, value =>
        {
            Assert.IsType<int>(value);
            int intValue = (int)value;
            Assert.InRange(intValue, attribute.Minimum, attribute.Maximum);
        });

        if (attribute.Seed != CombinatorialRandomDataAttribute.NoSeed)
        {
            Assert.Equal(RandomIterator(new Random(attribute.Seed), attribute.Minimum, attribute.Maximum).Distinct().Take(attribute.Count).ToArray(), attribute.Values.Cast<int>());
        }
    }

    internal static IEnumerable<int> RandomIterator(Random random, int minValue, int maxValue)
    {
        while (true)
        {
            int value = random.Next(minValue, maxValue + 1);
            yield return value;
        }
    }
}
