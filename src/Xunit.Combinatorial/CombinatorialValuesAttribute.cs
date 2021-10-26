// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;

    /// <summary>
    /// Specifies which values for this parameter should be used for running the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialValuesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialValuesAttribute"/> class.
        /// </summary>
        /// <param name="values">The values to pass to this parameter.</param>
        public CombinatorialValuesAttribute(params object?[]? values)
        {
            // When values is `null`, it's because the user passed in `null` as the only value and C# interpreted it as a null array.
            // Re-interpret that.
            this.Values = values ?? new object?[] { null };
        }

        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <value>An array of values.</value>
        public object?[] Values { get; }
    }
}
