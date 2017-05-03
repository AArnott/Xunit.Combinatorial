namespace Xunit.ValueAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Generates random values within a given range
    /// </summary>
    /// <remarks>
    /// Can only be used on the built-in integral types, as these can generate unbiased values across a uniformly distributed range.
    /// </remarks>
    public sealed class RandomAttribute : ParameterValuesAttribute
    {
        private static Random SharedRandom { get; } = new Random().WarmUp();

        private Func<Type, IEnumerable<object>> Generator { get; }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the entire range of the type of the parameter
        /// </summary>
        /// <param name="numberOfValues">The number of random values to generate</param>
        public RandomAttribute(int numberOfValues)
            : this(numberOfValues, null)
        {
        }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the entire range of the type of the parameter, with the random nubmer generator using the specified <paramref name="seed"/>
        /// </summary>
        /// <param name="numberOfValues">The number of random values to generate</param>
        /// <param name="seed">The seed the random number generator should use to initialize itself</param>
        public RandomAttribute(int numberOfValues, int seed)
            : this(numberOfValues, (int?)seed)
        {
        }

        private RandomAttribute(int numberOfValues, int? seed)
        {
            if (numberOfValues <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfValues), string.Format(Strings.RandomNumberOfValuesMustBePositive, nameof(numberOfValues)));
            }

            Generator = type => GetRandomSequence(type, numberOfValues, seed);
        }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the given range, inclusive of the bounds
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range to generate, inclusive</param>
        /// <param name="upperBound">The upper bound of the range to generate, inclusive</param>
        /// <param name="numberOfValues">The number of random values to generate</param>
        public RandomAttribute(long lowerBound, long upperBound, int numberOfValues)
            : this(lowerBound, upperBound, numberOfValues, null)
        {
        }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the given range, inclusive of the bounds, with the random nubmer generator using the specified <paramref name="seed"/>
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range to generate, inclusive</param>
        /// <param name="upperBound">The upper bound of the range to generate, inclusive</param>
        /// <param name="numberOfValues">The number of random values to generate</param>
        /// <param name="seed">The seed the random number generator should use to initialize itself</param>
        public RandomAttribute(long lowerBound, long upperBound, int numberOfValues, int seed)
            : this(lowerBound, upperBound, numberOfValues, (int?)seed)
        {
        }

        private RandomAttribute(long lowerBound, long upperBound, int numberOfValues, int? seed)
        {
            if (numberOfValues <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfValues), string.Format(Strings.RandomNumberOfValuesMustBePositive, nameof(numberOfValues)));
            }

            Generator = type => GetRandomSequenceFromLong(type, lowerBound, upperBound, numberOfValues, seed);
        }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the given range, inclusive of the bounds
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range to generate, inclusive</param>
        /// <param name="upperBound">The upper bound of the range to generate, inclusive</param>
        /// <param name="numberOfValues">The number of random values to generate</param>
        public RandomAttribute(ulong lowerBound, ulong upperBound, int numberOfValues)
            : this(lowerBound, upperBound, numberOfValues, null)
        {
        }

        /// <summary>
        /// Indicates that <paramref name="numberOfValues"/> values should be generated within the given range, inclusive of the bounds, with the random nubmer generator using the specified <paramref name="seed"/>
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range to generate, inclusive</param>
        /// <param name="upperBound">The upper bound of the range to generate, inclusive</param>
        /// <param name="numberOfValues">The number of random values to generate</param>
        /// <param name="seed">The seed the random number generator should use to initialize itself</param>
        public RandomAttribute(ulong lowerBound, ulong upperBound, int numberOfValues, int seed)
            : this(lowerBound, upperBound, numberOfValues, (int?)seed)
        {
        }

        private RandomAttribute(ulong lowerBound, ulong upperBound, int numberOfValues, int? seed)
        {
            if (numberOfValues < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfValues), string.Format(Strings.RandomNumberOfValuesMustBePositive, nameof(numberOfValues)));
            }

            Generator = type => GetRandomSequenceFromULong(type, lowerBound, upperBound, numberOfValues, seed);
        }

        /// <inheritdoc />
        public override IEnumerable<object> GetValues(ParameterInfo parameter)
        {
            var type = parameter.ParameterType;
            if (!type.IsNumericType())
            {
                throw new NotSupportedException(string.Format(Strings.RangeNotNumericType, nameof(RangeAttribute)));
            }

            return Generator(type);
        }

        private static IEnumerable<object> GetRandomSequence(Type type, int numberOfValues, int? seed)
        {
            var rng = seed.HasValue ? new Random(seed.Value).WarmUp() : SharedRandom;
            lock (rng)
            {
                if (type == typeof(byte))
                {
                    return GetRandomSequence(rng, byte.MinValue, byte.MaxValue, numberOfValues);
                }
                else if (type == typeof(sbyte))
                {
                    return GetRandomSequence(rng, sbyte.MinValue, sbyte.MaxValue, numberOfValues);
                }
                else if (type == typeof(ushort))
                {
                    return GetRandomSequence(rng, ushort.MinValue, ushort.MaxValue, numberOfValues);
                }
                else if (type == typeof(short))
                {
                    return GetRandomSequence(rng, short.MinValue, short.MaxValue, numberOfValues);
                }
                else if (type == typeof(uint))
                {
                    return GetRandomSequence(rng, uint.MinValue, uint.MaxValue, numberOfValues);
                }
                else if (type == typeof(int))
                {
                    return GetRandomSequence(rng, int.MinValue, int.MaxValue, numberOfValues);
                }
                else if (type == typeof(ulong))
                {
                    return GetRandomSequence(rng, ulong.MinValue, ulong.MaxValue, numberOfValues);
                }
                else if (type == typeof(long))
                {
                    return GetRandomSequence(rng, long.MinValue, long.MaxValue, numberOfValues);
                }
                else
                {
                    throw new NotSupportedException(string.Format(Strings.RandomNotIntegerType, nameof(RandomAttribute)));
                }
            }
        }

        private static IEnumerable<object> GetRandomSequenceFromLong(Type type, long lowerBound, long upperBound, int numberOfValues, int? seed)
        {
            var rng = seed.HasValue ? new Random(seed.Value).WarmUp() : SharedRandom;

            if (type == typeof(byte))
            {
                if (lowerBound < byte.MinValue || lowerBound > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "byte"));
                }

                if (upperBound < byte.MinValue || upperBound > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "byte"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (byte)lowerBound, (byte)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(sbyte))
            {
                if (lowerBound < sbyte.MinValue || lowerBound > sbyte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "sbyte"));
                }

                if (upperBound < sbyte.MinValue || upperBound > sbyte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "sbyte"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (sbyte)lowerBound, (sbyte)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(ushort))
            {
                if (lowerBound < ushort.MinValue || lowerBound > ushort.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "ushort"));
                }

                if (upperBound < ushort.MinValue || upperBound > ushort.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "ushort"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (ushort)lowerBound, (ushort)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(short))
            {
                if (lowerBound < short.MinValue || lowerBound > short.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "short"));
                }

                if (upperBound < short.MinValue || upperBound > short.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "short"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (short)lowerBound, (short)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(uint))
            {
                if (lowerBound < uint.MinValue || lowerBound > uint.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "uint"));
                }

                if (upperBound < uint.MinValue || upperBound > uint.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "uint"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (uint)lowerBound, (uint)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(int))
            {
                if (lowerBound < int.MinValue || lowerBound > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "int"));
                }

                if (upperBound < int.MinValue || upperBound > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "int"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (int)lowerBound, (int)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(ulong))
            {
                if (lowerBound < 0L)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "ulong"));
                }

                if (upperBound < 0L)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "ulong"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (ulong)lowerBound, (ulong)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(long))
            {
                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (long)lowerBound, (long)upperBound, numberOfValues);
                }
            }
            else
            {
                throw new NotSupportedException(string.Format(Strings.RandomNotIntegerType, nameof(RandomAttribute)));
            }
        }

        private static IEnumerable<object> GetRandomSequenceFromULong(Type type, ulong lowerBound, ulong upperBound, int numberOfValues, int? seed)
        {
            var rng = seed.HasValue ? new Random(seed.Value).WarmUp() : SharedRandom;
            if (type == typeof(byte))
            {
                if (lowerBound > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "byte"));
                }

                if (upperBound > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "byte"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (byte)lowerBound, (byte)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(sbyte))
            {
                if (lowerBound > (ulong)sbyte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "sbyte"));
                }

                if (upperBound > (ulong)sbyte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "sbyte"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (sbyte)lowerBound, (sbyte)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(ushort))
            {
                if (lowerBound > ushort.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "ushort"));
                }

                if (upperBound > ushort.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "ushort"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (ushort)lowerBound, (ushort)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(short))
            {
                if (lowerBound > (ulong)short.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "short"));
                }

                if (upperBound > (ulong)short.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "short"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (short)lowerBound, (short)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(uint))
            {
                if (lowerBound > uint.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "uint"));
                }

                if (upperBound > uint.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "uint"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (uint)lowerBound, (uint)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(int))
            {
                if (lowerBound > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "int"));
                }

                if (upperBound > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "int"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (int)lowerBound, (int)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(ulong))
            {
                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (ulong)lowerBound, (ulong)upperBound, numberOfValues);
                }
            }
            else if (type == typeof(long))
            {
                if (lowerBound > long.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(lowerBound), string.Format(Strings.RangeLowerBoundOutOfRange, "long"));
                }

                if (upperBound > long.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(upperBound), string.Format(Strings.RangeUpperBoundOutOfRange, "long"));
                }

                if (lowerBound > upperBound)
                {
                    var switchVar = lowerBound;
                    lowerBound = upperBound;
                    upperBound = switchVar;
                }

                lock (rng)
                {
                    return GetRandomSequence(rng, (long)lowerBound, (long)upperBound, numberOfValues);
                }
            }
            else
            {
                throw new NotSupportedException(string.Format(Strings.RandomNotIntegerType, nameof(RandomAttribute)));
            }
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, byte start, byte end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedByte(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, sbyte start, sbyte end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedSByte(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, ushort start, ushort end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedUInt16(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, short start, short end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedInt16(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, uint start, uint end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedUInt32(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, int start, int end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedInt32(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, ulong start, ulong end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedUInt64(start, end);
            }

            return values;
        }

        private static IEnumerable<object> GetRandomSequence(Random rng, long start, long end, int numberOfValues)
        {
            var values = new object[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = rng.GetUnbiasedInt64(start, end);
            }

            return values;
        }
    }
}
