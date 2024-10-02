// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Xunit;

/// <summary>
/// Defines a class that provides values for a parameter on a test method.
/// </summary>
public interface ICombinatorialValuesProvider
{
    /// <summary>
    /// Gets the values that should be passed to this parameter on the test method.
    /// </summary>
    /// <param name="parameter">The parameter to get values for.</param>
    /// <returns>An array of values.</returns>
    object?[] GetValues(ParameterInfo parameter);
}
