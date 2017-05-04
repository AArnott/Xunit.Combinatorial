// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;

    internal static class TypeHelpers
    {
        public static bool IsIntegerType(this Type type)
        {
            return type == typeof(byte) || type == typeof(sbyte)
                || type == typeof(ushort) || type == typeof(short)
                || type == typeof(uint) || type == typeof(int)
                || type == typeof(ulong) || type == typeof(long);
        }

        public static bool IsFloatingType(this Type type)
        {
            return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
        }

        public static bool IsNumericType(this Type type) => type.IsIntegerType() || type.IsFloatingType();
    }
}
