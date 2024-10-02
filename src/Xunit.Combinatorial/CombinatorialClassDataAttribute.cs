﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Reflection;

namespace Xunit;

/// <summary>
///     Specifies a class that provides the values for a combinatorial test.
/// </summary>
public class CombinatorialClassDataAttribute : Attribute, ICombinatorialValuesProvider
{
    private readonly object?[] values;

    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialClassDataAttribute" /> class.
    /// </summary>
    /// <param name="valuesSourceType">The type of the class that provides the values for a combinatorial test.</param>
    /// <param name="arguments">The arguments to pass to the constructor of <paramref name="valuesSourceType" />.</param>
    public CombinatorialClassDataAttribute(Type valuesSourceType, params object[]? arguments)
    {
        this.values = GetValues(valuesSourceType, arguments);
    }

    /// <inheritdoc />
    public object?[] GetValues(ParameterInfo parameter)
    {
        return this.values;
    }

    private static object?[] GetValues(Type valuesSourceType, object[]? args)
    {
        Requires.NotNull(valuesSourceType, nameof(valuesSourceType));

        if (!typeof(IEnumerable<object[]>).IsAssignableFrom(valuesSourceType))
        {
            throw new InvalidOperationException(
                $"The values source must be assignable to {typeof(IEnumerable<object?[]>)}).");
        }

        IEnumerable<object[]>? values;

        try
        {
            values = (IEnumerable<object[]>)Activator.CreateInstance(
                valuesSourceType,
                BindingFlags.CreateInstance | BindingFlags.OptionalParamBinding,
                null,
                args,
                CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Failed to create an instance of {valuesSourceType}. " +
                $"Please make sure the type has a public constructor and the arguments match.",
                ex);
        }

        return values.SelectMany(rows => rows).ToArray();
    }
}