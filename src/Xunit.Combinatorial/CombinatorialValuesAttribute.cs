// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using Validation;

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
        public CombinatorialValuesAttribute(params object[] values)
        {
            Requires.NotNull(values, nameof(values));

            this.Values = values;
        }

        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <value>An array of values.</value>
        public object[] Values { get; }
    }
}
