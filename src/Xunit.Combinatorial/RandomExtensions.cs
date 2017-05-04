// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;

    internal static class RandomExtensions
    {
        public static Random WarmUp(this Random random)
        {
            for (var i = 0; i < 2000; i++)
            {
                random.Next();
            }

            return random;
        }

        public static byte GetUnbiasedByte(this Random random, byte lowerBound, byte upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);
            return (byte)(lowerBound + random.GetUnbiasedUInt16Upto((ushort)difference));
        }

        public static sbyte GetUnbiasedSByte(this Random random, sbyte lowerBound, sbyte upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);
            return (sbyte)(lowerBound + random.GetUnbiasedUInt16Upto((ushort)difference));
        }

        public static ushort GetUnbiasedUInt16(this Random random, ushort lowerBound, ushort upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);
            return (ushort)(lowerBound + random.GetUnbiasedUInt16Upto((ushort)difference));
        }

        public static short GetUnbiasedInt16(this Random random, short lowerBound, short upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);
            return (short)(lowerBound + random.GetUnbiasedUInt32Upto(difference));
        }

        public static uint GetUnbiasedUInt32(this Random random, uint lowerBound, uint upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = (uint)Difference(lowerBound, upperBound);
            return lowerBound + random.GetUnbiasedUInt32Upto(difference);
        }

        public static int GetUnbiasedInt32(this Random random, int lowerBound, int upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);

            return (int)(lowerBound + random.GetUnbiasedUInt32Upto(difference));
        }

        public static ulong GetUnbiasedUInt64(this Random random, ulong lowerBound, ulong upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = upperBound - lowerBound;

            if (difference == 0)
            {
                return lowerBound;
            }
            else
            {
                return lowerBound + random.GetUnbiasedUInt64Upto(difference);
            }
        }

        public static long GetUnbiasedInt64(this Random random, long lowerBound, long upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException(string.Format(Strings.RandomExtensionsLowerBoundLessThanUpperBound, nameof(lowerBound), nameof(upperBound), nameof(lowerBound)));
            }

            var difference = Difference(lowerBound, upperBound);
            var randomVal = random.GetUnbiasedUInt64Upto(difference);

            if (randomVal <= long.MaxValue)
            {
                return lowerBound + (long)randomVal;
            }
            else
            {
                var newLow = lowerBound + long.MaxValue;
                var newVal = randomVal - long.MaxValue;
                return newLow + (long)newVal;
            }
        }

        private static ushort GetUnbiasedUInt16Upto(this Random random, ushort upperLimit)
        {
            if (upperLimit == ushort.MaxValue)
            {
                return random.GetUnbiasedUInt16();
            }
            else if (upperLimit == ushort.MaxValue - 1)
            {
                ushort x;
                do
                {
                    x = random.GetUnbiasedUInt16();
                }
                while (x == ushort.MaxValue);

                return x;
            }
            else
            {
                ushort remainder = (ushort)(ushort.MaxValue % (upperLimit + 2));
                ushort checkValue = (ushort)(ushort.MaxValue - remainder);
                ushort x;
                do
                {
                    x = random.GetUnbiasedUInt16();
                }
                while (x >= checkValue);

                return (ushort)(x % (upperLimit + 1));
            }
        }

        private static ushort GetUnbiasedUInt16(this Random random)
        {
            const int limit = ushort.MaxValue + 1;
            return (ushort)(random.Next() % limit);
        }

        private static uint GetUnbiasedUInt32Upto(this Random random, uint upperLimit)
        {
            if (upperLimit == uint.MaxValue)
            {
                return random.GetUnbiasedUInt32();
            }
            else if (upperLimit == uint.MaxValue - 1)
            {
                uint x;
                do
                {
                    x = random.GetUnbiasedUInt32();
                }
                while (x == uint.MaxValue);

                return x;
            }
            else
            {
                uint remainder = uint.MaxValue % (upperLimit + 2);
                uint checkValue = uint.MaxValue - remainder;
                uint x;
                do
                {
                    x = random.GetUnbiasedUInt32();
                }
                while (x >= checkValue);

                return x % (upperLimit + 1);
            }
        }

        private static uint GetUnbiasedUInt32(this Random random)
        {
            const int limit = ushort.MaxValue + 1;
            uint s1 = (uint)(random.Next() % limit) << 16;
            uint s2 = (uint)(random.Next() % limit);
            return s1 | s2;
        }

        private static ulong GetUnbiasedUInt64Upto(this Random random, ulong upperLimit)
        {
            if (upperLimit == ulong.MaxValue)
            {
                return random.GetUnbiasedUInt64();
            }
            else if (upperLimit == ulong.MaxValue - 1)
            {
                ulong x;
                do
                {
                    x = random.GetUnbiasedUInt64();
                }
                while (x == ulong.MaxValue);

                return x;
            }
            else
            {
                ulong remainder = ulong.MaxValue % (upperLimit + 2);
                ulong checkValue = ulong.MaxValue - remainder;
                ulong x;
                do
                {
                    x = random.GetUnbiasedUInt64();
                }
                while (x >= checkValue);

                return x % (upperLimit + 1);
            }
        }

        private static ulong GetUnbiasedUInt64(this Random random)
        {
            const int limit = ushort.MaxValue + 1;
            ulong s1 = (ulong)(random.Next() % limit) << 48;
            ulong s2 = (ulong)(random.Next() % limit) << 32;
            ulong s3 = (ulong)(random.Next() % limit) << 16;
            ulong s4 = (ulong)(random.Next() % limit);
            return s1 | s2 | s3 | s4;
        }

        private static uint Difference(int a, int b)
        {
            if (b < 0)
            {
                // a < b < 0
                return (uint)(b - a);
            }
            else if (a < 0)
            {
                // b > 0 > a
                return (a == int.MinValue ? 2147483648U : (uint)Math.Abs(a)) + (uint)b;
            }
            else
            {
                // b > a > 0
                return (uint)(b - a);
            }
        }

        private static ulong Difference(long a, long b)
        {
            if (b < 0)
            {
                // a < b < 0
                return (uint)(b - a);
            }
            else if (a < 0)
            {
                // b > 0 > a
                return (a == long.MinValue ? 9223372036854775808UL : (ulong)Math.Abs(a)) + (ulong)b;
            }
            else
            {
                // b > a > 0
                return (ulong)(b - a);
            }
        }
    }
}
