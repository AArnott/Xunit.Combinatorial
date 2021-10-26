namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CombinatorialRandomAttributeTests
    {
        [Fact]
        public void ConstructorNoParams()
            => Check(new CombinatorialRandomAttribute());

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(25)]
        public void ConstructorCount(int count)
            => Check(new CombinatorialRandomAttribute { Count = count });

        [Theory]
        [InlineData(1, 25)]
        [InlineData(3, 100)]
        [InlineData(24, 1000)]
        public void ConstructorCountMaxValue(int count, int maxValue)
            => Check(new CombinatorialRandomAttribute { Count = count, Maximum = maxValue });

        [Theory]
        [InlineData(1, 25, 35)]
        [InlineData(3, 100, 200)]
        public void ConstructorCountMinMaxValues(int count, int minValue, int maxValue)
            => Check(new CombinatorialRandomAttribute { Count = count, Minimum = minValue, Maximum = maxValue });

        [Theory]
        [InlineData(1, 25, 35, 234)]
        [InlineData(3, 100, 200, 1343)]
        [InlineData(24, 1000, 3000, 56732)]
        [InlineData(12, 576, 765, 0)]
        public void ConstructorCountMinMaxValuesSeed(int count, int minValue, int maxValue, int seed)
            => Check(new CombinatorialRandomAttribute { Count = count, Minimum = minValue, Maximum = maxValue, Seed = seed });

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ConstructorOutOfRangeCount(int count)
        {
            Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomAttribute { Count = count }.Values);
        }

        [Fact]
        public void CountHigherThanRangeSize()
        {
            Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomAttribute { Count = 6, Minimum = 1, Maximum = 5 }.Values);
            Assert.Throws<InvalidOperationException>(() => new CombinatorialRandomAttribute { Count = 4, Minimum = -3, Maximum = -1 }.Values);
            _ = new CombinatorialRandomAttribute { Count = 3, Minimum = 1, Maximum = 3 }.Values;
            _ = new CombinatorialRandomAttribute { Count = 3, Minimum = -3, Maximum = -1 }.Values;
        }

        internal void Check(CombinatorialRandomAttribute attribute)
        {
            Assert.NotNull(attribute.Values);
            Assert.Equal(attribute.Count, attribute.Values.Length);

            Assert.All(attribute.Values, value =>
            {
                Assert.IsType<int>(value);
                var intValue = (int)value;
                Assert.InRange(intValue, attribute.Minimum, attribute.Maximum);
            });

            if (attribute.Seed != CombinatorialRandomAttribute.NoSeed)
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
}