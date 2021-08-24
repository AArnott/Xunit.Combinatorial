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
            => Check(new CombinatorialRandomAttribute(count), count);

        [Theory]
        [InlineData(1, 25)]
        [InlineData(3, 100)]
        [InlineData(24, 1000)]
        public void ConstructorCountMaxValue(int count, int maxValue)
            => Check(new CombinatorialRandomAttribute(count, maxValue), count, maxValue: maxValue);

        [Theory]
        [InlineData(1, 25, 35)]
        [InlineData(3, 100, 200)]
        [InlineData(24, 1000, 1001)]
        public void ConstructorCountMinMaxValues(int count, int minValue, int maxValue)
            => Check(new CombinatorialRandomAttribute(count, minValue, maxValue), count, minValue, maxValue);

        [Theory]
        [InlineData(1, 25, 35, 234)]
        [InlineData(3, 100, 200, 1343)]
        [InlineData(24, 1000, 3000, 56732)]
        [InlineData(12, 576, 765, CombinatorialRandomAttribute.NoSeed)]
        public void ConstructorCountMinMaxValuesSeed(int count, int minValue, int maxValue, int seed)
            => Check(new CombinatorialRandomAttribute(count, minValue, maxValue, seed), count, minValue, maxValue, seed);

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ConstructorOutOfRangeCount(int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRandomAttribute(count));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRandomAttribute(count, 100));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRandomAttribute(count, 0, 100));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRandomAttribute(count, 0, 100, 325));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRandomAttribute(count, 0, 100, CombinatorialRandomAttribute.NoSeed));
        }

        internal void Check(
            CombinatorialRandomAttribute attribute, 
            int count = CombinatorialRandomAttribute.DefaultCount,
            int minValue = CombinatorialRandomAttribute.DefaultMinValue,
            int maxValue = CombinatorialRandomAttribute.DefaultMaxValue,
            int seed = CombinatorialRandomAttribute.NoSeed)
        {
            Assert.NotNull(attribute.Values);
            Assert.Equal(count, attribute.Values.Length);

            Assert.All(attribute.Values, value =>
            {
                Assert.IsType<int>(value);
                var intValue = (int)value;
                Assert.InRange(intValue, minValue, maxValue - 1);
            });

            if (seed != CombinatorialRandomAttribute.NoSeed)
            {
                Assert.Equal(RandomIterator(count, new Random(seed), minValue, maxValue), attribute.Values.Cast<int>());
            }
        }

        internal static IEnumerable<int> RandomIterator(int count, Random random, int minValue, int maxValue)
        {
            for (int i = 0; i < count; i++)
            {
                yield return random.Next(minValue, maxValue);
            }
        }
    }
}