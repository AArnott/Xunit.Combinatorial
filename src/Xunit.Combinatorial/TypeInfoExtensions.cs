// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Extension methods for the <see cref="Type"/> class to emulate older reflection APIs.
    /// </summary>
    internal static class TypeInfoExtensions
    {
        /// <summary>
        /// Returns the generic type arguments of specified type.
        /// </summary>
        /// <param name="type">The type whose generic type arguments should be returned.</param>
        /// <returns>An array of types.</returns>
        /// <remarks>
        /// This silly method allows the same code to compile against the newer
        /// as well as older Reflection APIs.
        /// </remarks>
        internal static Type[] GetGenericArguments(this TypeInfo type) => type.GenericTypeArguments;
    }
}
