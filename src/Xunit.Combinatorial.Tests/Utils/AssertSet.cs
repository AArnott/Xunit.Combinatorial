// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests.Utils
{
    using System;

    internal static class AssertSets
    {
        public static void Equal<T>(T[] expected, T[] actual)
        {
            if (!Test.SetsEqual(expected, actual))
            {
                throw new SetsNotEqualException<T>(expected, actual, "The two sets do not contain equivalent values");
            }
        }

        public static void Equal<T>(T[] expected, T[] actual, Func<T, T, bool> comparerFunc)
        {
            if (!Test.SetsEqual(expected, actual, comparerFunc))
            {
                throw new SetsNotEqualException<T>(expected, actual, "The two sets do not contain equivalent values");
            }
        }
    }
}
