// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using System;
    using Xunit.Combinatorial.Tests.Utils;
    using Xunit.ValueAttributes;

    public sealed class RandomAttributeTests
    {
        #region RejectsLowerValueOutOfRangeAbove

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(1000, 0, 1)] byte b) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(1000, 0, 1)] sbyte s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(100000, 0, 1)] ushort u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(100000, 0, 1)] short s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(10000000000L, 0L, 1)] uint u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(10000000000UL, 0UL, 1)] int i) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Random(10000000000000000000UL, 0UL, 1)] long l) { }

        #endregion

        #region RejectsLowerValueOutOfRangeBelow

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-1, 0, 1)] byte b) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-1000, 0, 1)] sbyte s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-1, 0, 1)] ushort u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-100000, 0, 1)] short s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-1, 0, 1)] uint u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-10000000000, 0, 1)] int i) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Random(-1, 0, 1)] ulong u) { }

        #endregion

        #region RejectsUpperValueOutOfRangeAbove

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 1000, 1)] byte b) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 1000, 1)] sbyte s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 100000, 1)] ushort u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 100000, 1)] short s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 10000000000, 1)] uint u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 10000000000, 1)] int i) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Random(0, 10000000000000000000, 1)] long l) { }

        #endregion

        #region RejectsUpperValueOutOfRangeBelow

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -1, 1)] byte b) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -1000, 1)] sbyte s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -1, 1)] ushort u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -100000, 1)] short s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -1, 1)] uint u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -10000000000, 1)] int i) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Random(0, -1, 1)] ulong u) { }

        #endregion
        
        #region RejectsZeroNumberOfValues

        [Fact]
        public void RejectsZeroNumberOfValuesInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsZeroNumberOfValuesInt64);
            });
        }

        private static void RejectsZeroNumberOfValuesInt64([Random(0, 1, 0)] long l) { }

        [Fact]
        public void RejectsZeroNumberOfValuesUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsZeroNumberOfValuesUInt64);
            });
        }

        private static void RejectsZeroNumberOfValuesUInt64([Random(0, 1, 0)] ulong u) { }

        #endregion

        #region RejectsNegativeNumberOfValues

        [Fact]
        public void RejectsNegativeNumberOfValuesInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsNegativeNumberOfValuesInt64);
            });
        }

        private static void RejectsNegativeNumberOfValuesInt64([Random(0, 1, -1)] long l) { }

        [Fact]
        public void RejectsNegativeNumberOfValuesUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsNegativeNumberOfValuesUInt64);
            });
        }

        private static void RejectsNegativeNumberOfValuesUInt64([Random(0, 1, -1)] ulong u) { }

        #endregion

        #region ProducesCorrectNumberOfValues

        [Fact]
        public void ProducesCorrectNumberOfValuesByte()
        {
            var data = Data.Generate<byte>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(15, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(15)] byte b) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesSByte()
        {
            var data = Data.Generate<sbyte>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(53, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(53)] sbyte s) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesUInt16()
        {
            var data = Data.Generate<ushort>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(16, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(16)] ushort u) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesInt16()
        {
            var data = Data.Generate<short>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(63, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(63)] short s) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesUInt32()
        {
            var data = Data.Generate<uint>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(17, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(17)] uint u) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesInt32()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(72, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(72)] int i) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesUInt64()
        {
            var data = Data.Generate<ulong>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(18, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(18)] ulong u) { }

        [Fact]
        public void ProducesCorrectNumberOfValuesInt64()
        {
            var data = Data.Generate<long>(new CombinatorialDataAttribute(), ProducesCorrectNumberOfValues);

            Assert.Equal(82, data.Length);
        }

        private static void ProducesCorrectNumberOfValues([Random(82)] long l) { }

        #endregion

        [Fact]
        public void RejectsNonNumericTypes()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<string>(new CombinatorialDataAttribute(), RejectsNonNumericTypes);
            });
        }

        private static void RejectsNonNumericTypes([Random(0, 1, 1)] string s) { }

        [Fact]
        public void RejectsNonIntegerNumericTypes()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<double>(new CombinatorialDataAttribute(), RejectsNonIntegerNumericTypes);
            });
        }

        private static void RejectsNonIntegerNumericTypes([Random(0, 1, 1)] double d) { }

        #region SameSeedProducesSameSequence

        [Fact]
        public void SameSeedProducesSameSequenceByte()
        {
            var firstSequence = Data.Generate<byte>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<byte>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 1559595546)] byte b) { }

        [Fact]
        public void SameSeedProducesSameSequenceSByte()
        {
            var firstSequence = Data.Generate<sbyte>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<sbyte>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 534011718)] sbyte s) { }

        [Fact]
        public void SameSeedProducesSameSequenceUInt16()
        {
            var firstSequence = Data.Generate<ushort>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<ushort>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 1655911537)] ushort u) { }

        [Fact]
        public void SameSeedProducesSameSequenceInt16()
        {
            var firstSequence = Data.Generate<short>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<short>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 630327709)] short s) { }

        [Fact]
        public void SameSeedProducesSameSequenceUInt32()
        {
            var firstSequence = Data.Generate<uint>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<uint>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 1752227528)] uint u) { }

        [Fact]
        public void SameSeedProducesSameSequenceInt32()
        {
            var firstSequence = Data.Generate<int>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<int>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 726643700)] int i) { }

        [Fact]
        public void SameSeedProducesSameSequenceUInt64()
        {
            var firstSequence = Data.Generate<ulong>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<ulong>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 1848543519)] ulong u) { }

        [Fact]
        public void SameSeedProducesSameSequenceInt64()
        {
            var firstSequence = Data.Generate<long>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);
            var secondSequence = Data.Generate<long>(new CombinatorialDataAttribute(), SameSeedProducesSameSequence);

            AssertSets.Equal(firstSequence, secondSequence);
        }

        private static void SameSeedProducesSameSequence([Random(100, 822959691)] long l) { }

        #endregion
    }
}