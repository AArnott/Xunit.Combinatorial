// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Xunit;

/// <summary>
/// Specifies which values for this parameter should be used for running the test method.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class CombinatorialValuesAttribute : Attribute, ICombinatorialValuesProvider
{
    private readonly object?[] values;

    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialValuesAttribute"/> class.
    /// </summary>
    /// <param name="values">The values to pass to this parameter.</param>
    public CombinatorialValuesAttribute(params object?[]? values)
    {
        // When values is `null`, it's because the user passed in `null` as the only value and C# interpreted it as a null array.
        // Re-interpret that.
        this.values = values ?? new object?[] { null };
    }

    /// <inheritdoc />
    public object?[] GetValues(ParameterInfo parameter)
    {
        return this.values;
    }
}
