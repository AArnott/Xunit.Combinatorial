// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents a source of values for a parameter in a test, which will be combined with the values for other parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ParameterValuesAttribute : Attribute
    {
        /// <summary>
        /// Obtains the sequence of values that should be used for the given parameter
        /// </summary>
        /// <param name="parameter">The test method parameter to get values for</param>
        /// <returns>The values to be used for the test method parameter</returns>
        public abstract IEnumerable<object> GetValues(ParameterInfo parameter);
    }
}
