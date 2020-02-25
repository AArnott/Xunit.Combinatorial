namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CombinatorialRangeAttributeTests
    {
        [Theory]
        [InlineData(0, 5)]
        public void CountOfIntegers_HappyPath_SetsAttributeWithRange(int from, int count)
        {
            object[] values = { 0, 1, 2, 3, 4 };
            var attribute = new CombinatorialRangeAttribute(from, count);
            Assert.Equal(values, attribute.Values);
        }

        [Theory]
        [InlineData(0, -2)]
        public void CountOfIntegers_NegativeCount_ArgOutOfRange(int from, int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRangeAttribute(from, count));
        }

        [Theory]
        [InlineData(0, 7, 2, true)]
        [InlineData(0, 8, 2, false)]
        public void IntegerStep_HappyPath_SetsAttributeWithRange(int from, int to, int step, bool unevenInterval)
        {
            object[] expectedValues = unevenInterval ? new object[]{ 0, 2, 4, 6 } : new object[]{ 0, 2, 4, 6, 8 };

            var attribute = new CombinatorialRangeAttribute(from, to, step);
            Assert.Equal(expectedValues, attribute.Values);
        }

        [Theory]
        [InlineData(4, 2, 1)]
        [InlineData(1, 5, -1)]
        public void IntegerStep_InvalidIntervalAndStep_ArgOutOfRange(int from, int to, int step)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombinatorialRangeAttribute(from, to, step));
        }
    }
}