// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Linq;
    using Xunit.Combinatorial.Tests.Utils;

    public sealed class RangeAttributeTests
    {
        #region GeneratesCorrectRangeInt64

        [Fact]
        public void GeneratesCorrectRangeInt64Byte()
        {
            var data = Data.Generate<byte>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new byte[] { 10, 13, 16, 19, 22, 25, 28, 31, 34, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(10, 38, 3)] byte b) { }

        [Fact]
        public void GeneratesCorrectRangeInt64SByte()
        {
            var data = Data.Generate<sbyte>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(1, 32, 1)] sbyte s) { }

        [Fact]
        public void GeneratesCorrectRangeInt64UInt16()
        {
            var data = Data.Generate<ushort>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new ushort[] { 1, 5, 9, 13, 17, 21 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(1, 22, 4)] ushort u) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Int16()
        {
            var data = Data.Generate<short>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new short[] { 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(13, 34, 2)] short s) { }

        [Fact]
        public void GeneratesCorrectRangeInt64UInt32()
        {
            var data = Data.Generate<uint>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new uint[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(8, 29, 1)] uint u) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Int32()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(6, 37, 1)] int i) { }

        [Fact]
        public void GeneratesCorrectRangeInt64UInt64()
        {
            var data = Data.Generate<ulong>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new ulong[] { 8, 11, 14, 17, 20, 23, 26, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(8, 29, 3)] ulong u) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Int64()
        {
            var data = Data.Generate<long>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new long[] { 2, 6, 10, 14, 18, 22 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(2, 23, 4)] long l) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(7, 29, 3)] float f) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(0, 36, 4)] double d) { }

        [Fact]
        public void GeneratesCorrectRangeInt64Decimal()
        {
            var data = Data.Generate<decimal>(new CombinatorialDataAttribute(), GeneratesCorrectRangeInt64);
            var expected = new decimal[] { 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeInt64([Range(14, 36, 2)] decimal d) { }

        #endregion

        #region GeneratesCorrectRangeUInt64

        [Fact]
        public void GeneratesCorrectRangeUInt64Byte()
        {
            var data = Data.Generate<byte>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new byte[] { 10, 13, 16, 19, 22, 25, 28, 31, 34, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(10UL, 38UL, 3UL)] byte b) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64SByte()
        {
            var data = Data.Generate<sbyte>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(1UL, 32UL, 1UL)] sbyte s) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64UInt16()
        {
            var data = Data.Generate<ushort>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new ushort[] { 1, 5, 9, 13, 17, 21 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(1UL, 22UL, 4UL)] ushort u) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Int16()
        {
            var data = Data.Generate<short>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new short[] { 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(13UL, 34UL, 2UL)] short s) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64UInt32()
        {
            var data = Data.Generate<uint>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new uint[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(8UL, 29UL, 1UL)] uint u) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Int32()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(6UL, 37UL, 1UL)] int i) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64UInt64()
        {
            var data = Data.Generate<ulong>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new ulong[] { 8, 11, 14, 17, 20, 23, 26, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(8UL, 29UL, 3UL)] ulong u) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Int64()
        {
            var data = Data.Generate<long>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new long[] { 2, 6, 10, 14, 18, 22 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(2UL, 23UL, 4UL)] long l) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(7UL, 29UL, 3UL)] float f) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(0UL, 36UL, 4UL)] double d) { }

        [Fact]
        public void GeneratesCorrectRangeUInt64Decimal()
        {
            var data = Data.Generate<decimal>(new CombinatorialDataAttribute(), GeneratesCorrectRangeUInt64);
            var expected = new decimal[] { 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeUInt64([Range(14UL, 36UL, 2UL)] decimal d) { }

        #endregion
        
        [Fact]
        public void GeneratesCorrectRangeSingleSingle()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), GeneratesCorrectRangeSingle);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeSingle([Range(7F, 29F, 3F)] float f) { }

        [Fact]
        public void GeneratesCorrectRangeSingleDouble()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), GeneratesCorrectRangeSingle);
            var expected = new double[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeSingle([Range(7F, 29F, 3F)] double d) { }

        [Fact]
        public void GeneratesCorrectRangeDoubleDouble()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), GeneratesCorrectRangeDouble);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GeneratesCorrectRangeDouble([Range(0D, 36D, 4D)] double d) { }
        
        #region RejectsLowerValueOutOfRangeAbove

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(1000, 0, 1)] byte b) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(1000, 0, 1)] sbyte s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(100000, 0, 1)] ushort u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(100000, 0, 1)] short s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(10000000000, 0, 1)] uint u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(10000000000, 0, 1)] int i) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeAboveInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeAbove);
            });
        }

        private static void RejectsLowerValueOutOfRangeAbove([Range(10000000000000000000, 0, 1)] long l) { }
        
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

        private static void RejectsLowerValueOutOfRangeBelow([Range(-1, 0, 1)] byte b) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-1000, 0, 1)] sbyte s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-1, 0, 1)] ushort u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-100000, 0, 1)] short s) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-1, 0, 1)] uint u) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-10000000000, 0, 1)] int i) { }

        [Fact]
        public void RejectsLowerValueOutOfRangeBelowUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsLowerValueOutOfRangeBelow);
            });
        }

        private static void RejectsLowerValueOutOfRangeBelow([Range(-1, 0, 1)] ulong u) { }

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

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 1000, 1)] byte b) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 1000, 1)] sbyte s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 100000, 1)] ushort u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 100000, 1)] short s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 10000000000, 1)] uint u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 10000000000, 1)] int i) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeAboveInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeAbove);
            });
        }

        private static void RejectsUpperValueOutOfRangeAbove([Range(0, 10000000000000000000, 1)] long l) { }

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

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -1, 1)] byte b) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -1000, 1)] sbyte s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -1, 1)] ushort u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -100000, 1)] short s) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -1, 1)] uint u) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -10000000000, 1)] int i) { }

        [Fact]
        public void RejectsUpperValueOutOfRangeBelowUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsUpperValueOutOfRangeBelow);
            });
        }

        private static void RejectsUpperValueOutOfRangeBelow([Range(0, -1, 1)] ulong u) { }
        
        #endregion

        #region RejectsStepValueOutOfRangeAbove

        [Fact]
        public void RejectsStepValueOutOfRangeAboveByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 1000)] byte b) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveSByte()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 1000)] sbyte s) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveUInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 100000)] ushort u) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveInt16()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 100000)] short s) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveUInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 10000000000)] uint u) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 10000000000)] int i) { }

        [Fact]
        public void RejectsStepValueOutOfRangeAboveInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsStepValueOutOfRangeAbove);
            });
        }

        private static void RejectsStepValueOutOfRangeAbove([Range(0, 1, 10000000000000000000)] long l) { }
        
        #endregion

        #region RejectsZeroStepValue

        [Fact]
        public void RejectsZeroStepValueInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsZeroStepValueInt64);
            });
        }

        private static void RejectsZeroStepValueInt64([Range(0, 1, 0)] long l) { }

        [Fact]
        public void RejectsZeroStepValueUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsZeroStepValueUInt64);
            });
        }

        private static void RejectsZeroStepValueUInt64([Range(0, 1, 0)] ulong u) { }

        [Fact]
        public void RejectsZeroStepValueSingle()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<float>(new CombinatorialDataAttribute(), RejectsZeroStepValueSingle);
            });
        }

        private static void RejectsZeroStepValueSingle([Range(0F, 1F, 0F)] float f) { }

        [Fact]
        public void RejectsZeroStepValueDouble()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<double>(new CombinatorialDataAttribute(), RejectsZeroStepValueDouble);
            });
        }

        private static void RejectsZeroStepValueDouble([Range(0D, 1D, 0D)] double d) { }

        #endregion

        #region RejectsNegativeStepValue

        [Fact]
        public void RejectsNegativeStepValueInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsNegativeStepValueInt64);
            });
        }

        private static void RejectsNegativeStepValueInt64([Range(0, 1, -1)] long l) { }

        [Fact]
        public void RejectsNegativeStepValueUInt64()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsNegativeStepValueUInt64);
            });
        }

        private static void RejectsNegativeStepValueUInt64([Range(0, 1, -1)] ulong u) { }

        [Fact]
        public void RejectsNegativeStepValueSingle()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<float>(new CombinatorialDataAttribute(), RejectsNegativeStepValueSingle);
            });
        }

        private static void RejectsNegativeStepValueSingle([Range(0F, 1F, -1F)] float f) { }

        [Fact]
        public void RejectsNegativeStepValueDouble()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Data.Generate<double>(new CombinatorialDataAttribute(), RejectsNegativeStepValueDouble);
            });
        }

        private static void RejectsNegativeStepValueDouble([Range(0D, 1D, -1D)] double d) { }

        #endregion

        #region IntegerValuesAttachToFloatingParameters

        /* This set of tests is technically redundant as these combinations are all tested above, but it's nice to explicitly call out that this is expected behavior */
        
        [Fact]
        public void IntegerValuesAttachToFloatingParametersInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), IntegerValuesAttachToFloatingParametersInt64Single);
            var expected = new float[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void IntegerValuesAttachToFloatingParametersInt64Single([Range(0, 36, 4)] float f) { }

        [Fact]
        public void IntegerValuesAttachToFloatingParametersInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), IntegerValuesAttachToFloatingParametersInt64Double);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void IntegerValuesAttachToFloatingParametersInt64Double([Range(0, 36, 4)] double d) { }

        [Fact]
        public void IntegerValuesAttachToFloatingParametersUInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), IntegerValuesAttachToFloatingParametersUInt64Single);
            var expected = new float[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void IntegerValuesAttachToFloatingParametersUInt64Single([Range(0UL, 36UL, 4UL)] float f) { }

        [Fact]
        public void IntegerValuesAttachToFloatingParametersUInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), IntegerValuesAttachToFloatingParametersUInt64Double);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void IntegerValuesAttachToFloatingParametersUInt64Double([Range(0UL, 36UL, 4UL)] double d) { }

        #endregion

        #region RejectFloatingValuesOnIntegerParametersSingle

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleByte()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(13F, 30F, 2F)] byte b) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleSByte()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(2F, 30F, 4F)] sbyte s) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleUInt16()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(1F, 39F, 2F)] ushort u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleInt16()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(19F, 28F, 5F)] short s) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleUInt32()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(4F, 24F, 5F)] uint u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleInt32()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(10F, 33F, 3F)] int i) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleUInt64()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(8F, 26F, 3F)] ulong u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersSingleInt64()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersSingle);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersSingle([Range(17F, 26F, 4F)] long l) { }

        #endregion

        #region RejectFloatingValuesOnIntegerParametersDouble

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleByte()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(13D, 30D, 2D)] byte b) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleSByte()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(2D, 30D, 4D)] sbyte s) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleUInt16()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(1D, 39D, 2D)] ushort u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleInt16()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(19D, 28D, 5D)] short s) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleUInt32()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(4D, 24D, 5D)] uint u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleInt32()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(10D, 33D, 3D)] int i) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleUInt64()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(8D, 26D, 3D)] ulong u) { }

        [Fact]
        public void RejectFloatingValuesOnIntegerParametersDoubleInt64()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectFloatingValuesOnIntegerParametersDouble);
            });
        }

        private static void RejectFloatingValuesOnIntegerParametersDouble([Range(17D, 26D, 4D)] long l) { }

        #endregion

        #region SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Byte()
        {
            var data = Data.Generate<byte>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new byte[] { 10, 13, 16, 19, 22, 25, 28, 31, 34, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(38, 10, 3)] byte b) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64SByte()
        {
            var data = Data.Generate<sbyte>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(32, 1, 1)] sbyte s) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64UInt16()
        {
            var data = Data.Generate<ushort>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new ushort[] { 1, 5, 9, 13, 17, 21 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(22, 1, 4)] ushort u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Int16()
        {
            var data = Data.Generate<short>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new short[] { 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(34, 13, 2)] short s) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64UInt32()
        {
            var data = Data.Generate<uint>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new uint[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(29, 8, 1)] uint u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Int32()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(37, 6, 1)] int i) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64UInt64()
        {
            var data = Data.Generate<ulong>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new ulong[] { 8, 11, 14, 17, 20, 23, 26, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(29, 8, 3)] ulong u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Int64()
        {
            var data = Data.Generate<long>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new long[] { 2, 6, 10, 14, 18, 22 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(23, 2, 4)] long l) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(29, 7, 3)] float f) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundInt64([Range(36, 0, 4)] double d) { }

        #endregion

        #region SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Byte()
        {
            var data = Data.Generate<byte>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new byte[] { 10, 13, 16, 19, 22, 25, 28, 31, 34, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(38UL, 10UL, 3UL)] byte b) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64SByte()
        {
            var data = Data.Generate<sbyte>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(32UL, 1UL, 1UL)] sbyte s) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64UInt16()
        {
            var data = Data.Generate<ushort>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new ushort[] { 1, 5, 9, 13, 17, 21 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(22UL, 1UL, 4UL)] ushort u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Int16()
        {
            var data = Data.Generate<short>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new short[] { 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(34UL, 13UL, 2UL)] short s) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64UInt32()
        {
            var data = Data.Generate<uint>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new uint[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(29UL, 8UL, 1UL)] uint u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Int32()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(37UL, 6UL, 1UL)] int i) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64UInt64()
        {
            var data = Data.Generate<ulong>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new ulong[] { 8, 11, 14, 17, 20, 23, 26, 29 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(29UL, 8UL, 3UL)] ulong u) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Int64()
        {
            var data = Data.Generate<long>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new long[] { 2, 6, 10, 14, 18, 22 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(23UL, 2UL, 4UL)] long l) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Single()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(29UL, 7UL, 3UL)] float f) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64Double()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundUInt64([Range(36UL, 0UL, 4UL)] double d) { }

        #endregion

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleSingle()
        {
            var data = Data.Generate<float>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleSingle);
            var expected = new float[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleSingle([Range(29F, 7F, 3F)] float f) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleDouble()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleDouble);
            var expected = new double[] { 7, 10, 13, 16, 19, 22, 25, 28 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundSingleDouble([Range(29F, 7F, 3F)] double d) { }

        [Fact]
        public void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundDoubleDouble()
        {
            var data = Data.Generate<double>(new CombinatorialDataAttribute(), SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundDoubleDouble);
            var expected = new double[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void SwitchesUpperAndLowerValuesWhenLowerBoundGreaterThanUpperBoundDoubleDouble([Range(36D, 0D, 4D)] double d) { }
        
        [Fact]
        public void RejectsNonNumericTypes()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<string>(new CombinatorialDataAttribute(), RejectsNonNumericTypes);
            });
        }

        private static void RejectsNonNumericTypes([Range(0, 1, 1)] string s) { }

        [Fact]
        public void AcceptsInt64ValuesForDecimal()
        {
            var data = Data.Generate<decimal>(new CombinatorialDataAttribute(), AcceptsInt64ValuesForDecimal);
            var expected = new decimal[] { 0, 1 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void AcceptsInt64ValuesForDecimal([Range(0, 1, 1)] decimal d) { }

        [Fact]
        public void AcceptsUInt64ValuesForDecimal()
        {
            var data = Data.Generate<decimal>(new CombinatorialDataAttribute(), AcceptsUInt64ValuesForDecimal);
            var expected = new decimal[] { 0, 1 }
              .Select(v => new object[] { v })
              .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void AcceptsUInt64ValuesForDecimal([Range(0UL, 1UL, 1UL)] decimal d) { }

        [Fact]
        public void RejectsSingleValuesForDecimal()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<decimal>(new CombinatorialDataAttribute(), RejectsSingleValuesForDecimal);
            });
        }

        private static void RejectsSingleValuesForDecimal([Range(0F, 1F, 1F)] decimal d) { }

        [Fact]
        public void RejectsDoubleValuesForDecimal()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<decimal>(new CombinatorialDataAttribute(), RejectsDoubleValuesForDecimal);
            });
        }

        private static void RejectsDoubleValuesForDecimal([Range(0D, 1D, 1D)] decimal d) { }

        [Fact]
        public void RejectsDoubleValuesForSingle()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<float>(new CombinatorialDataAttribute(), RejectsDoubleValuesForSingle);
            });
        }

        private static void RejectsDoubleValuesForSingle([Range(0D, 1D, 1D)] float f) { }

        #region RejectsOverflows

        [Fact]
        public void RejectsOverflowsByte()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<byte>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(254, 255, 255)] byte b) { }

        [Fact]
        public void RejectsOverflowsSByte()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<sbyte>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(126, 127, 127)] sbyte s) { }

        [Fact]
        public void RejectsOverflowsUInt16()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<ushort>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(65534, 65535, 65535)] ushort u) { }

        [Fact]
        public void RejectsOverflowsInt16()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<short>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(32766, 32767, 32767)] short s) { }

        [Fact]
        public void RejectsOverflowsUInt32()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<uint>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(4294967294, 4294967295, 4294967295)] uint u) { }

        [Fact]
        public void RejectsOverflowsInt32()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(2147483646, 2147483647, 2147483647)] int i) { }

        [Fact]
        public void RejectsOverflowsUInt64()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<ulong>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(18446744073709551614, 18446744073709551615, 18446744073709551615)] ulong u) { }

        [Fact]
        public void RejectsOverflowsInt64()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<long>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(9223372036854775806, 9223372036854775807, 9223372036854775807)] long l) { }

        [Fact]
        public void RejectsOverflowsSingle()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<float>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(3.40282346638528859e+37F, 3.40282346638528859e+38F, 3.40282346638528859e+38F)] float l) { }

        [Fact]
        public void RejectsOverflowsDouble()
        {
            Assert.Throws<OverflowException>(() =>
            {
                Data.Generate<double>(new CombinatorialDataAttribute(), RejectsOverflows);
            });
        }

        private static void RejectsOverflows([Range(1.7976931348623157E+307, 1.7976931348623157E+308, 1.7976931348623157E+308)] double l) { }

        #endregion
    }
}
