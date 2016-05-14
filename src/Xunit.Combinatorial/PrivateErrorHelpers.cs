// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Common utility methods used by the various error detection and reporting classes.
    /// </summary>
    internal static class PrivateErrorHelpers
    {
        /// <summary>
        /// Trims away a given surrounding type, returning just the generic type argument,
        /// if the given type is in fact a generic type with just one type argument and
        /// the generic type matches a given wrapper type.  Otherwise, it returns the original type.
        /// </summary>
        /// <param name="type">The type to trim, or return unmodified.</param>
        /// <param name="wrapper">The SomeType&lt;&gt; generic type definition to trim away from <paramref name="type"/> if it is present.</param>
        /// <returns><paramref name="type"/>, if it is not a generic type instance of <paramref name="wrapper"/>; otherwise the type argument.</returns>
        internal static Type TrimGenericWrapper(Type type, Type wrapper)
        {
            Type[] typeArgs;
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == wrapper && (typeArgs = type.GetTypeInfo().GetGenericArguments()).Length == 1)
            {
                return typeArgs[0];
            }
            else
            {
                return type;
            }
        }

        /// <summary>
        /// Helper method that formats string arguments.
        /// </summary>
        /// <param name="format">The unformatted string.</param>
        /// <param name="arguments">The formatting arguments.</param>
        /// <returns>The formatted string.</returns>
        internal static string Format(string format, params object[] arguments)
        {
            return string.Format(CultureInfo.CurrentCulture, format, arguments);
        }
    }
}
