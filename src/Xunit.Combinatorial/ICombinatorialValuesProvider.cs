// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System.Reflection;

    /// <summary>
    /// Specifies which values for this parameter should be used for running the test method.
    /// </summary>
    public interface ICombinatorialValuesProvider
    {
        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <param name="parameter">The parameter for which the values should be provided</param>
        /// <returns>The values</returns>
        object[] GetValues(ParameterInfo parameter);
    }
}
